using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private InputField UsernameInputField;
    [SerializeField] private TextMeshProUGUI FeedbackText;
    private bool _announced;

    private void Start()
    {
        _announced = false;
        FeedbackText.text = "";
        if (PlayerPrefs.HasKey("Username"))
        {
            UsernameInputField.text = PlayerPrefs.GetString("Username");
        }
    }

    public void StartGame()
    {
        GameServices.Get<GameFlow>().SwitchStage(GameStageType.StartGame);
    }

    void AnnounceUsername()
    {
        if (!_announced)
        {
            if (UsernameInputField.text.Length > 3)
            {
                PlayerPrefs.SetString("Username", UsernameInputField.text);
                StartGame();
                return;
            }
            else
            {
                PlayerPrefs.DeleteAll();
                FeedbackText.text = "Sin username, tu puntaje no podrá ser guardado. Si no quieres guardar juega como invitado.";
                _announced = true;
                return;
            }
        }
        StartGame();
    }
    
    /*private void Awake()
    {
        startGameButton.onClick.AddListener(StartGame);
    }*/
}
