using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private const string SHOT_PARAMETR = "Shot";

    [SerializeField] private SlideEventHandler _slideEventHandler;

    [SerializeField] private Cannon _cannon;

    [SerializeField] private TowerSpawner _towerSpawner;

    [SerializeField] private Animator _cannonAnimator;

    [SerializeField] private GameObject _vfx;

    [SerializeField] private float _cannonForce = 1000f;

    public event System.Action OnShotEnd;

    private void Awake()
    {
        _slideEventHandler.onSliderStop += OnShot;
    }

    private void OnShot(float sliderValue)
    {
        _cannonAnimator.SetTrigger(SHOT_PARAMETR);
        Instantiate(_vfx, Cannon.Instance.Point);

        Bullet bullet = Cannon.Instance.CurrentBullet;
        bullet.Shoot(bullet.transform.forward, sliderValue, _cannonForce);
    }
    private void OnAnimFinished()
    {
        StartCoroutine(WaitWhileBulletForceSubTowerDestructiveForce());
    }

    private IEnumerator WaitWhileBulletForceSubTowerDestructiveForce()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Cannon.Instance.CurrentBullet.Force < _towerSpawner.CurrentTower.DestructiveForce * _towerSpawner.CurrentTower.DestructiveForce);
        OnShotEnd?.Invoke();
    }

    private void OnDestroy()
    {
        _slideEventHandler.onSliderStop += OnShot;
    }
}
