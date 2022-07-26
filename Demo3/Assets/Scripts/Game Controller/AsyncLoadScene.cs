﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoadScene : MonoBehaviour
{
    public Slider loadingSlider;            //进度条
    public Text loadingText;                //文字显示加载进度

    private AsyncOperation operation;       //场景加载类的对象
    private float SliderLoadSpeed = 1;      //进度条的行进速度
    private float targetValue;              //加载进度(由于加载进度不能为 1，所以需要此变量在加载进度大于某一个值时让加载进度变为1)

    // Start is called before the first frame update
    void Start()
    {
        // 初始化进度条
        loadingSlider.value = 0.0f;

        if (SceneManager.GetActiveScene().name == "NowSence")
        {
            // 启动协程
            StartCoroutine(AsyncLoading());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // operation.progress 加载进度
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            // operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value)
        {
            // 插值运算（进度条向当前加载进度趋近）
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * SliderLoadSpeed);
            // 避免插值运算一直进行
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        // 显示加载百分比
        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        // 当进度条完成 100% 时，加载场景
        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
    }

    // 加载场景的协程
    IEnumerator AsyncLoading()
    {
        // 异步加载场景
        operation = SceneManager.LoadSceneAsync("Game");
        // 阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }
}
