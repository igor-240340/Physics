using UnityEngine;
using UnityEngine.InputSystem;

public class Utils
{
    // Returns a world space position of the mouse projected onto xy-plane
    public static Vector3 GetMouseWorldPosXY()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0; // Drops z-coordinate of the camera offset
        return mouseWorldPos;
    }

    public static Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    public static Vector2 GetMouseScreenPos()
    {
        return Mouse.current.position.ReadValue();
    }

    public static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(GetMouseScreenPos());
    }
}