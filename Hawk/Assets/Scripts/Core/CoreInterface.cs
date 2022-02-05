public interface IMeta
{
    public Character Player { get; }
    public SaveManager saveManager { get; }
    public void StartGame();
    public void FinishGame();
    public void AddPauseGameComponentToList(IPauseGame isPausedComponent);
    public void PauseGame(bool value);
}
