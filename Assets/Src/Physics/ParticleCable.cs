public class ParticleCable : ParticleLink, IParticleContactGenerator
{
    public float restitution;
    public float maxLength;

    public ParticleCable(Particle particleA, Particle particleB, float maxLength) : base(particleA, particleB)
    {
        this.maxLength = maxLength;
    }

    public bool GenerateContact(out ParticleContact contact)
    {
        contact = null;
        return contact is not null;
    }
}