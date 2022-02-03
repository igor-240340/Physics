using UnityEngine;
using UnityEngine.InputSystem;

public class Utils
{
    public static Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // Drops z-coordinate of the camera offset
        return mouseWorldPos;
    }
}