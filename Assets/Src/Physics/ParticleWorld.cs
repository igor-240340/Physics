using System.Collections.Generic;
using UnityEngine;

public class ParticleWorld
{
    public List<Particle> particles = new();
    public ParticleForceRegistry forceRegistry = new();
    public List<IParticleContactGenerator> contactGenerators = new();

    private Vector3 gravity = new(0, -10, 0);
    private float sqrWorldSize = (Vector3.one * 10).sqrMagnitude;
    private ParticleContactResolver contactResolver = new();

    public void Add(Particle particle)
    {
        particles.Add(particle);
    }

    public void Step(float dt)
    {
        forceRegistry.ApplyForces();

        List<Particle> outOfWorld = new();
        particles.ForEach(particle =>
        {
            if (particle.isPaused)
                return;

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
        
        // todo: what should we do if a particle is linked with another particle
        outOfWorld.ForEach(particle =>
        {
            particles.Remove(particle);
            forceRegistry.Remove(particle);
        });

        List<ParticleContact> contacts = new();
        contactGenerators.ForEach(contactGen =>
        {
            if (contactGen.GenerateContact(out ParticleContact contact))
                contacts.Add(contact);
        });
        contactResolver.ResolveContacts(contacts);
    }

    public void Reset()
    {
        particles.Clear();
        forceRegistry.Clear();
        contactGenerators.Clear();
        SwitchOnGravity();
    }

    public void SwitchOffGravity()
    {
        gravity = Vector3.zero;
    }

    public void SwitchOnGravity()
    {
        gravity = new Vector3(0, -10, 0);
    }
}