public abstract class ParticleLink
{
    public Particle particleA, particleB;

    protected ParticleLink(Particle particleA, Particle particleB)
    {
        this.particleA = particleA;
        this.particleB = particleB;
    }

    protected float CurrentLength()
    {
        return (particleA.pos - particleB.pos).magnitude;
    }
}