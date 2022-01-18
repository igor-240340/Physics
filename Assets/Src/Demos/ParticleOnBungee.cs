using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleOnBungee : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    
    private Vector3 firstMouseWorldPos;

    private ForceGenerator bungee;
    private ForceGenerator gravity;

    private bool cannotCreateAnymore = false;

    private void Awake()
    {
        bungee = new BungeeGenerator(72.1f, 1.5f, Vector3.zero);
        gravity = new GravityForceGenerator();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
            firstMouseWorldPos = GetMouseWorldPos();

        if (context.action.phase == InputActionPhase.Canceled && !cannotCreateAnymore)
        {
            Vector3 pos = ((BungeeGenerator) bungee).HangPoint + new Vector3(-2f, 0.5f, 0);
            GameObject particle = Instantiate(particlePrefab, pos, Quaternion.identity);
            particle.GetComponent<Particle>().Mass = 1f;
            particle.GetComponent<Particle>().AddForceGenerator(bungee);
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