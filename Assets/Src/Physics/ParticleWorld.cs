using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class ParticleWorld
{
    public List<Particle> particles = new List<Particle>();
    public ParticleForceRegistry forceRegistry = new ParticleForceRegistry();

    private Vector3 gravity = new Vector3(0, -10, 0);
    private float sqrWorldSize = (Vector3.one * 10).sqrMagnitude;
    private List<Particle> outOfWorld = new List<Particle>();

    public void Add(Particle particle)
    {
        particles.Add(particle);
    }

    public void Add(ParticleLink link)
    {
    }

    public void Step(float dt)
    {
        forceRegistry.ApplyForces();
        
        particles.ForEach(particle =>
        {
            particle.pos += particle.velocity * dt;

            if (particle.pos.sqrMagnitude > sqrWorldSize)
            {
                outOfWorld.Add(particle);
                return;
            }

            Vector3 oldVelocity = particle.velocity;
            particle.velocity += (particle.force * particle.invMass + gravity) * dt;

            particle.pos += (particle.velocity - oldVelocity) * dt / 2;

            particle.force = Vector3.zero;
        });

        outOfWorld.ForEach(particle => particles.Remove(particle));
        outOfWorld.Clear();
    }

    public void Clear()
    {
        particles.Clear();
        forceRegistry.Clear();
    }
}