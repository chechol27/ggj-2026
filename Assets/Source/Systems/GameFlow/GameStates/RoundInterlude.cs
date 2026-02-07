using UnityEngine;

public class RoundInterlude : GameStage
{
    private const float MAX_INTERLUDE_TIME = 5.0f;

    [SerializeField] private float timer;

    private GameFlow flow;

    private bool waiting;
    
    public override void OnStateEnter()
    {
        waiting = false;
    }

    public override void OnStateExit()
    {
        waiting = true;
        timer = 0;
    }

    private void Awake()
    {
        flow = GameServices.Get<GameFlow>();
    }
    
    public GameStageType GetNextRound()
    {
        GameStageType ret = GameServices.Get<Game>().currentRound % 10 == 0 ? GameStageType.AsteroidField : GameStageType.EnemyWave;
        Debug.Log(ret);
        return ret;
    }

    private void Update()
    {
        if (waiting) return;
        timer += Time.deltaTime;
        if (timer > MAX_INTERLUDE_TIME)
        {
            flow.SwitchStage(GetNextRound());
            timer = 0;
        }
    }
}
