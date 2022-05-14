using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ImGuiNET;

public class App : MonoBehaviour
{
    [SerializeField]
    private Material particleMaterial;

    private ParticleWorld world = new ParticleWorld();

    private const float particleSize = 0.2f;
    private Mesh particleMesh;
    private Bounds particleBounds = new Bounds(Vector3.zero, Vector2.one * particleSize);

    private List<IDemo> demos = new List<IDemo>();
    private IDemo activeDemo;

    private Particle pickedParticle;

    // ImGui
    private int corner;

    void OnEnable()
    {
        ImGuiUn.Layout += OnLayout;
    }

    void OnDisable()
    {
        ImGuiUn.Layout -= OnLayout;
    }

    void Start()
    {
        // Time.timeScale = 0.1f;
        // Application.targetFrameRate = 8;
        
        BuildParticleMesh();
        CreateDemos();
    }

    private void BuildParticleMesh()
    {
        particleMesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-particleSize / 2, -particleSize / 2);
        vertices[1] = new Vector3(-particleSize / 2, particleSize / 2);
        vertices[2] = new Vector3(particleSize / 2, particleSize / 2);
        vertices[3] = new Vector3(particleSize / 2, -particleSize / 2);
        particleMesh.vertices = vertices;

        particleMesh.triangles = new[]
        {
            0, 1, 3,
            1, 2, 3
        };

        particleMesh.normals = new[]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
    }

    private void CreateDemos()
    {
        demos.Add(new ParticleShotDemo(world));
        demos.Add(new AnchoredSpringDemo(world));
        demos.Add(new ParticleCableDemo(world));
        demos.Add(new ParticleRodDemo(world));
    }

    void FixedUpdate()
    {
        world.Step(Time.fixedDeltaTime);
    }

    void Update()
    {
        world.particles.ForEach(particle =>
        {
            Graphics.DrawMesh(particleMesh, particle.pos, Quaternion.identity, particleMaterial, 0);
            Debug.DrawLine(particle.pos, particle.pos + particle.velocity, Color.blue);
        });
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            Ray mouseRay = Utils.GetMouseRay();
            foreach (Particle particle in world.particles)
            {
                if (TryPickParticle(particle, mouseRay, out pickedParticle))
                {
                    particle.PausePhysics();
                    particle.force = particle.velocity = Vector3.zero;
                    break;
                }
            }
        }
        else if (context.action.phase == InputActionPhase.Canceled)
        {
            pickedParticle?.ResumePhysics();
            pickedParticle = null;
        }

        activeDemo?.OnFire(context);
    }

    private bool TryPickParticle(Particle particle, Ray mouseRay, out Particle picked)
    {
        picked = null;

        particleBounds.center = particle.pos;
        if (particleBounds.IntersectRay(mouseRay))
            picked = particle;

        return picked is not null;
    }

    public void OnDemoSelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            for (Key key = Key.Digit1; key <= Key.Digit0; key++)
            {
                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    int demoIndex = (int)key - (int)Key.Digit1;
                    if (demoIndex < demos.Count)
                    {
                        world.Reset();
                        activeDemo = demos[demoIndex];
                        activeDemo.Init();
                        break;
                    }
                }
            }
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (pickedParticle != null)
            pickedParticle.pos = Utils.GetMouseWorldPosXY();
    }

    // ImGui
    void OnLayout()
    {
        ShowInfoOverlay();
    }

    private void ShowInfoOverlay()
    {
        ImGuiWindowFlags windowFlags = ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.AlwaysAutoResize |
                                       ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoFocusOnAppearing |
                                       ImGuiWindowFlags.NoNav;
        if (corner != -1)
        {
            float pad = 10;
            Vector2 workPos = Camera.main.pixelRect.position;
            Vector2 workSize = Camera.main.pixelRect.size;
            Vector2 windowPos, windowPosPivot;
            windowPos.x = Convert.ToBoolean(corner & 1) ? (workPos.x + workSize.x - pad) : (workPos.x + pad);
            windowPos.y = Convert.ToBoolean(corner & 2) ? (workPos.y + workSize.y - pad) : (workPos.y + pad);
            windowPosPivot.x = Convert.ToBoolean(corner & 1) ? 1.0f : 0.0f;
            windowPosPivot.y = Convert.ToBoolean(corner & 2) ? 1.0f : 0.0f;
            ImGui.SetNextWindowPos(windowPos, ImGuiCond.Always, windowPosPivot);
            windowFlags |= ImGuiWindowFlags.NoMove;
        }

        ImGui.SetNextWindowBgAlpha(1);

        bool pOpen = false;
        if (ImGui.Begin("Help", ref pOpen, windowFlags))
        {
            ImGui.Text("To switch between demos press keys 0-9");
            ImGui.Separator();
            ImGui.Text($"Current demo: {activeDemo}");
            ImGui.Separator();
            ImGui.Text($"Particles in world: {world.particles.Count}");

            if (pickedParticle != null)
            {
                ImGui.Separator();
                ImGui.Text($"Picked particle\n" +
                           $"pos: {pickedParticle.pos}\n" +
                           $"vel: {pickedParticle.velocity}\n" +
                           $"force: {pickedParticle.force}");
            }

            if (ImGui.BeginPopupContextWindow())
            {
                if (ImGui.MenuItem("Custom", null, corner == -1)) corner = -1;
                if (ImGui.MenuItem("Top-left", null, corner == 0)) corner = 0;
                if (ImGui.MenuItem("Top-right", null, corner == 1)) corner = 1;
                if (ImGui.MenuItem("Bottom-left", null, corner == 2)) corner = 2;
                if (ImGui.MenuItem("Bottom-right", null, corner == 3)) corner = 3;
                if (pOpen && ImGui.MenuItem("Close")) pOpen = false;
                ImGui.EndPopup();
            }
        }

        ImGui.End();
    }
}