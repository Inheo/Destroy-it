using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float Force => _rigidbody.velocity.magnitude;
    public Vector3 Velocity => _rigidbody.velocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 direction, float forceValue, float force)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * forceValue * force, ForceMode.Impulse);
    }
}