using UnityEngine.InputSystem;

public interface IDemo
{
    void Init();
    
    void OnFire(InputAction.CallbackContext context);
}