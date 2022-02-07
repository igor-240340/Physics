using UnityEngine;
using UnityEngine.InputSystem;

public class AnchoredSpringDemo : IDemo
{
    private ParticleWorld world;
    private IParticleForceGenerator spring = new ParticleAnchoredSpring(Vector3.up, 10f, 1);

    public AnchoredSpringDemo(ParticleWorld world)
    {
        this.world = world;
    }

    public void Init()
    {
        Particle particle = new Particle(1, Vector3.zero);
        world.Add(particle);
        world.forceRegistry.Register(particle, spring);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}