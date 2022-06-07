using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        MatplotHelper.ClearPlot();

        /*MyPlot.SubPlot(2, 2, 1);
        MatplotHelper.DrawParticles(particles);
        MatplotHelper.DrawGens(contactGenerators);
        MatplotHelper.DrawVels(particles);*/
        
        /*MyPlot.SubPlot(2, 2, 2);
        MatplotHelper.DrawParticles(particles,"grey");
        MatplotHelper.DrawGens(contactGenerators, false, "grey");
        MatplotHelper.DrawVels(particles, "grey");*/

        forceRegistry.ApplyForces();

        List<Particle> outOfWorld = new();
        particles.ForEach(particle =>
        {
            if (particle.isPaused || particle.invMass == 0.0f)
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
        
        // Preserve positions after integrating
        MatplotHelper.CopyPositionsOf(particles);
        
        // MyPlot.SubPlot(2, 2, 2);
        /*MatplotHelper.DrawParticles(particles);
        MatplotHelper.DrawGens(contactGenerators, false);
        MatplotHelper.DrawVels(particles);*/

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
            {
                contacts.Add(contact);
            }
        });

        MatplotHelper.PreserveContactState(contacts);
        MatplotHelper.particles = particles;

        // MyPlot.clf();
        /*contacts.ForEach(c =>
        {
            MyPlot.scatter(
                new[] {c.particleA.pos.x, c.particleB.pos.x},
                new[] {c.particleA.pos.y, c.particleB.pos.y}, 2,
                new[]
                {
                    new MyPlot.KeywordValue("c", "k")
                }, 1, 20.0f);

            Vector3 penEnd = c.particleA.pos + (c.normal * c.penetration);
            MyPlot.plot(
                new[] {c.particleA.pos.x, penEnd.x},
                new[] {c.particleA.pos.y, penEnd.y}, 2,
                new[] {new MyPlot.KeywordValue("c", "red")}, 1);

            MyPlot.plot(
                new[] {penEnd.x, c.particleB.pos.x},
                new[] {penEnd.y, c.particleB.pos.y}, 2,
                new[] {new MyPlot.KeywordValue("c", "green")}, 1);
        });*/

        contactResolver.ResolveContacts(contacts);
        
        /*MyPlot.SubPlot(2, 2, 3);
        MatplotHelper.DrawParticles(particles);
        MatplotHelper.DrawGens(contactGenerators);
        MatplotHelper.DrawVels(particles);*/
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