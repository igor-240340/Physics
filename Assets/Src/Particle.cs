using UnityEngine;

public class Particle
{
    public Vector3 position;
    public Vector3 force;
    public Vector3 velocity;
    public float invMass;

    private float mass;

    public void SetMass(float mass)
    {
        this.mass = mass;
        invMass = 1 / mass;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public void ApplyForce(Vector3 force)
    {
        this.force += force;
    }
}