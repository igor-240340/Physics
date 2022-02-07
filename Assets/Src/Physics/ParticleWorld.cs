using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class ParticleWorld
{
    public List<Particle> particles = new();
    public ParticleForceRegistry forceRegistry = new();
    public List<IParticleContactGenerator> contactGenerators = new();

    private Vector3 gravity = new(0, -10, 0);
    private float sqrWorldSize = (Vector3.one * 10).sqrMagnitude;
    private List<Particle> outOfWorld = new();
    private ParticleContactResolver contactResolver = new();

    public void Add(Particle particle)
    {
        particles.Add(particle);
    }

    public void Step(float dt)
    {
        forceRegistry.ApplyForces();

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

        List<ParticleContact> contacts = new();
        contactGenerators.ForEach(contactGen =>
        {
            if (contactGen.GenerateContact(out ParticleContact contact))
                contacts.Add(contact);
        });

        contactResolver.ResolveContacts(contacts);

        outOfWorld.ForEach(particle =>
        {
            particles.Remove(particle);
            forceRegistry.Remove(particle);
        });
        outOfWorld.Clear();
    }

    public void Clear()
    {
        particles.Clear();
        forceRegistry.Clear();
    }
}