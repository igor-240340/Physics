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

    private void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            firstMouseWorldPos = GetMouseWorldPos();
        }

        if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector3 appliedForce = (firstMouseWorldPos - GetMouseWorldPos()) * 100;
            Debug.Log($"Applied force: {appliedForce.ToString("F5")}");
            Debug.Log($"Start pos: {firstMouseWorldPos.ToString("F5")}");
            
            particle = Instantiate(particlePrefab, firstMouseWorldPos, Quaternion.identity);
            particle.GetComponent<Particle>().ApplyForce(appliedForce);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return mouseWorldPos;
    }
}