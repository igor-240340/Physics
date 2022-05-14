using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleRodDemo : IDemo
{
    private ParticleWorld world;

    public ParticleRodDemo(ParticleWorld world)
    {
        this.world = world;
    }

    public void Init()
    {
        // world.SwitchOffGravity();
        
        Particle particleA = new Particle(1.35f, new Vector3(0.0f, 4.0f));
        particleA.invMass = 0.0f;
        world.Add(particleA);
        
        Particle particleB = new Particle(0.28f, new Vector3(3.0f, 2.0f));
        world.Add(particleB);

        ParticleCable cableAB = new ParticleCable(particleA, particleB, 5, 0.3f);
        world.contactGenerators.Add(cableAB);
        
        Particle particleC = new Particle(0.28f, new Vector3(0.0f, 0.0f));
        world.Add(particleC);
        
        ParticleRod rodBC = new ParticleRod(particleB, particleC, 3);
        world.contactGenerators.Add(rodBC);
        
        Particle particleD = new Particle(0.28f, new Vector3(2.0f, 0.0f));
        world.Add(particleD);
        
        ParticleRod rodBD = new ParticleRod(particleB, particleD, 4);
        world.contactGenerators.Add(rodBD);
        
        ParticleRod rodCD = new ParticleRod(particleC, particleD, 3);
        world.contactGenerators.Add(rodCD);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}