using UnityEngine;

public class ParticleContact
{
    public Particle particleA, particleB;
    public float restitution;
    public Vector3 contactNormal;
    public float penetration;

    public void Resolve()
    {
        ResolveVelocity();
        ResolveInterpenetration();
    }

    private void ResolveVelocity()
    {
        float separatingVelocity = Vector3.Dot(particleA.velocity - particleB.velocity, contactNormal);
        float newSeparatingVelocity = -separatingVelocity * restitution;
        float dSeparatingVelocity = newSeparatingVelocity - separatingVelocity;

        Debug.Log($"separatingVelocity: {separatingVelocity}");
        Debug.Log($"newSeparatingVelocity: {newSeparatingVelocity}");
        Debug.Log($"dSeparatingVelocity: {dSeparatingVelocity}");

        Debug.DrawLine(particleA.pos, particleA.pos + separatingVelocity * contactNormal, Color.yellow);
        Debug.DrawLine(particleA.pos, particleA.pos + newSeparatingVelocity * contactNormal, Color.red);
    }

    private void ResolveInterpenetration()
    {
    }
}