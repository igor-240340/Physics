using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleShot : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private GameObject particle;

    private Vector3 firstMouseWorldPos;

    private ForceGenerator gravity;

    private void Awake()
    {
        gravity = new GravityForceGenerator();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            firstMouseWorldPos = GetMouseWorldPos();
        }

        if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector3 force = (firstMouseWorldPos - GetMouseWorldPos()) * 100;
            
            particle = Instantiate(particlePrefab, firstMouseWorldPos, Quaternion.identity);
            particle.GetComponent<Particle>().AddForceGenerator(gravity);
            particle.GetComponent<Particle>().ApplyForce(force);
            // particle.GetComponent<Particle>().AddForceGenerator(new InputForceGenerator(15));

            Debug.Log($"Start pos: {particle.transform.position.ToString("F5")}");
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos;
    }
}