using System;
using UnityEngine;

public class ShootEventHandler : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;

    public event Action onShotEnd;

    private void Awake()
    {
        _shooter.OnShotEnd += OnShotEnd;
    }

    private void OnShotEnd()
    {
        onShotEnd?.Invoke();
    }
    
    private void OnDestroy()
    {
        _shooter.OnShotEnd -= OnShotEnd;
    }
}
