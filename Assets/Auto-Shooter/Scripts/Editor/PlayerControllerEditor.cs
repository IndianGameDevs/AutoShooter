using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]
public class PlayerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerController controller = target as PlayerController;

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Setup Player Controller"))
        {
            SetReferences(controller);
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void SetReferences(PlayerController controller)
    {
        Debug.Log("Setting References");
        //Todo
    }
}
