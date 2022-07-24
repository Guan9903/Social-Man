using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public string path = "http://127.0.0.1/Unity/RankingUpdate.php";
    public string downloadPath = "http://127.0.0.1/Unity/RankingDownload.php";

    public string name;
    public string score;

    public Text playerName;
    public static int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("PlayerID"));
        if (PlayerPrefs.GetString("PlayerID") == "")
        {
            playerName.text = "测试用";
        }
        else
            playerName.text =  "热烈欢迎" + PlayerPrefs.GetString("PlayerID") + "同志来遭受社会毒打";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        name = PlayerPrefs.GetString("PlayerID");
        score = playerScore.ToString();

        if (GUILayout.Button("Update"))
        {
            StartCoroutine("ScoreUpdate");
        }

        if (GUILayout.Button("Download"))
        {
            StartCoroutine("ScoreDownload");
        }
    }


    public IEnumerator ScoreUpdate()
    {
        WWWForm form = new WWWForm();

        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("name", name);
        data.Add("score", score);

        foreach (KeyValuePair<string, string> post in data)
        {
            form.AddField(post.Key, post.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(path, form);

        yield return www.SendWebRequest();

        string returnmesg = www.downloadHandler.text;
        Debug.Log(returnmesg);

    }

    public IEnumerator ScoreDownload()
    {
        WWWForm form = new WWWForm();
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("download", "1");

        foreach (KeyValuePair<string, string> post in data)
        {
            form.AddField(post.Key, post.Value);
        }

        UnityWebRequest www = UnityWebRequest.Post(downloadPath, form);

        yield return www.SendWebRequest();

        string returnMesg = www.downloadHandler.text;

        Debug.Log(returnMesg);
    }

}

