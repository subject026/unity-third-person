using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions controls; 
    // Start is called before the first frame update
    Vector2 move;
    public Vector2 rotate;
    float Speed;
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
        if (move.x > 0 && move.x < 0.5) {
            Speed = 5;
        }
        if (move.x >= 0.5) {
            Speed = 10;
        }
        // print("move.x: " + move.x);
        // print("move.y: " + move.y);

        Vector3 m = new Vector3(move.x, 0, move.y) * Speed * Time.deltaTime;
        transform.Translate(m, Space.Self); // relative to world space;
    }

    void OnEnable() {
        controls.Enable();
    }

    void OnDisable() {
        controls.Disable();
    }
}
