using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TwitchLogin : MonoBehaviour {

    //public InputField user;
    public string username;
    //public InputField oauth;
    public string oauthKey;

    //public PlayCommands generalCommands;

    TwitchIRC irc;

    public static bool connected;

    //public GameObject LoginPanel;

    [SerializeField]
    public TextBody body;


    IEnumerator Start()
    {
        yield return true;
        username = PlayerPrefs.GetString("user");
        oauthKey = PlayerPrefs.GetString("auth");
        irc = FindObjectOfType<TwitchIRC>();

        if (username.Length > 2)
            Submit();

    }

    public void Submit()
    {
        if (irc == null)
            Debug.LogError("No IRC client Found, make sure the \'TwitchPlays Client\' prefab is in the scene!");
        else
        {
            irc.Login(username, oauthKey);
            irc.Connected += Connected;
            StopCoroutine("reconnect");
            StartCoroutine("reconnect");
        }
    }

    void Connected()
    {
        connected = true;
        //body.PrintToBody("Successfuly connected to Twitch chat!");
        Debug.Log("Connected to Chat");
    }

    void Update()
    {
        if (connected)
        {
            PlayerPrefs.SetString("user", username);
            PlayerPrefs.SetString("auth", oauthKey);
            PlayerPrefs.Save();
            /*
            if(LoginPanel != null)
                LoginPanel.SetActive(false);
            */
        }

    }

    public void Disconnect()
    {
        connected = false;
        PlayerPrefs.DeleteKey("user");
        PlayerPrefs.DeleteKey("auth");
        StopCoroutine("disconnect");
        StartCoroutine("disconnect");
    }

    IEnumerator reconnect()
    {
        yield return new WaitForSeconds(1.0f);
        if (!connected)
        {
            Debug.Log("Failed to connect");
            body.PrintToBody("Unable to connect to Twitch chat.\nPlease attempt the 'LOGIN' process again.");
        }
        else
        {
            body.PrintToBody("Successfuly connected to Twitch chat!");
            irc.SendMsg("start");
        }
        yield return null;
    }

    IEnumerator disconnect()
    {
        //Application.LoadLevel(0);
        yield return new WaitForSeconds(1.0f);
        body.PrintToBody("Successfuly disconnected from Twitch chat!\nStored credentials have been cleared.\nTo resume, please 'LOGIN' to the terminal.");
        yield return null;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
