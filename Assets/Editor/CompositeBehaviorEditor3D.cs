using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//src: https://gist.github.com/RapidR3D/b245f7b72f476f733d751ecc092d44e2

[CustomEditor(typeof(CompositeBehavior3D))]
public class CompositeBehaviorEditor3D : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehavior3D cb = (CompositeBehavior3D)target;

        //Check empty
        if (cb.behaviors == null || cb.behaviors.Length == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            //Labels
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f), GUILayout.MaxWidth(290f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(65f), GUILayout.MaxWidth(65f));
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginChangeCheck();

            //Check for change and display arrays
            for (int i = 0; i < cb.behaviors.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(20f), GUILayout.MaxWidth(20f));
                cb.behaviors[i] = (FlockBehavior3D)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehavior3D), false, GUILayout.MinWidth(20f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                //Undo.RecordObject(target, "Behaviors");
                EditorUtility.SetDirty(target);
                GUIUtility.ExitGUI();
            }
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Behavior"))
        {
            AddBehavior(cb);
            GUIUtility.ExitGUI();
        }

        // Uncomment for button layout to be stacked

        /*EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();*/

        if (cb.behaviors != null && cb.behaviors.Length > 0)
        {
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehavior(cb);
                GUIUtility.ExitGUI();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void AddBehavior(CompositeBehavior3D cb)
    {
        int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;
        FlockBehavior3D[] newBehaviors = new FlockBehavior3D[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }

    void RemoveBehavior(CompositeBehavior3D cb)
    {
        int oldCount = cb.behaviors.Length;
        if (oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }

        FlockBehavior3D[] newBehaviors = new FlockBehavior3D[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
}
