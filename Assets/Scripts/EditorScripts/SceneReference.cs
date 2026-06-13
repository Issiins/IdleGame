using System;
using UnityEngine;
using UnityEditor;
[Serializable]
public class SceneReference
{
    #if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset;
    #endif
    [SerializeField, HideInInspector] private string sceneName;

    public string Name => sceneName;

    #if UNITY_EDITOR
    public void Validate()
    {
        if (sceneAsset != null)
            sceneName = sceneAsset.name;
    }
    #endif
}
