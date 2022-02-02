using UnityEngine;

public class Particle
{
    public Vector3 position;
    
    private float mass;
    private float invMass;
    
    public void SetMass(float mass)
    {
        this.mass = mass;
        invMass = 1 / mass;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }
}