using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewerRegistry : MonoBehaviour {

    static Dictionary<string, Viewer> viewerRegistry = new Dictionary<string, Viewer>();
    public static Dictionary<string, Viewer> Registry
    {
        get { return viewerRegistry; }
    }

    [SerializeField]
    private ViewerCharacter characterPrefab;

    [SerializeField]
    private Ticker ticker;
    [SerializeField]
    private Text playerCount;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
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

            ticker.PrintToTicker(v.UsernameFormatted() + " has joined the crew.\nHired as a " + Registry[username].Archetype.ToString()+". Brought " + Registry[username].food + " food and " + Registry[username].gold + " gold."); ;

        }
        else if (!Registry.ContainsKey(username) && username.ToLower().Equals(TwitchIRC.Instance.nickName.ToLower())){
            Viewer v = new Viewer(username);
            Registry.Add(username, v);

            ticker.PrintToTicker(v.UsernameFormatted() + " has founded the caravan.\nProvided an initial supply of " + TextEffects.Instance.DisplayFood(Registry[username].food) +  " and " + TextEffects.Instance.DisplayCurrency(Registry[username].gold) +".");
        }
    }

    public void KillPlayer(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;

        if (Registry.ContainsKey(parameter) && Registry[parameter].IsAlive)
        {
            Registry[parameter].Kill(username);
            int foodProvided = Random.Range(1, 4);
            ResourceTracker.food += foodProvided;
            ticker.PrintToTicker(parameter + " was " + TextEffects.Instance.DisplayColored("killed", "red") +  ".\nReluctantly, their body provides "  + TextEffects.Instance.DisplayFood(foodProvided) + ".");
        }
    }
}
