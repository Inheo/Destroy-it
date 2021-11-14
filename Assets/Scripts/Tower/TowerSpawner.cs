using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower[] _towers;

    [SerializeField] private GameEventHandler _gameEventHandler;

    public Tower CurrentTower { get; private set; }

    private static int _towerIndex;

    public event System.Action<Tower> OnSpawn;

    private void Awake()
    {
        _gameEventHandler.onGameWin += OnGameWin;
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        CurrentTower = Instantiate(_towers[_towerIndex], transform.position, _towers[_towerIndex].transform.rotation);
        OnSpawn?.Invoke(CurrentTower);
    }

    private void OnGameWin()
    {
        _towerIndex++;
        _towerIndex = _towerIndex >= _towers.Length ? 0 : _towerIndex;
    }

    private void OnDestroy()
    {
        _gameEventHandler.onGameWin -= OnGameWin;
    }
}
