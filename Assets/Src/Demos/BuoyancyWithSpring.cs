using UnityEngine;
using UnityEngine.InputSystem;

public class BuoyancyWithSpring : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private ForceGenerator buoyancy;
    private ForceGenerator gravity;

    private void Awake()
    {
        buoyancy = new SpringBuoyantForceGenerator(Vector3.zero);
        gravity = new GravityForceGenerator();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Canceled)
        {
            GameObject particle = Instantiate(particlePrefab, GetMouseWorldPos(), Quaternion.identity);
            particle.GetComponent<Particle>().Mass = 995;
            particle.GetComponent<Particle>().Size = 1f;
            particle.GetComponent<Particle>().AddForceGenerator(buoyancy);
            particle.GetComponent<Particle>().AddForceGenerator(gravity);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos;
    }
}