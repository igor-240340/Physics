using UnityEngine;

public class Particle
{
    public Vector3 pos;
    public Vector3 force;
    public Vector3 velocity;
    public float invMass;

    private float mass;

    public Particle(float mass, Vector3 pos)
    {
        SetMass(mass);
        SetPosition(pos);
    }
    
    public void SetMass(float mass)
    {
        this.mass = mass;
        invMass = 1 / mass;
    }

    public void SetPosition(Vector3 pos)
    {
        this.pos = pos;
    }

    public void ApplyForce(Vector3 force)
    {
        this.force += force;
    }
}