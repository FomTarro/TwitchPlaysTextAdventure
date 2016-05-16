using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class Module : MonoBehaviour {

    [SerializeField]
    private TextAsset document;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Parse()
    {
      
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(document.text);
      

    }
}
