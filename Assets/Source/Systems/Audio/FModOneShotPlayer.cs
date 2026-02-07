using FMODUnity;
using UnityEngine;


public class FModOneShotPlayer : MonoBehaviour
{
    public void PlayOneShot(string eventPath)
    {
        try
        {
            RuntimeManager.PlayOneShot(eventPath, transform.position);
        }
        catch
        {
            //discarded
        }
    }
}
