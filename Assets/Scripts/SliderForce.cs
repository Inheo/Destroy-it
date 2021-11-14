using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderForce : MonoBehaviour
{
    [SerializeField] private Animation _sliderAnimation;
    [SerializeField] private Slider _slider;

    [SerializeField] private float _waitDuration = 0.5f;

    [SerializeField] private CameraEventHandler _cameraEventHandler;

    public event Action<float> OnSliderStop;

    private void Start()
    {
        _cameraEventHandler.onCameraSwitch += OnCameraSwitch;
    }

    private void OnCameraSwitch()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        _slider.value = _slider.minValue;
        _sliderAnimation.gameObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        _sliderAnimation.Stop();

        yield return new WaitForSeconds(_waitDuration);
        _sliderAnimation.gameObject.SetActive(false);

        float halfMaxValue = _slider.maxValue * 0.5f;
        float t = _slider.value <= halfMaxValue ? _slider.value : halfMaxValue - (_slider.value - halfMaxValue);
        OnSliderStop?.Invoke(Mathf.Repeat(t, _slider.maxValue));
    }

    private void OnDestroy()
    {
        _cameraEventHandler.onCameraSwitch -= OnCameraSwitch;
    }
}
