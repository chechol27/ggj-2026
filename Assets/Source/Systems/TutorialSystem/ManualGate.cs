using UnityEngine;

public class ManualGate : MonoBehaviour, IGameService
{
    public bool IsOpen { get; private set; }

    public void Open(string reason = "")
    {
        IsOpen = true;
        Debug.Log($"[ManualGate] OPEN {reason}\n{StackTrace()}");
    }

    public void Close(string reason = "")
    {
        IsOpen = false;
        Debug.Log($"[ManualGate] CLOSE {reason}\n{StackTrace()}");
    }

    private string StackTrace()
    {
        return System.Environment.StackTrace;
    }
}