using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuUI : TuyenSingleton<MenuUI>
{
    [SerializeField] protected bool isShow = true;
    [SerializeField] protected AudioMixer audioMixer;
    [SerializeField] protected float multiAudio = 25;

    [Header("Back ground Slider")]
    [SerializeField] protected Slider bgmSlider;
    [SerializeField] protected string bgmParametr;

    [Header("Sound Effects Slider")]
    [SerializeField] protected Slider sfxSlider;
    [SerializeField] protected string sfxParametr;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    public void BGMSetValue(float value)
    {
        float newValue = Mathf.Log10(value) * multiAudio;
        audioMixer.SetFloat(bgmParametr, newValue);
        
    }

    public void SFXSetValue(float value)
    {
        float newValue = Mathf.Log10(value) * multiAudio;
        audioMixer.SetFloat(sfxParametr, newValue);

    }

    public virtual void Hide()
    {
        isShow = false;
        gameObject.SetActive(isShow);
    }

    public virtual void Show()
    {
        isShow = true;
        gameObject.SetActive(isShow);
    }

    private void OnEnable()
    {
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, 0.6f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParametr, 0.6f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(bgmParametr, bgmSlider.value);
        PlayerPrefs.SetFloat(sfxParametr, sfxSlider.value);
    }

    public void LoadUpVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, 0.6f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParametr, 0.6f);
        if (audioMixer != null)
        {
            if (!string.IsNullOrEmpty(bgmParametr))
            {
                float newValue = Mathf.Log10(bgmSlider.value) * multiAudio;
                audioMixer.SetFloat(bgmParametr, newValue);
            }

            if (!string.IsNullOrEmpty(sfxParametr))
            {
                float newValue = Mathf.Log10(sfxSlider.value) * multiAudio;
                audioMixer.SetFloat(sfxParametr, newValue);
            }
        }
        Debug.Log("do " + transform);
    }
    //public virtual void Toggle()
    //{
    //    if (this.isShow) this.Hide();
    //    else this.Show();
    //}
}
