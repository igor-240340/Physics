using System.Collections.Generic;

public class ParticleForceRegistry
{
    private struct ParticleForcePair
    {
        public Particle particle;
        public IParticleForceGenerator force;

        public ParticleForcePair(Particle particle, IParticleForceGenerator force)
        {
            this.particle = particle;
            this.force = force;
        }
    }

    private List<ParticleForcePair> pairs = new();

    public void Register(Particle particle, IParticleForceGenerator force)
    {
        pairs.Add(new ParticleForcePair(particle, force));
    }

    public void ApplyForces()
    {
        pairs.ForEach(pair =>
        {
            if (pair.particle.isPaused)
                return;
            
            pair.force.ApplyTo(pair.particle);
        });
    }

    public void Clear()
    {
        pairs.Clear();
    }

    public void Remove(Particle particle)
    {
        var pairsToRemove = new List<ParticleForcePair>();
        
        pairs.ForEach(pair =>
        {
            if (pair.particle == particle)
                pairsToRemove.Add(pair);
        });

        pairsToRemove.ForEach(pair => pairs.Remove(pair));
    }
}