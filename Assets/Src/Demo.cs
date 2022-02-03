using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Demo : MonoBehaviour
{
    [SerializeField]
    private Material particleMaterial;

    private ParticleWorld world = new ParticleWorld();
    private Particle particleA;
    private Particle particleB;

    private const float particleSize = 0.2f;
    private Mesh particleMesh;
    private Bounds particleBounds = new Bounds(Vector3.zero, Vector2.one * particleSize);

    private Particle selectedParticle;

    private bool mousePressed;
    private Vector3 firstMouseWorldPos;

    void Start()
    {
        BuildParticleMesh();
    }

    void FixedUpdate()
    {
        world.Step(Time.fixedDeltaTime);
    }

    void Update()
    {
        world.particles.ForEach(particle =>
        {
            Graphics.DrawMesh(particleMesh, particle.position, Quaternion.identity, particleMaterial, 0);

            if (mousePressed)
                SelectParticle(particle);
        });
    }

    void SelectParticle(Particle particle)
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Ray mouseRay = Camera.main.ScreenPointToRay(mouseScreenPos);

        particleBounds.center = particle.position;
        float dist;
        if (particleBounds.IntersectRay(mouseRay, out dist))
        {
            selectedParticle = particle;

            Debug.DrawRay(mouseWorldPos, mouseRay.direction * (dist + Camera.main.nearClipPlane), Color.magenta, 10);
            Debug.Log($"dist: {dist}");
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            mousePressed = true;
            firstMouseWorldPos = Utils.GetMouseWorldPos();
        }
        else if (context.action.phase == InputActionPhase.Canceled)
        {
            mousePressed = false;

            Vector3 forceToApply = (firstMouseWorldPos - Utils.GetMouseWorldPos()) * 100;
            Particle particle = new Particle();
            particle.SetMass(1);
            particle.SetPosition(firstMouseWorldPos);
            particle.ApplyForce(forceToApply);
            world.Add(particle);
        }
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
}