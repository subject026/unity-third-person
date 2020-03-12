using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions controls; 
    // Start is called before the first frame update
    Vector2 move;
    public Vector2 rotate;
    float Speed = 30;
    public Transform Camera;
    void Awake()
    {
        controls = new PlayerInputActions();

        controls.PlayerActions.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Move.canceled += ctx => move = Vector2.zero;

        controls.PlayerActions.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.PlayerActions.Rotate.canceled += ctx => rotate = Vector2.zero;

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
        Vector3 m = new Vector3(move.x, 0, move.y) * Speed * Time.deltaTime; // anaglog stick input
        Vector3 cameraAngles = Camera.rotation.eulerAngles; // angle camera is pointed at now        
        Quaternion rotation = Quaternion.Euler(0,cameraAngles.y,cameraAngles.z);
        Vector3 direction = rotation * m;        
        
        // !!! only rotate if stick isn't zero
        if (move.x > 0 || move.y > 0 || move.x < 0|| move.y > 0) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 300 * Time.deltaTime);
        }
        transform.Translate(direction, Space.World);
    }
}
