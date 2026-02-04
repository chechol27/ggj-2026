using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Score : MonoBehaviour, IGameService
{
    const string URL = "https://script.google.com/macros/s/AKfycbxSoO2gksEcMHaqklyrisA4w5j93bwKHJpq8Ir9RhQZWqmI3i0WQkYgI7gUBRlRiL7W/exec";

    IEnumerator SendScore(int points)
    {
        WWWForm form = new ();

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
    
    public void SendAndResetPoints()
    {
        if (!PlayerPrefs.HasKey("Username")) return;
        StartCoroutine(SendScore(GameServices.Get<Game>().Points));
    }
}