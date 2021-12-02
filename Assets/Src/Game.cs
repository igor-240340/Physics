using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private GameObject particle;

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
        if (context.action.phase == InputActionPhase.Canceled)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            particle = Instantiate(particlePrefab, mouseWorldPos, Quaternion.identity);
        }
    }
}