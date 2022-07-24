using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float damage;
    public float hurtRate;

    GameObject enemy;
    float nextHurt;

    // Start is called before the first frame update
    void Start()
    {
        nextHurt = hurtRate;
        enemy = gameObject;
        enemy.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        nextHurt += Time.fixedDeltaTime;

        if (collision.gameObject.tag == "Player" && nextHurt > hurtRate)
        {
            collision.gameObject.GetComponent<PlayerHealth>().GetHurt(damage);
            nextHurt = 0;
        }
    }
}
