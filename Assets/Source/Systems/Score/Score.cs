using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Score : MonoBehaviour, IGameService
{
    const string URL = "https://script.google.com/macros/s/AKfycbzbrajDajFsTeKpUTIkc10CoG_OM9gcAJHBb1CIhwI1E26KA774m3gDt5kSO_xBwR5d/exec";

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
                Debug.Log("Data enviada");
                Debug.Log("Respuesta de Google Sheets: " + www.downloadHandler.text);
            }
        }
    }
    
    public void SendAndResetPoints()
    {
        if (!PlayerPrefs.HasKey("Username")) return;
        StartCoroutine(SendScore(GameServices.Get<Game>().Points));
    }
}