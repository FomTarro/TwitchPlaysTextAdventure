using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Location {

    public string locationName;
    public string LocationFormatted()
    {
        return "^" + locationName + "^";
    }
    public bool displayEntryInTicker = true;

}
