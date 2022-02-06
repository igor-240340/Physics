using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleShotDemo : IDemo
{
    private ParticleWorld world;
    private Vector3 prevMousePos;

    public ParticleShotDemo(ParticleWorld world)
    {
        this.world = world;
    }

    public void Init()
    {
        Debug.Log("ParticleShotDemo.Init");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
            prevMousePos = Utils.GetMouseWorldPos();
        else if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector3 forceToApply = (prevMousePos - Utils.GetMouseWorldPos()) * 100;

            Particle particle = new Particle(1, prevMousePos);
            particle.ApplyForce(forceToApply);

            world.Add(particle);
        }
    }
}