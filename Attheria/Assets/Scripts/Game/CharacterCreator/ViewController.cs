using GolbyUtils;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private GameObject CharacterPrefab;
    [SerializeField] private Camera creationCamera;
    
    private PlayerInput Input;
    
    private Vector2 lastRotationMousePosition;
    private Vector2 lastMoveMousePosition;
    private Vector2 mouseSpeed;
    
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationLerp;
    private Vector3 targetRot;
    [SerializeField] private Transform rotationTransform;
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomLerp;
    private float targetZoom = 1;
    [Header("Move")]
    [SerializeField] private float dragSpeed = 5f;
    private Vector3 targetPos = new(0, 1, 0);
    [SerializeField] private float dragLerp;
    
    private bool isInView;

    private bool rotating;
    private bool moving;

    
    private void OnEnable()
    {
        Input = new();
        Input.CharacterCreator.Enable();
    }

    private void OnDisable()
    {
        Input!.CharacterCreator.Disable();
    }
        private void Update()
    {
        rotateCharacter();
        dragCharacter();
        zoomCharacter();
    }

    /// <summary>
    /// Rotates character view
    /// </summary>
    void rotateCharacter()
    {
        Vector2 currentMousePosition = Input.CharacterCreator.Point.ReadValue<Vector2>();
        Vector2 deltaMousePosition = currentMousePosition - lastRotationMousePosition;
        if (Input.CharacterCreator.Rotate.IsPressed())
        {
            if (isInView) rotating = true;

            if (rotating)
            {
                rotationTransform.Rotate(transform.up, Vector3.Dot(deltaMousePosition, creationCamera.transform.right) * rotationSpeed * Time.deltaTime, Space.World);
                rotationTransform.Rotate(creationCamera.transform.right, Vector3.Dot(deltaMousePosition, creationCamera.transform.up) * rotationSpeed * Time.deltaTime, Space.World);
                targetRot = rotationTransform.rotation.eulerAngles;
            }

        }
        else
        {
            rotating = false;
        }

        CharacterPrefab.transform.rotation = Quaternion.Lerp(CharacterPrefab.transform.rotation, Quaternion.Euler(targetRot), rotationLerp);
        CharacterPrefab.transform.eulerAngles = new Vector3(TransformUtil.ClampAngle(CharacterPrefab.transform.eulerAngles.x, -60, 60), CharacterPrefab.transform.eulerAngles.y, TransformUtil.ClampAngle(CharacterPrefab.transform.eulerAngles.z, -60, 60));
        lastRotationMousePosition = currentMousePosition;
    }

    /// <summary>
    /// Moves character view
    /// </summary>
    void dragCharacter()
    {
        Vector2 currentMousePosition = Input.CharacterCreator.Point.ReadValue<Vector2>();

        mouseSpeed = (currentMousePosition - lastMoveMousePosition) / Time.deltaTime;
        float moveX = -mouseSpeed.x * dragSpeed * Time.deltaTime;
        float moveY = mouseSpeed.y * dragSpeed * Time.deltaTime;

        if (Input.CharacterCreator.Drag.IsPressed())
        {
            if (isInView) moving = true;

            if (moving)
            {
                targetPos = new Vector3(CharacterPrefab.transform.position.x + moveX, CharacterPrefab.transform.position.y + moveY, 0f);
            }
        }
        else
        {
            moving = false;
        }

        CharacterPrefab.transform.position = Vector3.Lerp(CharacterPrefab.transform.position, targetPos, dragLerp);
        CharacterPrefab.transform.position = new Vector3(Mathf.Clamp(CharacterPrefab.transform.position.x, -1, 1), Mathf.Clamp(CharacterPrefab.transform.position.y, 0, 2), 0);

        lastMoveMousePosition = currentMousePosition;
    }

    /// <summary>
    /// Zooms character view
    /// </summary>
    void zoomCharacter()
    {
        float zoom = Input.CharacterCreator.Zoom.ReadValue<Vector2>().y;
        zoom = Mathf.Clamp(zoom, -.2f, .2f);
        if (zoom != 0) targetZoom = creationCamera.orthographicSize + -zoom;
        creationCamera.orthographicSize = Mathf.Lerp(creationCamera.orthographicSize, targetZoom, zoomLerp * zoomSpeed * Time.deltaTime);

        creationCamera.orthographicSize = Mathf.Clamp(creationCamera.orthographicSize, .2f, 2);
    }


    /// <summary>
    /// Resets view to default values
    /// </summary>
    public void ResetView()
    {
        creationCamera.orthographicSize = 1;
        CharacterPrefab.transform.eulerAngles = Vector3.zero;
        CharacterPrefab.transform.position = Vector3.up;

        targetPos = new(0, 1, 0);
        targetZoom = 1;
        targetRot = Vector3.zero;
        rotationTransform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Enable view handle when mouse is on screen
    /// </summary>
    public void OnPointerEnter() => isInView = true;

    /// <summary>
    /// Disable view handle when mouse is on screen
    /// </summary>
    public void OnPointerExit() => isInView = false;
}
