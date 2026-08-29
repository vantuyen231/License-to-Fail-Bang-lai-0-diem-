using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : TuyenSingleton<WinUI>
{
    [SerializeField] protected bool isShow;
    [SerializeField] protected BonusText bonusText;
    [SerializeField] protected CoinText coinText; 
    [SerializeField] protected MissionText missionText;
    [SerializeField] protected VehicalText vehicalText;
    [SerializeField] protected PedestrialText peedestrialText;
    [SerializeField] protected ScoreText scoreText;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBonusText();
        this.LoadCoinText();
        this.LoadMissionText();
        this.LoadScoreText();
        this.LoadVehicalText();
        this.LoadPedestrialText();
    }

    protected virtual void LoadBonusText()
    {
        if (bonusText != null) return;
        bonusText = GetComponentInChildren<BonusText>();
        Debug.Log(transform.name + ": LoadBonusText", gameObject);
    }

    protected virtual void LoadCoinText()
    {
        if (coinText != null) return;
        coinText = GetComponentInChildren<CoinText>();
        Debug.Log(transform.name + ": LoadCoinText", gameObject);
    }

    protected virtual void LoadMissionText()
    {
        if (missionText != null) return;
        missionText = GetComponentInChildren<MissionText>();
        Debug.Log(transform.name + ": LoadMissionText", gameObject);
    }

    protected virtual void LoadVehicalText()
    {
        if (vehicalText != null) return;
        vehicalText = GetComponentInChildren<VehicalText>();
        Debug.Log(transform.name + ": LoadVehicalText", gameObject);
    }

    protected virtual void LoadPedestrialText()
    {
        if (peedestrialText != null) return;
        peedestrialText = GetComponentInChildren<PedestrialText>();
        Debug.Log(transform.name + ": LoadPedestrialText", gameObject);
    }

    protected virtual void LoadScoreText()
    {
        if (scoreText != null) return;
        scoreText = GetComponentInChildren<ScoreText>();
        Debug.Log(transform.name + ": LoadScoreText", gameObject);
    }

    public virtual void Hide()
    {
        isShow = false;
        gameObject.SetActive(isShow);
    }

    public virtual void Show()
    {
        isShow = true;
        this.UpdateLastScore();
        gameObject.SetActive(isShow);
    }

    protected virtual void UpdateLastScore()
    {

    }
}
