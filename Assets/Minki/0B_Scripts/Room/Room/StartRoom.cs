using System;

public class StartRoom : Room
{
    public event Action OnGameStart;

    private string _chapterName;

    private void Start() {
        OnCameraMoveEnd += CameraMoveEndHandle;
    }

    private void CameraMoveEndHandle() {
        UIManager.Instance.ShowStageLabel(_chapterName);
    }

    public void GameStart() {
        Clear();
        OnGameStart?.Invoke();
    }
}
