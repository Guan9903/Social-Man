using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Sprite[] playerStates;
    public GameObject scrapParticle;  

    public Image hpImage;
    public Image hpEffectImage;

    public float playerHealth;
    public float hurtSpeed = 0.005f;
    public static bool isDead;

    float hp;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        hp = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpImage.fillAmount = hp / playerHealth;

        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }

    }

    public void GetHurt(float damage)
    {
        hp -= damage;
        CameraShake.StartCameraShake(1);
        switch ((int)(playerHealth / hp + 0.5f))
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = playerStates[0];
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = playerStates[1];
                EnableParticle();
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = playerStates[2];
                EnableParticle();
                break;
        }
        if (hp <= 0)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = playerStates[4];
            isDead = true;
        }
    }

    private void EnableParticle()
    {
        GameObject s = Instantiate(scrapParticle, transform.position, Quaternion.identity) as GameObject;
        var system = s.GetComponent<ParticleSystem>();
        Destroy(s, system.main.startLifetime.constant);
    }

}
