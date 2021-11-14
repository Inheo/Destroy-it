using UnityEngine;

public class CameraEventHandler : MonoBehaviour
{
    [SerializeField] private CameraSwitcher _cameraSwitcher;
    public event System.Action onCameraSwitch;

    private void Awake()
    {
        _cameraSwitcher.OnCameraSwitch += OnCameraSwitch;
    }

    private void OnCameraSwitch()
    {
        onCameraSwitch?.Invoke();
    }

    private void OnDestroy()
    {
        _cameraSwitcher.OnCameraSwitch -= OnCameraSwitch;
    }
}