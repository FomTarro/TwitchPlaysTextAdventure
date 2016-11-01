using UnityEngine;
using System.Collections.Generic;
using System;


public class Shop : MonoBehaviour {

    private List<ShopInventory> _inventoryList = new List<ShopInventory>();
    private Dictionary<string, string> _inventoryDictionary = new Dictionary<string, string>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CompileDictionary()
    {
        foreach(ShopInventory si in _inventoryList)
        {
            _inventoryDictionary.Add(si.itemHandle, si.cost);
        }
    }
}

[Serializable]
public class ShopInventory
{
    public string itemHandle;
    public string cost;
}
