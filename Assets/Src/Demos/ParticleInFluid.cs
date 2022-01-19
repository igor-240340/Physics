using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleInFluid : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private Vector3 firstMouseWorldPos;

    private ForceGenerator buoyantForce;
    private ForceGenerator gravityForce;
    private ForceGenerator stokesForce;

    private void Awake()
    {
        buoyantForce = new BuoyantForceGenerator(BuoyantForceGenerator.CASTOR_OIL_DENSITY);
        gravityForce = new GravityForceGenerator();
        stokesForce = new StokesDragGenerator();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
            firstMouseWorldPos = GetMouseWorldPos();

        if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector3 force = (firstMouseWorldPos - GetMouseWorldPos()) * 2f;

            GameObject particle = Instantiate(particlePrefab, firstMouseWorldPos, Quaternion.identity);
            particle.GetComponent<Particle>().Mass = 0.00835f;
            particle.GetComponent<Particle>().AddForceGenerator(buoyantForce);
            particle.GetComponent<Particle>().AddForceGenerator(gravityForce);
            particle.GetComponent<Particle>().AddForceGenerator(stokesForce);
            particle.GetComponent<Particle>().ApplyForce(force);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos;
    }
}