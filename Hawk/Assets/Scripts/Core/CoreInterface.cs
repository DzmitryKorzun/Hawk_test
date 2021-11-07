public interface IMeta
{
    public Character Player { get; }
    public bool IsLevelStart { get; }
    public void StartGame();
    public void FinishGame();
}
