using UnityEngine;

public class ParticleAnchoredSpring : IParticleForceGenerator
{
    private float k;
    private float restLength;
    private Vector3 anchor;

    public ParticleAnchoredSpring(Vector3 anchor, float k, float restLength)
    {
        this.anchor = anchor;
        this.k = k;
        this.restLength = restLength;
    }

    public void ApplyTo(Particle particle)
    {
        Vector3 spring = particle.pos - anchor;
        Vector3 springDir = spring.normalized;
        float springLength = spring.magnitude;
        float compression = springLength - restLength;
        Vector3 force = springDir * (-compression * k);

        Vector3 restSpringEnd = anchor + springDir * restLength;
        Debug.DrawLine(anchor, restSpringEnd, Color.green);
        Debug.DrawLine(restSpringEnd, particle.pos, Color.magenta);

        particle.ApplyForce(force);
    }
}