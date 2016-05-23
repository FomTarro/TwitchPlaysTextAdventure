using UnityEngine;
using System.Collections.Generic;
using System;


public class ColorRegistry : Singleton<ColorRegistry> {

    public List<ColorListingEntry> colors;
    public Dictionary<string, Color32> ColorList = new Dictionary<string, Color32>();


	// Use this for initialization
	void Awake () {
        CompileToDictionary();
	}

    void CompileToDictionary()
    {
        foreach (ColorListingEntry c in colors)
        {
            ColorList.Add(c.name, c.color);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

[Serializable]
public class ColorListingEntry
{
    public string name;
    public Color32 color;
}
