using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _destructiveForce = 4.5f;
    private Rigidbody[] _childRigidbodies;

    public event System.Action OnBulletCollision;

    public float DestructiveForce => _destructiveForce;

    void Start()
    {
        _childRigidbodies = GetComponentsInChildren<Rigidbody>();

        SetChildIsKinematic(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.Force < _destructiveForce * _destructiveForce) return;

            SetChildIsKinematic(false);

            OnBulletCollision?.Invoke();
        }
    }

    private void SetChildIsKinematic(bool isKinematic)
    {
        foreach (var rigidbody in _childRigidbodies)
        {
            rigidbody.isKinematic = isKinematic;
        }
    }
}
