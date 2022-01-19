using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SpringBuoyantForceGenerator : ForceGenerator
{
    private const float waterDensity = 997;
    private Vector3 attachPoint; // Water level

    public SpringBuoyantForceGenerator(Vector3 attachPoint)
    {
        this.attachPoint = attachPoint;
    }

    public void GetImpact(Particle particle)
    {
        Vector3 particleCorrectedPos = particle.transform.position;
        particleCorrectedPos.y = particle.transform.position.y - particle.Size / 2;

        if (particle.transform.position.y >= attachPoint.y)
            return;

        Vector3 localAttachPoint = particleCorrectedPos;
        localAttachPoint.y = attachPoint.y;

        float depth = (particleCorrectedPos - localAttachPoint).magnitude;
        depth = Mathf.Clamp(depth, 0, particle.Size);

        float submergedVolume = particle.Size * particle.Size * depth;
        float displacedWaterMass = submergedVolume * waterDensity;
        Vector3 force = displacedWaterMass * 9.8f * Vector3.up;

        particle.ApplyForce(force);
    }

    public Vector3 AttachPoint => attachPoint;

    public Vector3 HangPoint => attachPoint;
}