using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RecordingManager))]
public class RecordingManagerInspector : Editor
{
    bool recToggleAll = true;
    bool movToggleAll = true;
    bool spcToggleAll = true;

    public override void OnInspectorGUI()
    {
        RecordingManager recManager = (RecordingManager) target;

        // ADD OR REMOVE ACTOS
        GUILayout.BeginHorizontal();
        GUILayout.Label("Add/Remove Actors");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(40)))
        {
            ActorItem newActor = new ActorItem();
            recManager.cast.Add(newActor);
        }
        if (GUILayout.Button("-", GUILayout.Width(40)))
        {
            if (recManager.cast.Count > 0)
                recManager.cast.RemoveAt(recManager.cast.Count - 1);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(7);

        // ACTOR LIST WITH ENABLING BOOLS
        GUILayout.BeginHorizontal();
        GUILayout.Label("Actors");
        GUILayout.FlexibleSpace();
        GUILayout.Label("Rec", GUILayout.Width(25));
        GUILayout.Label("Mov", GUILayout.Width(25));
        GUILayout.Label("Spc", GUILayout.Width(25));
        GUILayout.EndHorizontal();
        for (int i = 0; i < recManager.cast.Count; i++)
        {
            GUILayout.BeginHorizontal();
            recManager.cast[i].actor = (GameObject)EditorGUILayout.ObjectField(recManager.cast[i].actor, typeof(GameObject), true);
            GUILayout.FlexibleSpace();
            recManager.cast[i].recordingEnabled = EditorGUILayout.Toggle(recManager.cast[i].recordingEnabled, GUILayout.Width(25));
            recManager.cast[i].movementEnabled = EditorGUILayout.Toggle(recManager.cast[i].movementEnabled, GUILayout.Width(25));
            recManager.cast[i].specialEnabled = EditorGUILayout.Toggle(recManager.cast[i].specialEnabled, GUILayout.Width(25));
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(7);

        // HELPERS
        GUILayout.BeginHorizontal();
        GUILayout.Label("Toggle All");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("I", GUILayout.Width(15)))
        {
            for (int i = 0; i < recManager.cast.Count; i++)
            {
                recManager.cast[i].recordingEnabled = recToggleAll;
            }
            recToggleAll = !recToggleAll;
        }
        GUILayout.Space(10);
        if (GUILayout.Button("I", GUILayout.Width(15)))
        {
            for (int i = 0; i < recManager.cast.Count; i++)
            {
                recManager.cast[i].movementEnabled = movToggleAll;
            }
            movToggleAll = !movToggleAll;
        }
        GUILayout.Space(10);
        if (GUILayout.Button("I", GUILayout.Width(15)))
        {
            for (int i = 0; i < recManager.cast.Count; i++)
            {
                recManager.cast[i].specialEnabled = spcToggleAll;
            }
            spcToggleAll = !spcToggleAll;
        }
        GUILayout.Space(10);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Focus on recorded");
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Enable", GUILayout.Width(81)))
        {
            for (int i = 0; i < recManager.cast.Count; i++)
            {
                recManager.cast[i].movementEnabled = recManager.cast[i].recordingEnabled;
                recManager.cast[i].specialEnabled = recManager.cast[i].recordingEnabled;
            }
        }
        GUILayout.EndHorizontal();
    }
}
