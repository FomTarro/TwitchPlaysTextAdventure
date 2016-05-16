using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StreamerInput : MonoBehaviour {

    [SerializeField]
    private TwitchIRC twitch;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private TextBody body;

    [SerializeField]
    PlayCommands loginCommands;
    [SerializeField]
    TwitchLogin login;

    bool loginIniatated = false;
    bool getUsername = false;

    public List<string> commandHistory;

    private int index = -1;

	// Use this for initialization
	void Start () {
        commandHistory.Add("");
	}
	
	// Update is called once per frame
	void Update () {

        inputField.Select();
        inputField.ActivateInputField();

        if (inputField.textComponent.text != "" && Input.GetKey(KeyCode.Return))
        {
            commandHistory.Add(inputField.textComponent.text);
            EnterCommand(inputField.textComponent.text);
            index = commandHistory.Count;
            //Debug.Log(index);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && index != -1)
        {
            index--;
            index = Mathf.Clamp(index, 0, commandHistory.Count - 1);
            Debug.Log(commandHistory[index]);
            inputField.text = commandHistory[index];
            inputField.MoveTextEnd(false);
            
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && index != -1)
        {
            index++;
            index = Mathf.Clamp(index, 0, commandHistory.Count - 1);
            Debug.Log(commandHistory[index]);
            inputField.text = commandHistory[index];
            inputField.MoveTextEnd(false);
            
        }
    }

    public void EnterCommand(string cmd)
    {
        
        Debug.Log(cmd);
        body.PrintToBody(">" + cmd + "\n");
        if (TwitchLogin.connected) {
            twitch.SendMsg(cmd);
        }
        else // hideous spaghetti code but it works offline!
        {
            if (cmd.ToLower().Equals("login") && !loginIniatated)
            {
                BeginLogin();
            }
            else if(loginIniatated)
            {
                Debug.Log("else");
                if (getUsername)
                {
                    login.username = cmd;
                    getUsername = false;
                    body.PrintToBody("Please provide an oAuth Token.\nIf you are unsure what an oAuth Token is, type 'OAUTH' for assistance.");
                }
                else if(inputField.text.ToLower().Contains("oauth:"))
                {
                    login.oauthKey = inputField.text.ToLower();
                    loginIniatated = false;
                    login.Submit();
                }
                else if (cmd.ToLower().Equals("oauth"))
                {
                    Application.OpenURL("http://www.twitchapps.com/tmi");
                }
                else
                {
                    body.PrintToBody("Invalid oAuth Token. Please enter a valid oAuth Token.");
                }
            }
        }
        inputField.text = "";
    }

    public void BeginLogin()
    {
        body.PrintToBody("Please provide your Twitch.tv username.");
        loginIniatated = true;
        getUsername = true;
    }

    public void CheckHidePassword()
    {
        if (inputField.text.ToLower().Contains("oauth:"))
        {
            inputField.contentType = InputField.ContentType.Password;
        }
        else
        {
            inputField.contentType = InputField.ContentType.Standard;
        }
    }
}
