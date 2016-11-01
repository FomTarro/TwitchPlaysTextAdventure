using UnityEngine;
using System.Collections;

public class CommandInput { 
    public string username;
    public string parameter;

    public CommandInput(string input)
    {
        //Debug.Log(input);
        username = input.Substring(0, input.IndexOf(PlayCommands.seperator)).ToLower();
        //Debug.Log(username);
        parameter = input.Substring(username.Length + 1);
        parameter = parameter.Trim().ToLower();
        //Debug.Log(parameter);
    }
}
