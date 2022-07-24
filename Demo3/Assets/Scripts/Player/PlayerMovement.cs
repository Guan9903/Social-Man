using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    //public float sneakSpeed;

    public Rigidbody2D playerBodyRb;
    public Camera cam;
    public float rotateSpeed;

    Vector2 movement;
    Vector2 mousePos;

    bool isLShiftDown;
    bool isLCtrlDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isLShiftDown = true;
        }
        else
            isLShiftDown = false;

    }

    private void FixedUpdate()
    {
        if (isLShiftDown)
        {
            Run();
        }
        else
            Walk();

        Rotate();
    }

    void Walk()
    {
        playerBodyRb.MovePosition(playerBodyRb.position + movement * walkSpeed * Time.fixedDeltaTime);
    }

    void Run()
    {
        playerBodyRb.MovePosition(playerBodyRb.position + movement * runSpeed * Time.fixedDeltaTime);
    }

    //void Sneak()
    //{
    //    rb.MovePosition(rb.position + movement * sneakSpeed * Time.fixedDeltaTime);
    //}

    void Rotate()
    {
        Vector2 lookDir = mousePos - playerBodyRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerBodyRb.rotation = angle;
    }
}
