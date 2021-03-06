﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewerRegistry : MonoBehaviour {

    static Dictionary<string, Viewer> _viewerRegistry = new Dictionary<string, Viewer>();
    public static Dictionary<string, Viewer> Registry
    {
        get { return _viewerRegistry; }
    }

    [SerializeField]
    private Ticker _ticker;
    [SerializeField]
    private Text _playerCount;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        _ticker = Ticker.Instance;
        /*
        for (int i = 0; i <50; i++)
        {
            ticker.PrintToTicker(i.ToString());
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddToRegistry(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        if (!Registry.ContainsKey(username) && !username.ToLower().Equals(TwitchIRC.Instance.nickName.ToLower())) {

            Viewer v = new Viewer(username, parameter);
            Registry.Add(username, v);

            _ticker.PrintToTicker(v.UsernameFormatted + " has joined the crew.\nHired as a " + v.Archetype.ToString()+". Brought [leaf]" + v.Food + " food[leaf] and [leaf]" + v.Currency + "hope[leaf]."); ;

        }
        else if (!Registry.ContainsKey(username) && username.ToLower().Equals(TwitchIRC.Instance.nickName.ToLower())){
            Viewer v = new Viewer(username);
            Registry.Add(username, v);

            _ticker.PrintToTicker(v.UsernameFormatted + " has founded the caravan.\nProvided an initial supply of [leaf]" + v.Food + " food[leaf] and [leaf]" + v.Currency +" hope[leaf].");
        }
    }

    public void KillPlayer(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        if (Registry.ContainsKey(parameter) && Registry[parameter].IsAlive)
        {
            Viewer v = Registry[parameter];
            Registry[parameter].Kill(username);
            int foodProvided = Random.Range(1, 4);
            ResourceTracker.food += foodProvided;
            _ticker.PrintToTicker("Reluctantly, their body provides [leaf]" + foodProvided + " food[leaf].");
        }
    }
}
