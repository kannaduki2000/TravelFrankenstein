using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateGameDebug : MonoBehaviour
{

    [MenuItem("Frankenstein/Create GameDebug")]
    public static void Create()
    {
        GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WorkFolder/kumakura/Prefabs/GameDebug.prefab");
        GameObject debugobject = PrefabUtility.InstantiatePrefab(gameObject) as GameObject;
        Undo.RegisterCreatedObjectUndo(debugobject, "debugobject");
    }
}
