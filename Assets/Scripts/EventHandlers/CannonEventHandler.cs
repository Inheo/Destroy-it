using UnityEngine;

public class CannonEventHandler : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    public event System.Action<Bullet> onBulletDeplete;

    private void Awake()
    {
        _cannon.OnBulletDeplete += OnBulletDeplete;
    }

    private void OnBulletDeplete(Bullet lastBullet)
    {
        onBulletDeplete?.Invoke(lastBullet);
    }
    
    private void OnDestroy()
    {
        _cannon.OnBulletDeplete -= OnBulletDeplete;
    }
}