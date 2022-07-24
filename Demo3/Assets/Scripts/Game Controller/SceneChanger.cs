using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    public Camera cam;
    public float transitionTime;

    //private int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ButtonController.isPlay)
        {
            FadeToScene();
            ButtonController.isPlay = false;
        }
    }

    public void OpenGlitch()
    {
        cam.GetComponent<GlitchEffect>().enabled = true;
    }

    public void CloseGlitch()
    {
        cam.GetComponent<GlitchEffect>().enabled = false;
    }

    public void FadeToScene()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        StartCoroutine(FadeTransition());
    }

    IEnumerator FadeTransition()
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Game");
    }
}
