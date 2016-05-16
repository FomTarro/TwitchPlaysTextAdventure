using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Poll : MonoBehaviour {

    private Dictionary<string, List<Viewer>> results = new Dictionary<string, List<Viewer>>();
    private List<Viewer> voters = new List<Viewer>();

    [SerializeField]
    private Text resultsTextField;

    bool isActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string resultsText = "Type 'VOTE: <option>' to vote!\n";
        foreach (string s in results.Keys)
        {
            resultsText += s + ": ";
            decimal percentage = ((decimal)results[s].Count / Mathf.Max(1, voters.Count)) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "% / ";
            Debug.Log(percentageString);
        }

        resultsTextField.text = resultsText;
    }

    public void MakePoll(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        string[] options = parameter.Split(',');
        results = new Dictionary<string, List<Viewer>>();
        voters = new List<Viewer>();
        foreach(string option in options)
        {
            string trimmedOption = option.Trim();
            //Debug.Log(trimmedOption);
            try
            {
                results.Add(trimmedOption, new List<Viewer>());
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }

    }

    public void Vote(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        if (results.ContainsKey(parameter) && !voters.Contains(ViewerRegistry.Registry[username]))
        {
            Debug.Log("Voted!");
            voters.Add(ViewerRegistry.Registry[username]);
            results[parameter].Add(ViewerRegistry.Registry[username]);
        }

    }

    public void ViewResults(string input)
    {
        string resultsText = "Type 'VOTE: <option>' to vote!\n";
        foreach (string s in results.Keys)
        {
            resultsText += s + ": ";
            decimal percentage = ((decimal)results[s].Count / voters.Count) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "% / ";
            Debug.Log(percentageString);
        }

        resultsTextField.text = resultsText;
    }
}
