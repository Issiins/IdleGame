using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace EditorScripts
{
    [Serializable]
    public class SceneReference
    {
        #if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;
        #endif
        [SerializeField, HideInInspector] private string sceneName;
        public string name => sceneName;
        #if UNITY_EDITOR
        public void Validate()
        {
            if (sceneAsset != null)
                sceneName = sceneAsset.name;
        }
        #endif
    }
}
