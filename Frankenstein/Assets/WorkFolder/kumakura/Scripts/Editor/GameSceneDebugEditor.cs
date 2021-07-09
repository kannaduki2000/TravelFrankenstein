using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(GameSceneDebug))]
public class GameSceneDebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox(
        "DebugMode\n" +
        "true : �f�o�b�O���[�h�̗L����\n" +
        "false : �f�o�b�O���[�h�̖�����\n\n", MessageType.Info);
        EditorGUILayout.HelpBox(
        "�`�g�����`\n" +
        "��Ctrl�L�[ : �V�[���̍ēǂݍ���\n" +
        "��Shift�L�[ : �Q�[��������3�{�ɕύX\n" +
        "�EShift�L�[ : �Q�[��������0.5�{�ɕύX\n" +
        "Shift�L�[�𗣂��ƒʏ�̃Q�[�������Ԃɖ߂�܂�", MessageType.Info);
        EditorGUILayout.HelpBox("BuildError���N����ꍇ�͂���GameObject��j�����Ă�������", MessageType.Warning);
    }
}
