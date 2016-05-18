using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TickerHeader : Singleton<TickerHeader> {

	[SerializeField]
    private Text textField;

    private string defaultText;

    // Use this for initialization
    void Start()
    {
        defaultText = textField.text;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrintToHeader(string msg)
    {
        textField.text = msg;
    }

    public void ResetText()
    {
        textField.text = defaultText;
    }
}
