using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NewPlayerInputs : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] float playerSpeed;

    [Header("Camera Variables")]
    [SerializeField] CinemachineCamera followCamera;
    [SerializeField] CinemachineCamera aimCamera;
    public bool isAiming { private set; get; }


    Rigidbody rb;
    Vector2 inputVector;
    Vector3 movementVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            aimCamera.Priority = 1;
            followCamera.Priority = 0;
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
        rb.linearVelocity = movementVector * playerSpeed;
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
