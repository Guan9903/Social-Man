using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyId;
    public float enemyHealth;
    //public AudioSource hurtSound;

    public static int score;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            switch(enemyId)
            {
                case 1:
                    score = 100;
                    LeaderboardUI.playerScore += score;
                    break;
                case 2:
                    score = 200;
                    LeaderboardUI.playerScore += score;
                    break;
                case 3:
                    score = 300;
                    LeaderboardUI.playerScore += score;
                    break;
            }

            Destroy(gameObject);
        }
    }

    public void GetHurt(float damage)
    {
        //hurtSound.Play();
        FindObjectOfType<AudioManager>().Play("EnemyHurt");
        enemyHealth -= damage;
        if (enemyHealth <= 0)
            isDead = true;

    }
}
