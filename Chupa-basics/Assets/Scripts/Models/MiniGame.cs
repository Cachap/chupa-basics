using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public delegate void MiniGameResult(bool success);

    public MiniGameResult OnMiniGameComplete;

    public void StartMiniGame()
    {
        Initialize();
    }

    public void EndMiniGame(bool success)
    {
        OnMiniGameComplete?.Invoke(success);
        Cleanup();
    }

    protected abstract void Initialize();
    protected abstract void Cleanup();
}
