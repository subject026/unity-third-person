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
        Vector3 m = new Vector3(move.x, 0, move.y) * Speed * Time.deltaTime;
        Vector3 cameraAngles = Camera.rotation.eulerAngles;        
        Quaternion rotation = Quaternion.Euler(0,cameraAngles.y,cameraAngles.z);
        Vector3 direction = rotation * m;
        Quaternion playerRotation = Quaternion.Euler(0,cameraAngles.y, 0);

        // Quaternion playerRotation = Quaternion.Euler(0, cameraAngles.y, 0);
        // float targetDir = direction - transform.position;
        // float difference = Vector3.Angle(targetDir, cameraAngles.y);

        // Where is it before the move?
        print(transform.position);
        // Where is it after the move?
        // What angle is it moving at compared to camera angle

        // Compare that to direction player is facing now

        Vector3 playerAngles = transform.rotation.eulerAngles;
        // print("transform.rotation: " + ); // current y

        // rotation should be a quaterion - original y angle changed to new y angle
        float angleY = rotation.eulerAngles.y;
        Quaternion newPlayerRotation = Quaternion.Euler(playerAngles.x, angleY, playerAngles.z); 
        // print(rotation.eulerAngles.y);

        // Quaternion YRot = Quaternion.AngleAxis(angleY, Vector3.up);
        // transform.Rotate(newPlayerRotation.eulerAngles, Space.Self);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 300 * Time.deltaTime);


        transform.Translate(direction, Space.Self);
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }
}
