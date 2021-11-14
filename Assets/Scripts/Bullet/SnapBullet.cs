using UnityEngine;

public class SnapBullet : MonoBehaviour
{
    [SerializeField] private float _snapDistance = 1;

    private Vector3 _startPosition;

    public event System.Action<Bullet> OnSnap;

    private Bullet _bullet;

    private void Start()
    {
        _startPosition = transform.position;
        _bullet = GetComponent<Bullet>();
    }

    private void OnMouseUp()
    {
        if (Cannon.Instance.CurrentBullet) return;
        Check();
    }

    private void Check()
    {
        bool isSnapDistance = (transform.position - Cannon.Instance.transform.position).magnitude <= _snapDistance * _snapDistance;

        if (isSnapDistance)
        {
            transform.position = Cannon.Instance.Point.position;
            transform.rotation = Cannon.Instance.Point.rotation;
            OnSnap?.Invoke(_bullet);
        }
        else
        {
            transform.position = _startPosition;
        }
    }
}
