using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, PlayerT;
    public PlayerController Player;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        ControlCam();
    }

    void ControlCam(){
        mouseX += Player.rotate.x * RotationSpeed;
        Target.rotation = Quaternion.Euler(0, mouseX, 0);
        // Player.transform.rotation = Quaternion.Euler(0, mouseX, 0);

        transform.LookAt(Target);
    }
}
