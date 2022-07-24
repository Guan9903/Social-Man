using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform playerTransform;
    public float duration;      //持续时间
    public float amplitude;     //幅度
    public float feedbackAmplitude;

    static bool shootingStartShake = false;
    static bool shootingStarted = false;

    static bool hurtStartShake = false;
    static bool hurtStarted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneController.sceneLoad)
        //{
        //    StopAllCoroutines();
        //}
    }

    private void FixedUpdate()
    {
        //ShootingShake();
        //HurtShake();
    }

    private void LateUpdate()
    {
        ShootingShake();
        HurtShake();
    }

    public static void StartCameraShake(int shakeType)
    {
        switch (shakeType)
        {
            case 0:
                shootingStartShake = true;
                shootingStarted = true;
                break;
            case 1:
                hurtStartShake = true;
                hurtStarted = true;
                break;
        }
        
    }

    void ShootingShake()
    {
        if (shootingStartShake)
        {
            transform.position += Random.insideUnitSphere * amplitude;
        }
        if (shootingStarted)
        {
            StartCoroutine(WaitForSecondShooting(duration));
            shootingStarted = false;
        }
    }

    void HurtShake()
    {
        if (hurtStartShake)
        {
            transform.position += Random.insideUnitSphere * amplitude * 2f;
            gameObject.GetComponent<CRTPostProcess>().InjuryFeedback(feedbackAmplitude);
        }
        if (hurtStarted)
        {
            StartCoroutine(WaitForSecondHurt(duration));
            hurtStarted = false;
        }
    }

    IEnumerator WaitForSecondShooting(float t)
    {
        yield return new WaitForSeconds(t);
        shootingStartShake = false;
        transform.position = transform.position;
    }

    IEnumerator WaitForSecondHurt(float t)
    {
        yield return new WaitForSeconds(t);
        hurtStartShake = false;
        gameObject.GetComponent<CRTPostProcess>().InitRgb();
    }

}
