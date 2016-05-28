using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Module))]
[CanEditMultipleObjects]
public class ModuleInspector : Editor
{
    string[] _choices;
    Module module;

    void OnEnable()
    {
        module = target as Module;
        _choices = module.handles.ToArray(); ;
    }

    public override void OnInspectorGUI()
    {
       
        module.Document = EditorGUILayout.ObjectField("XML Document", module.Document, typeof(TextAsset), true) as TextAsset;
        EditorGUILayout.LabelField("Location Name", module.location.locationName);
        module.location.displayEntryInTicker = EditorGUILayout.Toggle("Display Arrival?", module.location.displayEntryInTicker);
        if (module.handles.Count > 0)
        {
            module.index = EditorGUILayout.Popup("Starting Text", module.index, _choices);
            module.startTextHandle = _choices[module.index];
            EditorGUILayout.TextArea(module.StringList[module.startTextHandle].EnforceNewlines(), GUILayout.Height(100f));
        }
        EditorUtility.SetDirty(target);
    }
}