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
        Particle particleA = new Particle(1, Vector3.left);
        world.Add(particleA);

        Particle particleB = new Particle(1, Vector3.right);
        world.Add(particleB);

        ParticleCable cable = new ParticleCable(particleA, particleB, 5);
        world.contactGenerators.Add(cable);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("ParticleCableDemo.OnFire");
    }
}