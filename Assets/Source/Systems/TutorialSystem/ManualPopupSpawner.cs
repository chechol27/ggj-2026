using UnityEngine;

public class ManualPopupSpawner : MonoBehaviour
{
    [SerializeField] private ManualPopupUI popupPrefab;
    [SerializeField] private Canvas HUDTutorialCanvas;

    private ManualPopupUI current;

    public ManualPopupUI Show(GameStageType stage, string text)
    {
        if (current != null)
            Destroy(current.gameObject);

        current = Instantiate(popupPrefab,HUDTutorialCanvas.transform);
        current.Setup(stage, text);

        return current;
    }
}