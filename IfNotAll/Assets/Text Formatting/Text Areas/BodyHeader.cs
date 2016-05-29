using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BodyHeader : Singleton<BodyHeader> {

    [SerializeField]
    private Text _textField;

    private string _defaultText;

	// Use this for initialization
	void Start ()
    {
        _defaultText = _textField.text;
        PrintToHeader(_defaultText);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void PrintToHeader(string msg)
    {
        msg = msg.EnforceNewlines();
        msg = msg.HighlightPresets();
        _textField.text = msg;
    }

    public void ResetText()
    {
        PrintToHeader(_defaultText);
    }
}
