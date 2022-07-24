using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Image progressBar;

    bool canLoad;

    // Start is called before the first frame update
    void Start()
    {
        canLoad = false;
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync("Game");

        while (gameScene.progress < 1)
        {
            progressBar.fillAmount = gameScene.progress;
            canLoad = true;
            yield return new WaitForEndOfFrame();
        }

    }
}
