using System.Collections.Generic;
using UnityEngine;

namespace Source.Systems.ModelViewer.Core
{
    [CreateAssetMenu(menuName = "Model Viewer/Database", fileName = "ModelViewerDatabase")]
    public class ModelViewerDatabaseSO :ScriptableObject
    {
        public List<ModelViewerItemSO> items = new();
    }
}