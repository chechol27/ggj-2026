using UnityEngine;

public abstract class GameStage : MonoBehaviour
{
    public abstract void OnStateEnter();

    public virtual void OnStateExit()
    {
        
    }
}
