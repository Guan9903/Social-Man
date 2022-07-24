using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CansRuin : MonoBehaviour
{
    public GameObject ruin;
    public float damage = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GameObject r = Instantiate(ruin, transform.position, Quaternion.identity) as GameObject;
            var system = r.GetComponent<ParticleSystem>();
            Destroy(r, system.main.startLifetime.constant);

            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().GetHurt(damage);
            }

            Destroy(gameObject);
        }

    }
}
