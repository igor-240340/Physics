using UnityEngine;
using UnityEngine.InputSystem;

public class CircleRodDemo : IDemo
{
    private ParticleWorld world;
    
    public CircleRodDemo(ParticleWorld world)
    {
        this.world = world;
    }
    
    public void Init()
    {
        Particle anchor = new Particle(1.35f, new Vector3(0.0f, 4.0f));
        anchor.invMass = 0.0f;
        world.Add(anchor);

        Particle centerParticle = new Particle(1.35f, new Vector3(0.0f, 0.0f));
        centerParticle.invMass = 0.0f;
        world.Add(centerParticle);

        const float radius = 2;
        const int sectorsCount = 8;
        const float angleStep = Mathf.PI * 2 / sectorsCount;
        float chordLen = Mathf.Sin(Mathf.PI / sectorsCount) * radius * 2;
        float angle = 0;
        Particle prevParticle = null;
        for (int i = 0; i < sectorsCount; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            Particle particle = new Particle(1.35f, new Vector3(x, y));
            world.Add(particle);

            ParticleRod radiusRod = new ParticleRod(centerParticle, particle, radius);
            world.contactGenerators.Add(radiusRod);

            if (prevParticle != null)
            {
                ParticleRod chordRod = new ParticleRod(particle, prevParticle, chordLen);
                world.contactGenerators.Add(chordRod);
            }

            prevParticle = particle;
            angle += angleStep;
        }
        
        ParticleRod c = new ParticleRod(world.particles[2], prevParticle, chordLen);
        world.contactGenerators.Add(c);
        
        ParticleCable cable = new ParticleCable(anchor, centerParticle, 2, 0.0f);
        world.contactGenerators.Add(cable);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}