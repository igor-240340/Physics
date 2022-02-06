using System.Collections.Generic;

public class ParticleForceRegistry
{
    private struct ParticleForcePair
    {
        public Particle particle;
        public ParticleForceGenerator force;

        public ParticleForcePair(Particle particle, ParticleForceGenerator force)
        {
            this.particle = particle;
            this.force = force;
        }
    }

    private List<ParticleForcePair> pairs = new List<ParticleForcePair>();

    public void Register(Particle particle, ParticleForceGenerator force)
    {
        pairs.Add(new ParticleForcePair(particle, force));
    }

    public void ApplyForces()
    {
        pairs.ForEach(pair => pair.force.ApplyTo(pair.particle));
    }

    public void Clear()
    {
        pairs.Clear();
    }
}