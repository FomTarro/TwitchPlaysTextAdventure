using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModuleMaster : Singleton<ModuleMaster> {

    private Dictionary<string, Module> _moduleRegistry = new Dictionary<string, Module>();
    public Dictionary<string, Module> ModuleRegistry
    {
        get { return _moduleRegistry; }
    }

    public void CompileRegistry()
    {
        foreach (Module m in GetComponentsInChildren<Module>())
        {
            try
            {
                _moduleRegistry.Add(m.name, m);
            }
            catch (Exception e)
            {
                Debug.Log(m.name + " not added to registry.");
            }
        }
    }

    public string LookupHandleInModule(string moduleName, string handle)
    {
        string val = "";
        try
        {
            val = _moduleRegistry[moduleName].StringList[handle.ToLower()];
        }
        catch
        {
            val = "Unable to find handle ^" + handle.ToLower() + "^ in module ^" + moduleName + "^!\nPlease report this to the developer!\nYou can reach him on Twitter at ~@FomTarro~.";
        }
        return val;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
