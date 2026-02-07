using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Systems.ModelViewer.View
{
    public class ModelViewerListItemUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text label;

        public void Bind(string text, System.Action onClick)
        {
            label.text = text;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}