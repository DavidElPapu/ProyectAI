using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class FirstPersonPlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    public Transform playerCamera;
    public float sensX;
    public float sensY;
    private float xRotation;
    private float yRotation;

    [Header("Movement")]
    public float walkSpeed;
    private Vector3 moveDirection;
    private float moveSpeed, horizontalInput, verticalInput;

    public Transform cameraTrasform, bodyTransform, canon;
    private Rigidbody rb;

    public GameObject bullet;
    private PlayerActions actions;
    public LayerMask defaultLayer;

    private void Awake()
    {
        
    }

    private void Start()
    {
        actions = new PlayerActions();
        actions.Gameplay.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        moveSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.linearDamping = 5f;
        rb.useGravity = false;
        //playerCamera.transform.parent = transform;
        playerCamera.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        actions.Gameplay.Shoot.performed += OnShoot;

    }

    private void Update()
    {
        UpdateCamera();
        PlayerInput();
        RotatePlayerBody();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        Vector2 input = actions.Gameplay.Move.ReadValue<Vector2>();
        //moveDirection = new Vector3(input.x, 0, input.y);
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    public Vector3 GetPlayerMoveDirection()
    {
        return new Vector3(horizontalInput, 0, verticalInput);
    }

    public Transform GetPlayerBodyTransform()
    {
        return transform;
    }

    private void MovePlayer()
    {
        moveDirection = bodyTransform.forward * verticalInput + bodyTransform.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void RotatePlayerBody()
    {
        bodyTransform.rotation = Quaternion.Euler(0, cameraTrasform.rotation.eulerAngles.y, 0);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void UpdateCamera()
    {
        Vector2 mouseInput = actions.Gameplay.MouseMove.ReadValue<Vector2>();
        float mouseX = mouseInput.x * Time.deltaTime * sensX;
        float mouseY = mouseInput.y * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerCamera.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        GameObject newBullet = Instantiate(bullet, canon.position, canon.rotation);
        if (newBullet.TryGetComponent(out Rigidbody brb))
        {
            brb.AddForce(newBullet.transform.forward * 15f, ForceMode.Impulse);
        }
    }

    public bool IsPlayerLookingAtMe(GameObject me)
    {
        if (Physics.Raycast(canon.position, canon.forward, out RaycastHit hit, 100f, defaultLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject == me)
                return true;
        }
        return false;
    }
}
