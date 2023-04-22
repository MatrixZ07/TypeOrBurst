using System.Collections;

public abstract class State
{
    protected readonly GameManager _gameManager;
    public State(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public virtual IEnumerator Pause() { yield break; }
    public virtual IEnumerator Resume() { yield break; }
    public virtual IEnumerator Start() { yield break; }
    public virtual IEnumerator End() { yield break;}

}
public class GameRunningState : State
{
    public GameRunningState(GameManager gameManager) : base(gameManager) { }
    //TODO: Hier alles für UI-Initialisierung implementieren
    public override IEnumerator Start()
    {
        return base.Start();
    }
}

public class GameNotRunningState : State
{
    public GameNotRunningState(GameManager gameManager) : base(gameManager) { }
    //TODO: Hier alles für UI-Initialisierung implementieren
}

