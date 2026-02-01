using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStageDebugger))]
public class GameStageDebuggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Switch State"))
        {
            var tg = target as GameStageDebugger;
            tg?.SimulateStageSwitch();
        }
    }
}
