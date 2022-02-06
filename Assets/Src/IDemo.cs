using UnityEngine.InputSystem;

public interface IDemo
{
    void OnFire(InputAction.CallbackContext context);

    void Init();
}