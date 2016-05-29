using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Module))]
public class ModuleInspector : Editor
{
    string[] _choices;
    Module _module;

    void OnEnable()
    {
        _module = target as Module;
        
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        _choices = _module._handles.ToArray();
        _module.Document = EditorGUILayout.ObjectField("XML Document", _module.Document, typeof(TextAsset), true) as TextAsset;
        EditorGUILayout.LabelField("Location Name", _module.location.locationName);
        _module.location.displayEntryInTicker = EditorGUILayout.Toggle("Display Arrival?", _module.location.displayEntryInTicker);
        if (_choices.Length > 0)
        {
            _module.index = EditorGUILayout.Popup("Starting Text", _module.index, _choices);
            _module.startTextHandle = _choices[_module.index];
            EditorGUILayout.TextArea(_module.StringList[_module.startTextHandle].EnforceNewlines(), GUILayout.Height(100f));
        }
        EditorUtility.SetDirty(target);
    }
}