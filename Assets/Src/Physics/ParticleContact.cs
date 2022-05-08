using UnityEngine;

public class ParticleContact
{
    public Particle particleA, particleB;
    public Vector3 normal;
    public float penetration;
    public float restitution;

    public void Resolve()
    {
        if (particleA.invMass == 0.0f && particleB.invMass == 0.0f)
            return;
        
        ResolveVelocity();
        ResolvePenetration();
    }

    private void ResolveVelocity()
    {
        float normalVelocity = CalculateNormalVelocity();
        float impulseNormalProj = -normalVelocity * (1 + restitution) / (particleA.invMass + particleB.invMass);

        Vector3 impulse = impulseNormalProj * normal;
        particleA.velocity += impulse * particleA.invMass;
        particleB.velocity += -impulse * particleB.invMass;
    }

    private float CalculateNormalVelocity()
    {
        return Vector3.Dot(particleA.velocity - particleB.velocity, normal);
    }

    private void ResolvePenetration()
    {
        // Splits total penetration between two particles in inverse proportion to their masses.
        float penOverTotalInvMass = penetration / (particleA.invMass + particleB.invMass);
        Vector3 offsetA = penOverTotalInvMass * particleA.invMass * normal;
        Vector3 offsetB = -(penOverTotalInvMass * particleB.invMass * normal);

        Debug.Log($"offsetA:{offsetA.magnitude}\n" +
                  $"offsetB:{offsetB.magnitude}\n" +
                  $"pen:{penetration}\n");

        Debug.Log($"newAvel:{particleA.velocity.ToString("F4")}\n" +
                  $"newBvel:{particleB.velocity.ToString("F4")}\n");

        particleA.pos += offsetA;
        particleB.pos += offsetB;
    }
}