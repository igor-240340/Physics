using UnityEngine;

public class ParticleCable : ParticleLink, IParticleContactGenerator
{
    public float maxLength;
    public float restitution;

    public ParticleCable(Particle particleA, Particle particleB, float maxLength, float restitution)
        : base(particleA, particleB)
    {
        this.maxLength = maxLength;
        this.restitution = restitution;
    }

    public bool GenerateContact(out ParticleContact contact)
    {
        contact = null;

        if (CurrentLength() >= maxLength)
        {
            contact = new ParticleContact
            {
                contactNormal = (particleA.pos - particleB.pos).normalized,
                particleA = particleA,
                particleB = particleB,
                penetration = CurrentLength() - maxLength,
                restitution = restitution
            };
            Debug.DrawLine(particleB.pos, particleB.pos + contact.contactNormal * maxLength, Color.green);
            Debug.DrawLine(particleB.pos + contact.contactNormal * maxLength, particleA.pos, Color.magenta);
        }
        else
            Debug.DrawLine(particleB.pos, particleA.pos, Color.green);

        return contact is not null;
    }
}