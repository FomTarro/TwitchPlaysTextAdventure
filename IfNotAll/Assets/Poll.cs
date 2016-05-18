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
        if(results.Count > 1)
        {
            isActive = true;
            ViewResults("");
        }
        else
        {
            TextBody.Instance.PrintToBody("Not enough different options provided.\nWhat kind of poll has only one choice?");
        }

    }

    public void ClosePoll(string input)
    {
        if (isActive)
        {
            isActive = false;
            PostResults(input);
        }
    }

    public void Vote(string input)
    {
            CommandInput cin = new CommandInput(input);
            string username = cin.username;
            string parameter = cin.parameter;

            //Debug.Log("Casting vote for " + parameter);

            if (results.ContainsKey(parameter) && ViewerRegistry.Registry.ContainsKey(username) && !voters.Contains(ViewerRegistry.Registry[username]))
            {
                //Debug.Log("Voted!");
                voters.Add(ViewerRegistry.Registry[username]);
                results[parameter].Add(ViewerRegistry.Registry[username]);
                ViewResults("");
        }
    }

    public void ViewResults(string input)
    {
        string howToText = "Type 'VOTE: <option>' to vote!\n";
        string resultsText = "";
        foreach (string s in results.Keys)
        {
            resultsText += s.ToUpper() + ": ";
            decimal percentage = ((decimal)results[s].Count / Mathf.Max(1,voters.Count)) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "% / ";
            //Debug.Log(percentageString);
        }

        BodyHeader.Instance.PrintToHeader(howToText + resultsText);
    }

    public void PostResults(string input)
    {
        string resultsText = "Poll concluded with the followng results:";
        foreach (string s in results.Keys)
        {
            resultsText += "\n" + s.ToUpper() + ": ";
            decimal percentage = ((decimal)results[s].Count / Mathf.Max(1, voters.Count)) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "%";
            Debug.Log(percentageString);
        }
        BodyHeader.Instance.ResetText();
        Ticker.Instance.PrintToTicker(resultsText);
    }
}
