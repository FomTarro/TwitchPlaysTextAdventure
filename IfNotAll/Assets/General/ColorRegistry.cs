using UnityEngine;
using System.Collections.Generic;
using System;


public class ColorRegistry : Singleton<ColorRegistry> {
   
    public List<ColorListingEntry> colors;
    private Dictionary<string, Color32> colorList = new Dictionary<string, Color32>();
    [SerializeField]
    private Color32 defaultColor;

	// Use this for initialization
	void Awake () {
        CompileToDictionary();
	}

    void CompileToDictionary()
    {
        foreach (ColorListingEntry c in colors)
        {
            colorList.Add(c.name, c.color);
        }
    }
	
    public string HexOfNamedColor(string color)
    {
        if (colorList.ContainsKey(color))
            return HexConverter.ColorToHex(colorList[color]);
        else
            return HexConverter.ColorToHex(defaultColor);
    }
}

[Serializable]
public class ColorListingEntry
{
    public string name;
    public Color32 color;
}
