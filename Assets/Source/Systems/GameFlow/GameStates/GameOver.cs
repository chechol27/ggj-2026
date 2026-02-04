using System.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Networking;

public class GameOver : GameStage
{
    private string URL =
        "https://script.google.com/macros/s/AKfycbxSoO2gksEcMHaqklyrisA4w5j93bwKHJpq8Ir9RhQZWqmI3i0WQkYgI7gUBRlRiL7W/exec";

    
    public override void OnStateEnter()
    {
        GameServices.Get<HUD>().SetHUDUI<GameOverHUD>();
        GameServices.Get<GameFlow>().SetPause(true);
        SendAndResetPoints();
        //Canvas de perder
    }

    private void SendAndResetPoints()
    {
        if (!PlayerPrefs.HasKey("Username")) return;
        StartCoroutine(SendScore(GameServices.Get<Game>().Points));
    }

    public override void OnStateExit()
    {
        GameServices.Get<GameFlow>().SetPause(false);
    }
    
    IEnumerator SendScore(int points)
    {
        WWWForm form = new WWWForm();

        form.AddField("uid", PlayerPrefs.GetString("Username", "null"));
        form.AddField("score", points);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Respuesta de Google Sheets: " + www.downloadHandler.text);
                Debug.Log("Data enviada");
            }
        }
    }
}
