using UnityEngine;

public class SnapEventHandler : MonoBehaviour
{
    [SerializeField] private SnapBullet[] _snapBullets;
    public event System.Action<Bullet> onSnap;

    private void Awake()
    {
        foreach (var snap in _snapBullets)
        {
            snap.OnSnap += OnSnap;
        }
    }
    private void OnSnap(Bullet bullet)
    {
        onSnap?.Invoke(bullet);
    }
    
    private void OnDestroy()
    {
        foreach (var snap in _snapBullets)
        {
            snap.OnSnap -= OnSnap;
        }
    }
}
