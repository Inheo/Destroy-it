using UnityEngine;

public class DragBullet : MonoBehaviour
{
    [SerializeField] private Vector3 _maxPosition;
    [SerializeField] private Vector3 _minPosition;

    private float _distanceToCamera;

    private void OnMouseDown()
    {
        _distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
    }

    private void OnMouseDrag()
    {
        if (Cannon.Instance.CurrentBullet) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ClampVector3(ray.GetPoint(_distanceToCamera), _minPosition, _maxPosition);
        transform.position = rayPoint;
    }

    private Vector3 ClampVector3(Vector3 vector, Vector3 min, Vector3 max)
    {
        vector.x = Mathf.Clamp(vector.x, min.x, max.x);
        vector.y = Mathf.Clamp(vector.y, min.y, max.y);
        vector.z = Mathf.Clamp(vector.z, min.z, max.z);

        return vector;
    }
}