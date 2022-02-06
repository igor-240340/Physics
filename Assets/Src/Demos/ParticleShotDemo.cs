using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleShotDemo : Demo
{
    private ParticleWorld world;
    private Vector3 prevMousePos;

    public ParticleShotDemo(ParticleWorld world)
    {
        this.world = world;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
            prevMousePos = Utils.GetMouseWorldPos();
        else if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector3 forceToApply = (prevMousePos - Utils.GetMouseWorldPos()) * 100;
            
            Particle particle = new Particle();
            particle.SetMass(1);
            particle.SetPosition(prevMousePos);
            particle.ApplyForce(forceToApply);
            
            world.Add(particle);
        }
    }
}