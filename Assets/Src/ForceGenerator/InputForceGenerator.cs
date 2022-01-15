using UnityEngine;
using UnityEngine.InputSystem;

public class InputForceGenerator : ForceGenerator
{
    private float forceFactor;

    public InputForceGenerator(float forceFactor)
    {
        this.forceFactor = forceFactor;
    }

    public void GetImpact(Particle particle)
    {
        if (Keyboard.current.upArrowKey.isPressed)
            particle.ApplyForce(Vector3.up * forceFactor);

        if (Keyboard.current.leftArrowKey.isPressed)
            particle.ApplyForce(Vector3.left * forceFactor);

        if (Keyboard.current.rightArrowKey.isPressed)
            particle.ApplyForce(Vector3.right * forceFactor);
    }
}