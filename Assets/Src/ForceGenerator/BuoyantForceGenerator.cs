using System;
using UnityEngine;

public class BuoyantForceGenerator : ForceGenerator
{
    public const float WATER_DENSITY = 997;
    public const float GLYCERIN_DENSITY = 1260;
    public const float GEAR_OIL_DENSITY = 901;
    public const float CASTOR_OIL_DENSITY = 969;

    private float fluidDensity;

    public BuoyantForceGenerator(float fluidDensity)
    {
        this.fluidDensity = fluidDensity;
    }

    public void GetImpact(Particle particle)
    {
        if (particle.gameObject.transform.position.y > 0)
            return;

        // TODO: move to Particle class
        const double particleVolume = 1.1503e-6;

        float k = Convert.ToSingle((particleVolume * fluidDensity) * 9.8);
        particle.ApplyForce(Vector3.up * k);
    }
}