using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Score : MonoBehaviour, IGameService
{
    const string URL = "https://script.google.com/macros/s/AKfycbwSDFyjwKwx6_Xc0NqKsctXQPhxnJrMRwylyOsI-JVga2bre1PKa0wwVrknpqj7X7NS/exec";

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