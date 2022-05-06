using UnityEngine;

public class ParticleContact
{
    public Particle particleA, particleB;
    public Vector3 contactNormal;
    public float penetration;
    public float restitution;

    public void Resolve()
    {
        ResolveVelocity();
        ResolveInterpenetration();
    }

    private void ResolveVelocity()
    {
        float sepVelocity = CalculateSeparatingVelocity();
        float impulseANormalProj = -sepVelocity * (1 + restitution) / (particleA.invMass + particleB.invMass);
        
        Vector3 impulseA = impulseANormalProj * contactNormal;
        particleA.velocity += impulseA * particleA.invMass;
        particleB.velocity += -impulseA * particleB.invMass;
    }

    private void ResolveInterpenetration()
    {
        Debug.Log("ResolveInterpenetration");
    }

    private float CalculateSeparatingVelocity()
    {
        return Vector3.Dot(particleA.velocity - particleB.velocity, contactNormal);
    }
}