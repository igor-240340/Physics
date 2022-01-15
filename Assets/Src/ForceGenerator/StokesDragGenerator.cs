using System;
using UnityEngine;

public class StokesDragGenerator : ForceGenerator
{
    public void GetImpact(Particle particle)
    {
        if (particle.gameObject.transform.position.y > 0)
            return;

        Vector3 velocity = particle.Velocity;

        float fluidViscosity = 1.31f;
        float particleRadius = 0.0065f;

        float k = Convert.ToSingle(6 * Mathf.PI * particleRadius * fluidViscosity); // Stokes' law
        particle.ApplyForce(-velocity * k);
    }
}