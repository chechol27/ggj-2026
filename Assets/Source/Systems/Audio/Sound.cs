using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Sound : MonoBehaviour, IGameService
{
    private Dictionary<string, EventInstance> currentEvents = new ();

    EventInstance GetEventByName(string eventPath)
    {
        if (!currentEvents.ContainsKey(eventPath))
        {
            currentEvents[eventPath] = RuntimeManager.CreateInstance(eventPath);
        }

        return currentEvents[eventPath];
    }

    public void Play(string eventPath, bool allowMultiple = false)
    {
        EventInstance instance = GetEventByName(eventPath);
        if (!allowMultiple)
        {
            var reslt = instance.getPlaybackState(out var state);
            if (reslt == RESULT.OK)
            {
                if (state != PLAYBACK_STATE.PLAYING)
                {
                    GetEventByName(eventPath).start();
                }
            }
        }
        else
        {
            GetEventByName(eventPath).start();
        }

    }

    public void Stop(string eventPath)
    {
        GetEventByName(eventPath).stop(STOP_MODE.ALLOWFADEOUT);
    }
    
    public void SetParameter(string eventPath, string paramName, float param)
    {
        GetEventByName(eventPath).setParameterByName(paramName, param);
    }
}
