using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] private Game _game;
    public event System.Action onGameWin;

    private void Awake()
    {
        _game.OnGameWin += OnGameWin;
    }

    private void OnGameWin()
    {
        onGameWin?.Invoke();
    }
    
    private void OnDestroy()
    {
        _game.OnGameWin -= OnGameWin;
    }
}