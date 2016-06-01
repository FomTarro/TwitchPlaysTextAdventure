using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(InputField))]
public class StreamerInput : MonoBehaviour {

    [SerializeField]
    private TwitchIRC _twitch;
    private InputField _inputField;
    [SerializeField]
    private TextBody _body;

    [SerializeField]
    TwitchLogin _login;

    bool _loginIniatated = false;
    bool _getUsername = false;

    public List<string> _commandHistory;

    private int index = -1;

	// Use this for initialization
	void Start () {
        _inputField = GetComponent<InputField>();
        _commandHistory.Add("");
	}
	
	// Update is called once per frame
	void Update () {

        _inputField.Select();
        _inputField.ActivateInputField();

        if (_inputField.textComponent.text != "" && Input.GetKey(KeyCode.Return))
        {
            _commandHistory.Add(_inputField.textComponent.text);
            EnterCommand(_inputField.textComponent.text);
            index = _commandHistory.Count;
            //Debug.Log(index);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && index != -1)
        {
            index--;
            index = Mathf.Clamp(index, 0, _commandHistory.Count - 1);
            Debug.Log(_commandHistory[index]);
            _inputField.text = _commandHistory[index];
            _inputField.MoveTextEnd(false);
            
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) && index != -1)
        {
            index++;
            index = Mathf.Clamp(index, 0, _commandHistory.Count - 1);
            Debug.Log(_commandHistory[index]);
            _inputField.text = _commandHistory[index];
            _inputField.MoveTextEnd(false);
            
        }
    }

    public void EnterCommand(string cmd)
    {
        
        Debug.Log(cmd);
        _body.PrintToBody(">" + cmd + "\n");
        if (TwitchLogin.connected) {
            _twitch.SendMsg(cmd);
        }
        else // hideous spaghetti code but it works offline!
        {
            if (cmd.ToLower().Equals("login") && !_loginIniatated)
            {
                BeginLogin();
            }
            else if(_loginIniatated)
            {
                Debug.Log("else");
                if (_getUsername)
                {
                    _login.username = cmd;
                    _getUsername = false;
                    _body.PrintToBody("Please provide an oAuth Token.\nIf you are unsure what an oAuth Token is, type 'OAUTH' for assistance.");
                }
                else if(_inputField.text.ToLower().Contains("oauth:"))
                {
                    _login.oauthKey = _inputField.text.ToLower();
                    _loginIniatated = false;
                    _login.Submit();
                }
                else if (cmd.ToLower().Equals("oauth"))
                {
                    Application.OpenURL("http://www.twitchapps.com/tmi");
                }
                else
                {
                    _body.PrintToBody("Invalid oAuth Token. Please enter a valid oAuth Token.");
                }
            }
        }
        _inputField.text = "";
    }

    public void BeginLogin()
    {
        _body.PrintToBody("Please provide your Twitch.tv username.");
        _loginIniatated = true;
        _getUsername = true;
    }

    public void CheckHidePassword()
    {
        if (_inputField.text.ToLower().Contains("oauth:"))
        {
            _inputField.contentType = InputField.ContentType.Password;
        }
        else
        {
            _inputField.contentType = InputField.ContentType.Standard;
        }
    }
}
