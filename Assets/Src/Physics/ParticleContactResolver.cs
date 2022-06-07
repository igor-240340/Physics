using System.Collections.Generic;

public class ParticleContactResolver
{
    public void ResolveContacts(List<ParticleContact> contacts)
    {
        int currentPlotIndex = 1;
        int contactIndex = 0;
        foreach (var contact in contacts)
        {
            MyPlot.SubPlot(3, contacts.Count, contacts.Count + currentPlotIndex++);
            
            // The whole system and the contact on the moment of its detection
            MatplotHelper.DrawPosAfterIntegrating();
            MatplotHelper.DrawPreservedContact(contactIndex++);
            
            // The whole system and the contact on the moment of its handling
            MatplotHelper.DrawParticles();
            MatplotHelper.DrawContact(contact, "black");
            
            contact.Resolve();
            
            MatplotHelper.DrawParticles("green");
            MatplotHelper.DrawContact(contact, "green", true);
        }
    }
}