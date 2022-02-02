using UnityEngine;

public class ParticleCable : ParticleLink, ParticleContactGenerator
{
    public float lenght;
    public float restitution;
    
    public void AddContact(ParticleContact contact)
    {
        throw new System.NotImplementedException();
    }
}