using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Point _firstForeshortening;
    [SerializeField] private Point _secondForeshortening;

    [SerializeField] private SnapEventHandler _snapEventHandler;
    [SerializeField] private ShootEventHandler _shootEventHandler;

    [SerializeField] private float _moveDuration = 1;
    [SerializeField] private float _rotateDuration = 1;

    public event System.Action OnCameraSwitch;

    private void Awake()
    {
        _snapEventHandler.onSnap += OnSnap;
        _shootEventHandler.onShotEnd += OnShotEnd;
    }

    private void OnSnap(Bullet bullet)
    {
        if (Game.IsGameOver) return;

        Move(_secondForeshortening);
        Rotate(_secondForeshortening);
        StartCoroutine(WaitUntilCameraMoveAndRoatateEnd());
    }

    private void OnShotEnd()
    {
        if (Game.IsGameOver) return;

        Move(_firstForeshortening);
        Rotate(_firstForeshortening);
    }

    private void Move(Point point)
    {
        StartCoroutine(transform.Move(point.Position, _moveDuration));
    }

    private void Rotate(Point point)
    {
        StartCoroutine(transform.Rotate(point.Rotation, _rotateDuration));
    }

    private IEnumerator WaitUntilCameraMoveAndRoatateEnd()
    {
        yield return new WaitForSeconds(Mathf.Max(_moveDuration, _rotateDuration));

        OnCameraSwitch?.Invoke();
    }

    private void OnDestroy()
    {
        _snapEventHandler.onSnap -= OnSnap;
        _shootEventHandler.onShotEnd -= OnShotEnd;
    }
}