using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //public GameObject blackFade;
    public static bool isPlay;
    public Camera cam;
    public InputField inputField;
    public Text errorMsg;

    private void Start()
    {
        isPlay = false;
        errorMsg.enabled = false;
        inputField.ActivateInputField();
        inputField.text = "";
    }

    public void Play()
    {
        //blackFade.SetActive(true);
        if (inputField.text != "")
        {
            errorMsg.enabled = false;
            PlayerPrefs.SetString("PlayerID", inputField.text);

            isPlay = true;
            cam.GetComponent<GlitchEffect>().enabled = true;
        }
        else
        {
            errorMsg.enabled = true;
        }

        //SceneManager.LoadScene("Game");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StoreId()
    {

        if (inputField.text != null)
        {
            Debug.Log(inputField.text);
            PlayerPrefs.SetString(inputField.text, "PlayerID");
        }

    }

}
