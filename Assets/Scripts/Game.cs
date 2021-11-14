using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private ResultPanel _winPanel;
    [SerializeField] private ResultPanel _losePanel;
    [SerializeField] private TowerSpawner _towerSpawner;
    [SerializeField] private CannonEventHandler _cannonEventHandler;

    private Tower _currentTower;

    public event System.Action OnGameWin;
    public event System.Action OnGameLose;

    public static bool IsGameOver { get; private set; }

    private void Awake()
    {
        IsGameOver = false;
        _towerSpawner.OnSpawn += OnSpawn;
        _cannonEventHandler.onBulletDeplete += OnBulletDeplete;
        OnGameWin += Win;
        OnGameLose += Lose;
    }

    private void OnSpawn(Tower tower)
    {
        _currentTower = tower;
        _currentTower.OnBulletCollision += OnBulletCollision;
    }

    private void OnBulletCollision()
    {
        IsGameOver = true;
        StartCoroutine(Wait(2f, () => OnGameWin?.Invoke()));
    }

    private void OnBulletDeplete(Bullet lastBullet)
    {
        IsGameOver = true;
        StartCoroutine(WaitUntilBulletForceSubTowerDestructiveForce(lastBullet));
    }

    private void Win()
    {
        _winPanel.Show();
    }

    private void Lose()
    {
        _losePanel.Show();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator WaitUntilBulletForceSubTowerDestructiveForce(Bullet bullet)
    {
        yield return new WaitWhile(() => bullet.Force < _currentTower.DestructiveForce * _currentTower.DestructiveForce && !IsGameOver && bullet.Velocity.y < 0);
        OnGameLose?.Invoke();
    }

    private IEnumerator Wait(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
    }

    private void OnDestroy()
    {
        _towerSpawner.OnSpawn -= OnSpawn;
        _cannonEventHandler.onBulletDeplete -= OnBulletDeplete;
        OnGameWin -= Win;
        OnGameLose -= Lose;
    }
}