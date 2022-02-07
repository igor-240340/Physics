using System.Collections.Generic;

public class ParticleContactResolver
{
    public void ResolveContacts(List<ParticleContact> contacts)
    {
        contacts.ForEach(contact => contact.resolve());
    }
}