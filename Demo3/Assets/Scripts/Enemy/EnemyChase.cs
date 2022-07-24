using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform playerTarget;
    public Rigidbody2D enemyBodyRb;

    public float defaultSpeed;
    public float closeSpeed;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
        Face2Player();
    }

    void Face2Player()
    {
        Vector2 lookDir = playerTarget.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        enemyBodyRb.rotation = angle;
    }

    void Chase()
    {
        movement = playerTarget.position - transform.position;
        enemyBodyRb.MovePosition(enemyBodyRb.position + movement * defaultSpeed * Time.deltaTime);
    }
}
