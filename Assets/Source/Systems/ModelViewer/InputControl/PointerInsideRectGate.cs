using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Systems.ModelViewer.InputControl
{
    public class PointerInsideRectGate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool IsPointerInside { get; private set; }

        public void OnPointerEnter(PointerEventData eventData) => IsPointerInside = true;
        public void OnPointerExit(PointerEventData eventData) => IsPointerInside = false;

        private void OnDisable() => IsPointerInside = false;
    }
}