using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Unity.Netcode;

public class NewPlayerInputs : NetworkBehaviour
{
    [Header("Player Variables")]
    [SerializeField] float playerSpeed;
    [SerializeField] Material[] playerColors;
    NetworkVariable<int> playerIndex = new NetworkVariable<int>();
    public static int playerCount = 0;

    [Header("Camera Variables")]
    [SerializeField] CinemachineCamera followCamera;
    [SerializeField] CinemachineCamera aimCamera;
    [SerializeField] Transform aimCameraTransform;
    Vector3 aimCameraFoward;
    Vector3 aimCameraRight;

    public bool isAiming { private set; get; }


    Rigidbody rb;
    Renderer myRenderer;
    Vector2 inputVector;
    Vector3 movementVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAiming = false;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playerIndex.Value = playerCount++;
        }

        if(myRenderer == null)
        {
            myRenderer = GetComponent<Renderer>();
        }

        myRenderer.material = playerColors[playerIndex.Value];

        //only enable camera for current player
        if(!IsOwner)
        {
            followCamera.enabled = false;
            aimCamera.enabled = false;
            return;
        }

        

        aimCamera.Priority = 0;
        followCamera.Priority = 1;

    }


    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            aimCamera.Priority = 1;
            followCamera.Priority = 0;


            //camera based movement
            aimCameraFoward = aimCameraTransform.forward;
            aimCameraFoward.y = 0;
            aimCameraFoward.Normalize();

            aimCameraRight = aimCameraTransform.right;
            aimCameraRight.y = 0;
            aimCameraRight.Normalize();

        }
        else
        {
            aimCamera.Priority = 0;
            followCamera.Priority = 1;
        }


        //rotate player on input
        if(movementVector.sqrMagnitude > 0.1 && !isAiming)
        {
            transform.forward = movementVector;
        }
    }

    private void FixedUpdate()
    {
        if (!isAiming)
        {
            rb.linearVelocity = movementVector * playerSpeed;
        }
        else
        {
            rb.linearVelocity = (aimCameraFoward * movementVector.z + aimCameraRight * movementVector.x).normalized * playerSpeed;
        }
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0 ,inputVector.y);
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
    }
}
