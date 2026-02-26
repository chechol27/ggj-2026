using System.Collections.Generic;
using UnityEngine;

namespace Source.Systems.TutorialSystem.Core
{
    [CreateAssetMenu(menuName = "Game/Manual/Stage Manual DB")]
    public class StageManualDatabaseSO : ScriptableObject
    {
        public List<StageManualEntry> entries = new();
    }
}