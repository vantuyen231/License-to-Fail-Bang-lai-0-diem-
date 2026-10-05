using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : TuyenSingleton<LoseUI>
{
    [SerializeField] protected bool isShow;
    [SerializeField] protected LosePannelUI losePannelUI;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    protected virtual void OnEnable()
    {
        MissionManager.EndGame += UpdateLoseUI;
    }


    protected virtual void OnDisable()
    {
        MissionManager.EndGame -= UpdateLoseUI;
    }

    protected void UpdateLoseUI(bool stateGame, int completeMission, int maxMission)
    {
        if (!stateGame)
        {
            this.Show();
            return;
        }else
        {
            this.Hide();
        }
    }

    protected override void LoadComponents()
    {
        this.LoadLosePannelUI();
    }
    protected virtual void LoadLosePannelUI()
    {
        if (losePannelUI != null) return;
        losePannelUI = GetComponentInChildren<LosePannelUI>();
        Debug.Log(transform.name + ": LoadLosePannelUI", gameObject);
    }
    public virtual void Hide()
    {
        isShow = false;
        losePannelUI.HidePannelLose();
    }

    public virtual void Show()
    {
        isShow = true;
        losePannelUI.ShowPannelLose();
    }
}
