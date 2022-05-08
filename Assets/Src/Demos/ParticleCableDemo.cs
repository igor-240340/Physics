using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleCableDemo : IDemo
{
    private ParticleWorld world;

    public ParticleCableDemo(ParticleWorld world)
    {
        this.world = world;
    }

    public void Init()
    {
        // world.SwitchOffGravity();

        Particle particleA = new Particle(1.35f, new Vector3(5, -5));
        particleA.invMass = 0.0f;
        particleA.velocity = new Vector3(-0.7f, 0.3f);
        world.Add(particleA);

        Particle particleB = new Particle(2.7f, new Vector3(2, -2));
        particleB.velocity = new Vector3(0.2f, 0.4f);
        world.Add(particleB);

        ParticleCable cable = new ParticleCable(particleA, particleB, 2, 0.3f);
        world.contactGenerators.Add(cable);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}