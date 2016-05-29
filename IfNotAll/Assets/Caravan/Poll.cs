using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Poll : MonoBehaviour {

    private Dictionary<string, List<Viewer>> _results = new Dictionary<string, List<Viewer>>();
    private List<Viewer> _voters = new List<Viewer>();

    private BodyHeader _bodyHeader;
    private Ticker _ticker;
    private TextBody _body;

    bool _isActive = false;

	// Use this for initialization
	void Start () {

        _bodyHeader = BodyHeader.Instance;
        _ticker = Ticker.Instance;
        _body = TextBody.Instance;
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
        _results = new Dictionary<string, List<Viewer>>();
        _voters = new List<Viewer>();
        foreach(string option in options)
        {
            string trimmedOption = option.Trim();
            //Debug.Log(trimmedOption);
            try
            {
                _results.Add(trimmedOption, new List<Viewer>());
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        if(_results.Count > 1)
        {
            _isActive = true;
            ViewResults("");
        }
        else
        {
            _body.PrintToBody("Not enough different options provided.\nWhat kind of poll has only one choice?");
        }

    }

    public void ClosePoll(string input)
    {
        if (_isActive)
        {
            _isActive = false;
            PostResults(input);
        }
    }

    public void Vote(string input)
    {
            CommandInput cin = new CommandInput(input);
            string username = cin.username;
            string parameter = cin.parameter;

            //Debug.Log("Casting vote for " + parameter);

            if (_results.ContainsKey(parameter) && ViewerRegistry.Registry.ContainsKey(username) && !_voters.Contains(ViewerRegistry.Registry[username]))
            {
                //Debug.Log("Voted!");
                _voters.Add(ViewerRegistry.Registry[username]);
                _results[parameter].Add(ViewerRegistry.Registry[username]);
                ViewResults("");
        }
    }

    public void ViewResults(string input)
    {
        string howToText = "Type 'VOTE: <option>' to vote!\n";
        string resultsText = "";
        foreach (string s in _results.Keys)
        {
            resultsText += "'"+ s.ToUpper() + "': ";
            decimal percentage = ((decimal)_results[s].Count / Mathf.Max(1,_voters.Count)) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "% / ";
            //Debug.Log(percentageString);
        }

        _bodyHeader.PrintToHeader(howToText + resultsText);
    }

    public void PostResults(string input)
    {
        string resultsText = "Poll concluded with the followng results:";
        foreach (string s in _results.Keys)
        {
            resultsText += "\n'" + s.ToUpper() + "': ";
            decimal percentage = ((decimal)_results[s].Count / Mathf.Max(1, _voters.Count)) * 100.00m;
            string percentageString = percentage.ToString("F").PadLeft(6, '0');
            resultsText += percentageString + "%";
            Debug.Log(percentageString);
        }
        _bodyHeader.ResetText();
        _ticker.PrintToTicker(resultsText);
    }
}
