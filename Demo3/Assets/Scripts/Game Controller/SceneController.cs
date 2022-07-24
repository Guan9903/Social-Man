using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Camera cam;
    public GameObject gameStopPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    public Text tipsText;

    [SerializeField] public static bool sceneLoad;
    bool gamePlaying;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //gameStopPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);

        gamePlaying = true;
        sceneLoad = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePlaying)
            {
                Time.timeScale = 0;
                cam.GetComponent<CameraShake>().enabled = false;
                FindObjectOfType<AudioManager>().Pause("Theme");
                tipsText.text = "ESC继续";
                gamePlaying = false;
            }
            else if (!gamePlaying)
            {
                Time.timeScale = 1;
                cam.GetComponent<CameraShake>().enabled = true;
                FindObjectOfType<AudioManager>().Play("Theme");
                tipsText.text = "ESC暂停";
                gamePlaying = true;
            }
        }

        if (PlayerHealth.isDead)
        {
            //Time.timeScale = 0;
            //cam.GetComponent<CameraShake>().enabled = false;
            cam.GetComponent<GlitchEffect>().enabled = true;
            StartCoroutine(GameOverAppear());
        }
        else if (WaveSpawner.endGame)
        {
            cam.GetComponent<GlitchEffect>().enabled = true;
            StartCoroutine(GameWinAppear());
        }
    }

    IEnumerator GameOverAppear()
    {
        yield return new WaitForSeconds(2f);
        cam.GetComponent<GlitchEffect>().enabled = false;
        cam.GetComponent<CameraShake>().enabled = false;
        gameOverPanel.SetActive(true);
    }

    IEnumerator GameWinAppear()
    {
        yield return new WaitForSeconds(2f);
        cam.GetComponent<GlitchEffect>().enabled = false;
        cam.GetComponent<CameraShake>().enabled = false;
        gameWinPanel.SetActive(true);
    }

}
