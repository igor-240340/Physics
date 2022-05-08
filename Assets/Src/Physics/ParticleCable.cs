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
                normal = (particleB.pos - particleA.pos).normalized,
                penetration = CurrentLength() - maxLength,
                restitution = restitution
            };
            // Debug.DrawLine(particleA.pos, particleA.pos + contact.normal, Color.red, 10);
            Debug.Log($"Contact detected:\n" +
                      $"Apos:{particleA.pos.ToString("F4")}\n" +
                      $"Bpos:{particleB.pos.ToString("F4")}\n" +
                      $"Avel:{particleA.velocity.ToString("F4")}\n" +
                      $"Bvel:{particleB.velocity.ToString("F4")}\n" +
                      $"penetration: {contact.penetration}");
        }
        else
            Debug.DrawLine(particleB.pos, particleA.pos, Color.green);

        return contact is not null;
    }
}