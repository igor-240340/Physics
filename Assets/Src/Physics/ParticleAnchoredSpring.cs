using UnityEngine;

public class ParticleAnchoredSpring : ParticleForceGenerator
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
        float springLength = spring.magnitude;
        float compression = springLength - restLength;
        Vector3 force = spring.normalized * (-compression * k);
        
        particle.ApplyForce(force);

        Debug.DrawLine(anchor, anchor + spring.normalized * restLength, Color.white);

        // Debug.Log($"compression: {compression.ToString("F5")}");
        Debug.DrawLine(anchor + spring.normalized * restLength,
            (anchor + spring.normalized * restLength) + spring.normalized * compression, Color.magenta);


        Debug.Log($"force: {force.ToString("F5")}");
        // Debug.DrawLine(particle.transform.position, particle.transform.position + force / 100f, Color.yellow);
    }
}