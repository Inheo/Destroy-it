using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Transform _followPoint;
    [SerializeField] private Transform _lookPoint;

    public Vector3 Position => _followPoint.position;
    public Quaternion Rotation => _lookPoint.rotation;
}