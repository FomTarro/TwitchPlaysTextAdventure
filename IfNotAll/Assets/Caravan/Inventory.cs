using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : Singleton<Inventory> {

    private Ticker _ticker;
    private TextBody _body;
    private List<string> _inventorySpace = new List<string>();

    // Use this for initialization
    void Start () {

        _ticker = Ticker.Instance;
        _body = TextBody.Instance;
        _inventorySpace.Add("A whole lot of nothing");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayInventory(string input)
    {
        CommandInput cin = new CommandInput(input);
        string username = cin.username;
        string parameter = cin.parameter;
        _body.PrintToBody("The ^INVENTORY^ contains:");
        foreach(string s in _inventorySpace)
        {
            _body.PrintToBody("* " + s);
        }
    }
}
