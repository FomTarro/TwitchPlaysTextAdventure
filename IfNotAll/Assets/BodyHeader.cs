using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyHeader : Singleton<BodyHeader> {

    [SerializeField]
    private Text textField;

    private string defaultText;

	// Use this for initialization
	void Start ()
    {
        defaultText = textField.text;
        PrintToHeader(defaultText);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void PrintToHeader(string msg)
    {
        msg = msg.EnforceNewlines();
        msg = msg.HighlightPresets();
        textField.text = msg;
    }

    public void ResetText()
    {
        PrintToHeader(defaultText);
    }
}
