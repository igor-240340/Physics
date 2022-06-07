using UnityEngine;

public class ParticleRod : ParticleLink, IParticleContactGenerator
{
    public float maxLength;

    public ParticleRod(Particle particleA, Particle particleB, float maxLength)
        : base(particleA, particleB)
    {
        this.maxLength = maxLength;
    }

    public bool GenerateContact(out ParticleContact contact)
    {
        contact = null;

        if (CurrentLength() - maxLength > 0.0f)
        {
            contact = new ParticleContact
            {
                particleA = particleA,
                particleB = particleB,
                normal = (particleB.pos - particleA.pos).normalized,
                penetration = CurrentLength() - maxLength,
                restitution = 0.0f
            };
            Debug.Log($"Contact detected ({contact.GetHashCode()}):\n" +
                      $"Apos:{particleA.pos.ToString("F4")}\n" +
                      $"Bpos:{particleB.pos.ToString("F4")}\n" +
                      $"Avel:{particleA.velocity.ToString("F4")}\n" +
                      $"Bvel:{particleB.velocity.ToString("F4")}\n" +
                      $"penetration: {contact.penetration}");
        }
        else if (CurrentLength() - maxLength < 0.0f)
        {
            contact = new ParticleContact
            {
                particleA = particleA,
                particleB = particleB,
                normal = (particleA.pos - particleB.pos).normalized,
                penetration = maxLength - CurrentLength(),
                restitution = 0.0f
            };
            Debug.Log($"Contact detected ({contact.GetHashCode()}):\n" +
                      $"Apos:{particleA.pos.ToString("F4")}\n" +
                      $"Bpos:{particleB.pos.ToString("F4")}\n" +
                      $"Avel:{particleA.velocity.ToString("F4")}\n" +
                      $"Bvel:{particleB.velocity.ToString("F4")}\n" +
                      $"penetration: {contact.penetration}");
        }

        Debug.DrawLine(particleB.pos, particleA.pos, Color.green);

        return contact is not null;
    }
}