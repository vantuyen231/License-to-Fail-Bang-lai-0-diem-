using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class SliderShopAbstrast : TuyenMonoBehaviour
{
    [SerializeField] protected Slider slider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSilder();
    }

    protected virtual void LoadSilder()
    {
        if (slider != null) return;
        slider = GetComponent<Slider>();

        if (slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = 300;
        }
        Debug.Log(transform.name + ": LoadSilder", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.OnCarChangedHandler();
    }
    protected virtual void OnEnable()
    {
        ShopManager.OnCarChanged += this.OnCarChangedHandler;

    }

    protected virtual void OnDisable()
    {
        ShopManager.OnCarChanged -= this.OnCarChangedHandler;

    }

    private void OnCarChangedHandler()
    {
        if (ShopManager.Instance == null || ShopManager.Instance.CarStatsTest == null) return;
        this.UpdateCarStats();
    }

    protected virtual void UpdateCarStats()
    {
    }
}
