using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject football;
    public Rigidbody2D playerRb;
    public Camera cam;
    public float speed;

    // 设置两次枪击的间隔时间
    public float fireRate = 0.25f;

    // 设置足球为物体带来的冲击力
    public float hitForce = 100f;

    // 玩家上次射击后的间隔时间
    float nextFire;

    Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }

    void Shoot()
    {
        Vector2 shootDir = mousePos - playerRb.position;
        nextFire += Time.fixedDeltaTime;
        if (Input.GetMouseButton(0) && nextFire > fireRate)
        {
            nextFire = 0;
            GameObject fb = Instantiate(football, shootPoint.position, Quaternion.identity) as GameObject;
            //fb.GetComponent<Rigidbody2D>().MovePosition(shootPoint.position + transform.forward * speed * Time.deltaTime);
            fb.GetComponent<Rigidbody2D>().velocity = shootDir * speed;
            Destroy(fb, 1.5f);
        }
    }
}
