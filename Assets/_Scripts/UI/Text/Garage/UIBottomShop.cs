using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIBottomShop : TuyenMonoBehaviour
{
    [SerializeField] protected CostCar costUI;
    [SerializeField] protected BuyBtn buyUI;
    [SerializeField] protected SelectGaraBtn selectGaraBtn;
    [SerializeField] protected EquippedGaraBtn equippedGaraBtn;
    public bool show = true;

    protected override void Start()
    {
        base.Start();
        this.UpdateStateBtn();
    }

    protected void OnEnable()
    {
        ShopManager.OnCarChanged += UpdateStateBtn;
    }

    protected void OnDisable()
    {
        ShopManager.OnCarChanged -= UpdateStateBtn;
    }

    #region (LoadComponents)
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCostCar();
        this.LoadBuyBtn();
        this.LoadSelectGaraBtn();
        this.LoadEquippedGaraBtn();
    }

    protected virtual void LoadCostCar()
    {
        if (costUI != null) return;
        costUI = GetComponentInChildren<CostCar>();
        Debug.Log(transform.name + ": LoadCostCar", gameObject);
    }

    protected virtual void LoadBuyBtn()
    {
        if (buyUI != null) return;
        buyUI = GetComponentInChildren<BuyBtn>();
        Debug.Log(transform.name + ": LoadBuyBtn", gameObject);
    }

    protected virtual void LoadSelectGaraBtn()
    {
        if (selectGaraBtn != null) return;
        selectGaraBtn = GetComponentInChildren<SelectGaraBtn>();
        Debug.Log(transform.name + ": LoadSelectGaraBtn", gameObject);
    }

    protected virtual void LoadEquippedGaraBtn()
    {
        if (equippedGaraBtn != null) return;
        equippedGaraBtn = GetComponentInChildren<EquippedGaraBtn>();
        Debug.Log(transform.name + ": LoadEquippedGaraBtn", gameObject);
    }
    #endregion

    protected void UpdateStateBtn()
    {
        bool isUnlocked = ShopManager.Instance.IsCurrentCarUnlocked();
        bool isEquipped = ShopManager.Instance.IsCurrentCarSelected();
        if (!isUnlocked)
        {
            this.SetCostCar();
            SetButtonState(true, true, false, false);
        }
        else if (!isEquipped)
        {
            SetButtonState(false, false, true, false);
        }
        else
        {
            SetButtonState(false, false, false, true);
        }
    }

    private void SetButtonState(bool cost, bool buy, bool select, bool equipped)
    {
        costUI.gameObject.SetActive(cost);
        buyUI.gameObject.SetActive(buy);
        selectGaraBtn.gameObject.SetActive(select);
        equippedGaraBtn.gameObject.SetActive(equipped);
    }

    private void SetCostCar()
    {
        int cost = ShopManager.Instance.CarStatsTest.costCar;
        costUI.UpdateCost(cost);
    }
}
