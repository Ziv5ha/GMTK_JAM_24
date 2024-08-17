using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using System;
using TMPro;

public class EditorShortcuts: MonoBehaviour {

    [MenuItem("MyMenu/Set Selected Active State %_g")]
    static void SetSelectedActiveState() {
        bool allActive = true;
        for (int i = 0; i < Selection.gameObjects.Length; i++) {
            allActive &= Selection.gameObjects[i].activeSelf;
        }
        if (allActive) {
            for (int i = 0; i < Selection.gameObjects.Length; i++) {
                Undo.RecordObject(Selection.gameObjects[i], "undo " + i);
                Selection.gameObjects[i].SetActive(false);
            }
        } else {
            for (int i = 0; i < Selection.gameObjects.Length; i++) {
                Undo.RecordObject(Selection.gameObjects[i], "undo " + i);
                Selection.gameObjects[i].SetActive(true);
            }
        }
    }

}
