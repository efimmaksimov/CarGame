using System;

public class GameSaver : Singleton<GameSaver>
{
    private event Action onSaveGame;

    public void SaveGame()
    {
        onSaveGame?.Invoke();
        UnityEngine.Debug.Log("Save");
    }

    public void AddListener(Action onSaveGame)
    {
        this.onSaveGame += onSaveGame;
    }

    public void RemoveListener(Action onSaveGame)
    {
        this.onSaveGame -= onSaveGame;
    }
}
