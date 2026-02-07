using Source.Systems.ModelViewer.Core;
using Source.Systems.ModelViewer.InputControl;
using UnityEngine;

namespace Source.Systems.ModelViewer
{
    public class ModelViewerController : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField] private Pool pool; 

        [Header("Viewer")]
        [SerializeField] private Transform spawnRoot; 
        [SerializeField] private bool deactivatePrevious = true;

        [Header("Rotation")]
        [SerializeField] private ViewerLookRotator  dragRotator;

        private GameObject currentInstance;
        private GameObject currentPrefab;
        private ModelViewerItemSO currentItem;

        public void Select(ModelViewerItemSO item)
        {
            if (item == null || item.prefab == null) return;

            currentItem = item;

            if (currentInstance != null && deactivatePrevious)
            {
                currentInstance.SetActive(false);
                currentInstance = null;
                currentPrefab = null;
            }

            if (!pool.Spawn(item.prefab, out var go, activate: true))
            {
                Debug.LogWarning($"[ModelViewer] Pool no pudo spawnear: {item.prefab.name}");
                return;
            }

            currentInstance = go;
            currentPrefab = item.prefab;

            currentInstance.transform.SetParent(spawnRoot, worldPositionStays: false);
            currentInstance.transform.localPosition = item.localPosition;
            currentInstance.transform.localRotation = Quaternion.Euler(item.localEuler);
            currentInstance.transform.localScale = item.localScale;

            if (dragRotator != null)
            {
                dragRotator.SetTarget(spawnRoot);

                if (item.resetRotationOnSelect)
                    dragRotator.ResetRotation();
            }
        }
    }
}