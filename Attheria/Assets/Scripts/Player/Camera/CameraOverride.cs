using Mirror;
using UnityEngine;

public class CameraOverride : NetworkBehaviour, Cinemachine.AxisState.IInputAxisProvider
{
    
    [SerializeField] private PlayerInputManager playerInputManager;
    
    public float GetAxisValue(int axis)
    {
        //if (!isLocalPlayer) return 0;
        if (MenuManager.Instance.Opened) return 0;
        
        if (axis == 0) {
            return playerInputManager.PlayerInput.Camera.Look.ReadValue<Vector2>().x;
        }
        else if (axis == 1)
        {
            return playerInputManager.PlayerInput.Camera.Look.ReadValue<Vector2>().y;
        }
        else
        {
            return 0;
        }
    }
}
