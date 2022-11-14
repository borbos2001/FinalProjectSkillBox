
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    public string _voluemParameter = "MasterVoluem";
    public AudioMixer _mixer;
    public Slider _slider;

    private float _volumeValue;
    private const float _soundConst = 20f;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }
    private void HandleSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _soundConst;
        _mixer.SetFloat(_voluemParameter, _volumeValue);
    }
    void Start()
    {
         _volumeValue = PlayerPrefs.GetFloat(_voluemParameter, Mathf.Log10(_slider.value) * _soundConst);
        _slider.value = Mathf.Pow(10f, _volumeValue/ _soundConst);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_voluemParameter, _volumeValue);
    }

}
