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
        Debug.Log(input);
        string username = input.Substring(0, input.IndexOf(PlayCommands.seperator));
        Debug.Log(username);
        string parameter = input.Substring(username.Length+1);
        parameter = parameter.Trim();
        Debug.Log(parameter);

        if (!Registry.ContainsKey(username)) {

            Viewer v = new Viewer(username, parameter.ToLower());
            Registry.Add(username, v);

            ticker.PrintToTicker(username + " has joined the crew.\nHired as a " + Registry[username].Archetype.ToString()+". Brought " + Registry[username].food + " food and " + Registry[username].gold + " gold."); ;

            /*
            GameObject go = Instantiate<GameObject>(characterPrefab.gameObject);
            Registry.Add(username, go.GetComponent<ViewerCharacter>());
            Registry[username].GenerateStartingStats();
            ticker.PrintToTicker(username + " has joined the crew.\nHired as a " + parameter +".\nBrought " + Registry[username].food + " food and " + Registry[username].gold + " gold.");
            ResourceTracker.crew = Registry.Count;
            ResourceTracker.food += Registry[username].food;
            ResourceTracker.gold += Registry[username].gold;
            */
        }
    }

    public void KillPlayer(string input)
    {
       
        Debug.Log(input);
        string username = input.Substring(0, input.IndexOf(PlayCommands.seperator));
        string parameter = input.Substring(username.Length + 1);
        parameter = parameter.Trim();
        Debug.Log(parameter);

        if(Registry.ContainsKey(parameter) && Registry[parameter].IsAlive)
        {
            Registry[parameter].Kill(username);
            int foodProvided = Random.Range(1, 4);
            ResourceTracker.food += foodProvided;
            ticker.PrintToTicker(parameter + " was killed.\nReluctantly, their body provides " + foodProvided + " food.");
        }
    }
}
