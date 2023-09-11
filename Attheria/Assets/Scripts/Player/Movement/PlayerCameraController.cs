using Cinemachine;
using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : NetworkBehaviour
{
    public CameraType CameraType;

    [SerializeField] private PlayerInputManager playerInputManager;

    [SerializeField] private float turnSpeed;

    [SerializeField] private CinemachineFreeLook freeLookCamera;

    [SerializeField] private Transform cameraLookAt;
    [SerializeField] private Transform body;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform orientation;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public float horizontal;
    public float vertical;

    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject thirdPersonLockCamera;
    [SerializeField] private GameObject thirdPersonFreeCamera;

    [SerializeField] private GameObject ThirdPerson;
    [SerializeField] private GameObject FirstPerson;
    private void Start()
    {
        if (!isLocalPlayer) return;

        playerCam.SetActive(true);
        thirdPersonLockCamera.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInputManager.PlayerInput.Camera.ToggleView.performed += toggleCameraView;
        playerInputManager.PlayerInput.Camera.ToggleMode.performed += toggleCameraMode;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (MenuManager.Instance.Opened) return;

        xAxis.m_InputAxisValue = playerInputManager.PlayerInput.Camera.Look.ReadValue<Vector2>().x;
        yAxis.m_InputAxisValue = playerInputManager.PlayerInput.Camera.Look.ReadValue<Vector2>().y;

        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        firstPersonCamera.transform.rotation = Quaternion.Euler(new Vector3(cameraLookAt.eulerAngles.x, cameraLookAt.eulerAngles.y, 0));

        switch (CameraType)
        {
            case CameraType.FirstPerson:
                rotateFirstPersonCamera();
                break;
            case CameraType.ThirdPersonLocked:
                rotateLockCamera();
                break;
            case CameraType.ThirdPersonFree:
                rotateFreeCamera();
                break;
        }
    }

    void rotateFirstPersonCamera()
    {
        orientation.rotation = Quaternion.Euler(new Vector3(0, playerCamera.transform.rotation.eulerAngles.y, 0));
    }

    void rotateFreeCamera()
    {
        orientation.rotation = Quaternion.Euler(new Vector3(0, playerCamera.transform.rotation.eulerAngles.y, 0));

        float horizontalInput = playerInputManager.PlayerInput.PlayerMovement.Movement.ReadValue<Vector2>().x;
        float verticalInput = playerInputManager.PlayerInput.PlayerMovement.Movement.ReadValue<Vector2>().y;
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero) body.forward = Vector3.Slerp(body.forward, inputDir.normalized, Time.deltaTime * turnSpeed);
    }

    void rotateLockCamera()
    {
        body.rotation = Quaternion.Euler(new Vector3(body.rotation.x, playerCamera.transform.rotation.eulerAngles.y, body.rotation.z));
        orientation.rotation = Quaternion.Euler(new Vector3(0, playerCamera.transform.rotation.eulerAngles.y, 0));
    }

    void toggleCameraView(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        ChangeCameraType(context.ReadValue<float>() > 0 ? CameraType.FirstPerson : CameraType.ThirdPersonLocked);
    }

    void toggleCameraMode(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (MenuManager.Instance.Opened) return;

        switch (CameraType)
        {
            case CameraType.ThirdPersonFree:
                ChangeCameraType(CameraType.ThirdPersonLocked);
                break;
            case CameraType.ThirdPersonLocked:
                ChangeCameraType(CameraType.ThirdPersonFree);
                break;
            case CameraType.FirstPerson:
                break;
        }
    }

    public void ChangeCameraType(CameraType type)
    {
        if (MenuManager.Instance.Opened) return;
        CameraType = type; 

        firstPersonCamera.SetActive(false);
        thirdPersonLockCamera.SetActive(false);
        thirdPersonFreeCamera.SetActive(false);

        ThirdPerson.SetActive(false);
        FirstPerson.SetActive(false);

        switch (type)
        {
            case CameraType.FirstPerson:
                firstPersonCamera.SetActive(true);
                FirstPerson.SetActive(true);
                break;
            case CameraType.ThirdPersonLocked:
                thirdPersonLockCamera.SetActive(true);
                ThirdPerson.SetActive(true);
                break;
            case CameraType.ThirdPersonFree:
                thirdPersonFreeCamera.SetActive(true);
                ThirdPerson.SetActive(true);
                break;
        }
    }
}