using UnityEngine;

public class BungeeGenerator : ForceGenerator
{
    private float k;
    private float restLength;
    private Vector3 attachPoint;

    public BungeeGenerator(float k, float restLength, Vector3 attachPoint)
    {
        this.k = k;
        this.restLength = restLength;
        this.attachPoint = attachPoint;
    }

    public void GetImpact(Particle particle)
    {
        Vector3 spring = particle.transform.position - attachPoint;
        Vector3 dir = spring.normalized;
        Vector3 restSpring = dir * restLength;
        Vector3 compression = spring - restSpring;

        if (spring.magnitude < restSpring.magnitude)
            return;

        Debug.DrawLine(attachPoint, attachPoint + restSpring, Color.white);
        Debug.DrawLine(attachPoint + restSpring, attachPoint + restSpring + compression, Color.magenta);

        Vector3 force = -compression * k;

        particle.ApplyForce(force);
    }

    public Vector3 AttachPoint => attachPoint;

    public Vector3 HangPoint => attachPoint - new Vector3(0, restLength, 0);
}