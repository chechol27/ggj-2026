using UnityEngine;

namespace Source.Systems.ModelViewer.Core
{
    [CreateAssetMenu(menuName = "Model Viewer/Item", fileName = "ModelViewerItem")]
    public class ModelViewerItemSO:ScriptableObject
    { 
        [Header("UI")]
        public string displayName = "Item";

        [Header("Model")]
        public GameObject prefab;

        [Header("Viewer Transform")]
        public Vector3 localPosition = Vector3.zero;
        public Vector3 localEuler = Vector3.zero;
        public Vector3 localScale = Vector3.one;

        [Header("Optional")]
        public bool resetRotationOnSelect = true;
        
        public bool useYawLimits = false;
        public float minYaw = -180f;
        public float maxYaw = 180f;
    }
}