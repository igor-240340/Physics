using UnityEngine;

public class GravityForceGenerator : ForceGenerator
{
    private float gravityAccel = -9.8f;

    public void GetImpact(Particle particle)
    {
        particle.ApplyForce(new Vector3(0, particle.Mass * gravityAccel));
    }
}