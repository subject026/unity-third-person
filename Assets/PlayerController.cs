using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions controls; 
    // Start is called before the first frame update
    Vector2 move;
    public Vector2 rotate;
    public float jumpingVelocity = 20;
    public float movingVelocity;
    public float Speed = 500;
    public Transform Camera;
    Rigidbody playerRigidbody;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        controls = new PlayerInputActions();

        controls.PlayerActions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Move.canceled += ctx => move = Vector2.zero;

        controls.PlayerActions.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Rotate.canceled += ctx => rotate = Vector2.zero;

        controls.PlayerActions.Jump.performed += ctx => HandleJump();

    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }

    void HandleMove() {
        Vector3 m = new Vector3(move.x, 0, move.y) * Speed * Time.deltaTime; // anaglog stick input * speed
        Vector3 cameraAngles = Camera.rotation.eulerAngles; // angle camera is pointed at now        
        Quaternion rotation = Quaternion.Euler(0,cameraAngles.y,cameraAngles.z);
        Vector3 direction = rotation * m;        
        
        //
        // velocity business
        

        // !!! only rotate if stick isn't zero
        if (m != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 300 * Time.deltaTime);
        }
        playerRigidbody.velocity = direction;
        
        // transform.Translate(direction, Space.World);
    }

    void HandleJump() {
        print("Jump!!");

        playerRigidbody.velocity = new Vector3(
            playerRigidbody.velocity.x,
            jumpingVelocity,
            playerRigidbody.velocity.z
        );
    }
}
