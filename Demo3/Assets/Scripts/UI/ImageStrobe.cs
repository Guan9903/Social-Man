using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageStrobe : MonoBehaviour
{
    public float interval;           //闪烁的间隔时间，在Unity中修改

    float temp;
    bool isDisplay = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Effect();
    }

    public void Effect()
    {
        temp += Time.deltaTime;
        if (temp >= interval)
        {
            if(isDisplay)
            {
                gameObject.GetComponent<Image>().enabled = false;
                isDisplay = false;
                temp = 0;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = true;
                isDisplay = true;
                temp = 0;
            }
        }
    }

}
