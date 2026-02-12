using System;
using UnityEngine;

namespace Source.Systems.TutorialSystem.Core
{
    [Serializable]
    public class StageManualEntry
    {
        public GameStageType stage;
        [TextArea(4, 12)] public string text;
    }
}