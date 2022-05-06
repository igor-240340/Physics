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
                particleA = particleA,
                particleB = particleB,
                contactNormal = (particleA.pos - particleB.pos).normalized,
                penetration = CurrentLength() - maxLength,
                restitution = restitution
            };
            Debug.DrawLine(particleB.pos, particleB.pos + contact.contactNormal * maxLength, Color.green);
            Debug.DrawLine(particleB.pos + contact.contactNormal * maxLength, particleA.pos, Color.magenta);
            Debug.Log($"Contact detected:\nA:{particleA.pos.ToString("F4")}\nB:{particleB.pos.ToString("F4")}");
        }
        else
            Debug.DrawLine(particleB.pos, particleA.pos, Color.green);

        return contact is not null;
    }
}