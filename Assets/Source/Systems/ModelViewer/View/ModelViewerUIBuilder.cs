using System.Collections.Generic;
using Source.Systems.ModelViewer.Core;
using UnityEngine;

namespace Source.Systems.ModelViewer.View
{
    public class ModelViewerUIBuilder : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private ModelViewerDatabaseSO database;

        [Header("UI")]
        [SerializeField] private Transform contentRoot; 
        [SerializeField] private ModelViewerListItemUI itemPrefab;

        [Header("Target")]
        [SerializeField] private ModelViewerController viewer;

        private readonly List<ModelViewerListItemUI> spawned = new();

        private void OnEnable()
        {
            Rebuild();
        }

        public void Rebuild()
        {
            for (int i = spawned.Count - 1; i >= 0; i--)
            {
                if (spawned[i] != null) Destroy(spawned[i].gameObject);
            }
            spawned.Clear();

            if (database == null || database.items == null) return;

            foreach (var item in database.items)
            {
                if (item == null) continue;

                var ui = Instantiate(itemPrefab, contentRoot);
                ui.Bind(item.displayName, () => viewer.Select(item));
                spawned.Add(ui);
            }

            if (database.items.Count > 0 && database.items[0] != null)
                viewer.Select(database.items[0]);
        }
    }
}