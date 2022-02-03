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
        particles.ForEach(particle =>
        {
            particle.position += particle.velocity * dt;

            Vector3 oldVelocity = particle.velocity;
            particle.velocity += (particle.force * particle.invMass + gravity) * dt;

            particle.position += (particle.velocity - oldVelocity) * dt / 2;
            
            particle.force = Vector3.zero;
        });
    }
}