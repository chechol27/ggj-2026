using System;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using UnityEngine;


public class AnimatorAudioPlayer : MonoBehaviour
{

    public void AnimationAudioEvent(string eventPath)
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
