using System.Collections.Generic;
using UnityEngine;

public class ParticleWorld
{
    public List<Particle> particles = new List<Particle>();

    private Vector3 gravity = new Vector3(0, -10, 0);

    public void Add(Particle particle)
    {
        particles.Add(particle);
    }

    public void Add(ParticleLink link)
    {
    }

    public void Step(float dt)
    {
        Debug.Log($"step: {dt}");
    }
}