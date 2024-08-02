using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarController : MonoBehaviour
{

    [SerializeField] Slider _animationSlider;
    [SerializeField] Slider _currentValueSlider;
    float _updateTime;
    const float _durationTime = 0.3f;//duration to animate slider again
    Coroutine _animateSliderCoroutine;
    void Start()
    {
        _updateTime = 0f;
        _animationSlider.value = _currentValueSlider.value;
        StartCoroutine(DebugCall());
    }
    public void SetValue(float value)
    {
        _currentValueSlider.value = value;
        if (_updateTime + _durationTime < Time.time)
        {
            if (_animateSliderCoroutine != null) StopCoroutine(_animateSliderCoroutine);
            _animateSliderCoroutine = StartCoroutine(IEnuSetValue(value));
        }
        _updateTime = Time.time;
    }
    IEnumerator IEnuSetValue(float value)
    {
        print(value);
        while (value != _animationSlider.value)
        {
            _animationSlider.value = Mathf.Lerp(_animationSlider.value, value, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator DebugCall()
    {
        while (true)
        {
            Debug.Log("Debug Call");
            yield return new WaitForSeconds(1f);
            SetValue(Random.Range(0f, 1f));
        }
    }
}
