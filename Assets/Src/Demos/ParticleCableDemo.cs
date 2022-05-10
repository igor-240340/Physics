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

        Particle particleA = new Particle(1.35f, new Vector3(-1.5f, 3f));
        particleA.invMass = 0.0f;
        world.Add(particleA);
        
        Particle particleB = new Particle(1.35f, new Vector3(1.3f, 1.3f));
        particleB.invMass = 0.0f;
        world.Add(particleB);

        Particle particleC = new Particle(2.7f, new Vector3(2, 0));
        // particleB.velocity = new Vector3(0.2f, 0.4f);
        world.Add(particleC);

        ParticleCable cableAC = new ParticleCable(particleA, particleC, 5, 0.3f);
        ParticleCable cableBC = new ParticleCable(particleB, particleC, 5, 0.3f);
        world.contactGenerators.Add(cableAC);
        world.contactGenerators.Add(cableBC);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}