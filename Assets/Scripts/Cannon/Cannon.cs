using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private SnapEventHandler _snapEventHandler;
    [SerializeField] private ShootEventHandler _shootEventHandler;
    [SerializeField] private Transform _point;
    [SerializeField] private Bullet[] _bullets;

    public System.Action<Bullet> OnBulletDeplete;

    public Bullet CurrentBullet { get; private set; }

    public static Cannon Instance { get; private set; }

    public Transform Point => _point;
    
    private int _countShoot = 0;

    private void Awake()
    {
        Instance = this;
        _snapEventHandler.onSnap += OnReloaded;
        _shootEventHandler.onShotEnd += OnShotEnd;
    }

    private void OnReloaded(Bullet bullet)
    {
        CurrentBullet = bullet;
    }

    private void OnShotEnd()
    {
        _countShoot++;

        if(_countShoot == _bullets.Length)
        {
            OnBulletDeplete?.Invoke(CurrentBullet);
        }

        CurrentBullet = null;
    }

    private void OnDestroy()
    {
        _snapEventHandler.onSnap -= OnReloaded;
        _shootEventHandler.onShotEnd -= OnShotEnd;
    }
}