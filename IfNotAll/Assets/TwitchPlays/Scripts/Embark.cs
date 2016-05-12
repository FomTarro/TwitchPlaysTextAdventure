using UnityEngine;
using System.Collections;

public class Embark : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Go(string input)
    {
        /*
        Debug.Log(input);
        string username = input.Substring(0, input.IndexOf(PlayCommands.seperator));
        Debug.Log(username);
        string parameter = input.Substring(username.Length + 1);
        Debug.Log(parameter);
        */

        Debug.Log("DO YOU REALLY WANT TO DO THAT?");

    }
}
