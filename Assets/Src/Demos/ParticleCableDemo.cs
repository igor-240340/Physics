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
        world.SwitchOffGravity();

        Particle particleA = new Particle(0.38f, new Vector3(-6.48f, -4.77f));
        particleA.velocity = new Vector3(3.20f, 3.21f);
        world.Add(particleA);

        Particle particleB = new Particle(0.19f, new Vector3(-8.49f, -4.69f));
        particleB.velocity = new Vector3(-1.29f, 1.30f);
        world.Add(particleB);

        ParticleCable cable = new ParticleCable(particleA, particleB, 5, 0.5f);
        world.contactGenerators.Add(cable);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}