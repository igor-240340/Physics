using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleOnSpring : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    
    private Vector3 firstMouseWorldPos;

    private ForceGenerator spring;
    private ForceGenerator gravity;

    private bool cannotCreateAnymore = false;

    private void Awake()
    {
        spring = new BasicSpringGenerator(72.1f, 1.5f, Vector3.zero);
        gravity = new GravityForceGenerator();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
            firstMouseWorldPos = GetMouseWorldPos();

        if (context.action.phase == InputActionPhase.Canceled && !cannotCreateAnymore)
        {
            Vector3 pos = ((BasicSpringGenerator) spring).HangPoint + new Vector3(0, 0.5f, 0);
            GameObject particle = Instantiate(particlePrefab, pos, Quaternion.identity);
            particle.GetComponent<Particle>().Mass = 1f;
            particle.GetComponent<Particle>().AddForceGenerator(spring);
            particle.GetComponent<Particle>().AddForceGenerator(gravity);

            cannotCreateAnymore = true;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos;
    }
}