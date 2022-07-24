using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject footballPrefab;
    //public AudioSource shootSound;

    public float shootForce;
    public float fireRate = 0.25f;

    float nextFire;

    private void FixedUpdate()
    {
        Shoot();
    }

    void Shoot()
    {
        nextFire += Time.fixedDeltaTime;
        //nextFire += Time.deltaTime;
        if (Input.GetMouseButton(0) && nextFire > fireRate)
        {
            //shootSound.Play();
            FindObjectOfType<AudioManager>().Play("Shoot");
            GameObject bullet = Instantiate(footballPrefab, shootPoint.position, shootPoint.rotation) as GameObject;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.up * shootForce, ForceMode2D.Impulse);

            Destroy(bullet, 1.5f);
            nextFire = 0;

            CameraShake.StartCameraShake(0);
        }
    }
}
