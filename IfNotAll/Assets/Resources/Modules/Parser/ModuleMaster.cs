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

    [SerializeField]
    private Module _activeModule;
    public Module ActiveModule
    {
        get { return _activeModule; }
    }

    void CompileRegistry()
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
                Debug.Log(e.Message);
            }
        }
    }

    public void SetActiveModule(Module m)
    {
        CompileRegistry();
        _activeModule = m;
        m.EnterArea();
    }

    public string LookupHandleInModule(string moduleName, string handle)
    {
        string val = "";
        try
        {
            val = _moduleRegistry[moduleName].StringList[handle.ToLower()];
        }
        catch(Exception e)
        {
            val = "Unable to find handle ^" + handle.ToLower() + "^ in module ^" + moduleName + "^!\nPlease report this to the developer!\nYou can reach him on Twitter at ~@FomTarro~.";
            Debug.Log(e.Message);
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
