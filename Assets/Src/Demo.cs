using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        BuildParticleMesh();
        
        particleA = new Particle();
        particleA.SetMass(1);
        particleA.SetPosition(Vector3.zero);
        world.Add(particleA);

        particleB = new Particle();
        particleB.SetMass(1);
        particleB.SetPosition(Vector3.right);
        world.Add(particleB);
    }

    void FixedUpdate()
    {
        world.Step(Time.fixedDeltaTime);
    }

    void Update()
    {
        DrawParticles();
    }

    private void DrawParticles()
    {
        world.particles.ForEach(particle =>
        {
            Graphics.DrawMesh(particleMesh, particle.position, Quaternion.identity, particleMaterial, 0);
        });
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