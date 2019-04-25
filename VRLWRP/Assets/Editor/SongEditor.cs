using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SongEditor : EditorWindow {

    AnimationCurve curve;
    SongNodes nodes;
    [MenuItem("Window/Song Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SongEditor>("Song Editor");
    }

    void OnGUI()
    {
        
        EditorGUILayout.CurveField(curve);
        nodes = EditorGUILayout.ObjectField("", nodes, typeof(SongNodes), true) as SongNodes;

        if (GUILayout.Button("Populate Graph"))
        {
            if (nodes == null)
            {
                ShowNotification(new GUIContent("No object selected for graph"));
            }
            else
            {
                ClearGraph();
                PopulateGraph();
            }
        }

        if (GUILayout.Button("Save Changes"))
        {
            if (nodes == null)
            {
                ShowNotification(new GUIContent("No object selected to save"));
            }
            else
            {
                SaveNodeChanges();
            }
        }

    }

    void ClearGraph()
    {

        if (curve == null)
        {
            curve = new AnimationCurve();
        }
        for (int i = curve.keys.Length - 1; i >= 0; i--)
        {
            curve.RemoveKey(i);
        }
    }


    void PopulateGraph()
    {
        foreach(SongNode node in nodes.nodes)
        {
            curve.AddKey(node.floatTimestamp, 0);
        }

    }

    void SaveNodeChanges()
    {
        EditorUtility.SetDirty(nodes);

        List<SongNode> songnodeList = new List<SongNode>();
        for(int i =0; i<curve.keys.Length; i++)
        {
            Keyframe currentKey = curve.keys[i];
            SongNode node = new SongNode();
            node.floatTimestamp = currentKey.time;
            songnodeList.Add(node);
        }

        nodes.nodes = songnodeList;

    }
}
