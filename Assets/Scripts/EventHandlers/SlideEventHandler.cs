using UnityEngine;

public class SlideEventHandler : MonoBehaviour
{
    [SerializeField] private SliderForce _sliderForce;
    
    public event System.Action<float> onSliderStop;

    private void Awake()
    {
        _sliderForce.OnSliderStop += OnSliderStop;
    }
    
    private void OnSliderStop(float value)
    {
        onSliderStop?.Invoke(value);
    }
    
    private void OnDestroy()
    {
        _sliderForce.OnSliderStop -= OnSliderStop;
    }
}