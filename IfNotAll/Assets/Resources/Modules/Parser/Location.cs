using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Location {

    public string locationName;
    public string LocationFormatted
    {
        get { return "[magenta]" + locationName + "[magenta]"; }
    }
    public bool displayEntryInTicker = true;

}
