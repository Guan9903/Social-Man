using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitCRT : MonoBehaviour
{
    public Material effectMat;
    public float smoothRefresh;
    public float smoothDistort;
    public float interval;

    float refreshP;
    float distortion;
    float sD;

    // Start is called before the first frame update
    void Start()
    {
        refreshP = 1080.0f;
        sD = smoothDistort;
        StartCoroutine(Distort());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        effectMat.SetFloat("_ScanPoint", refreshP);
        effectMat.SetFloat("_Distort", distortion);

        if (effectMat != null)
            Graphics.Blit(source, destination, effectMat);
    }

    private void FixedUpdate()
    {
        refreshP = Mathf.MoveTowards(refreshP, -200.0f, smoothRefresh);
        if (refreshP <= -200.0f)
        {
            refreshP = 2000.0f;
        }
    }

    IEnumerator Distort()
    {
        float current = 0.0f;
        float target = NormalRandom(-interval, interval);
        distortion = current;
        while (true)
        {
            current = Mathf.MoveTowards(current, target, sD * Time.deltaTime);
            distortion = current;
            if (current == target)
            {
                yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
                target = NormalRandom(-interval, interval);
            }
            yield return null;
        }
    }

    float NormalRandom(float min, float max)
    {
        sD = smoothDistort * Random.Range(0.2f, 1.0f);
        return Random.Range(min, max);
    }
}
