using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public AnimationCurve curve;
    //public Sprite hitCursor;

    public Vector2 initSize;
    public Vector2 fireSize;
    public float roateSpeed;
    //public float smoothTime;

    SpriteRenderer rend;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        attacking = false;
        transform.localScale = initSize;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;

        if (Input.GetMouseButton(0))
        {
            transform.localScale = Vector3.one * fireSize;
        }
        else if(Input.GetMouseButtonUp(0))
            transform.localScale = initSize;

        CursorRotate();
    }

    void CursorRotate()
    {
        if (!attacking)
        {
            transform.Rotate(Vector3.forward * roateSpeed);
        }
        else
            transform.Rotate(Vector3.forward * roateSpeed / 2f);

    }
}
