using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : TuyenSingleton<WinUI>
{
    [SerializeField] protected bool isShow;
    [SerializeField] protected WinPannelUI winPannel;
    [SerializeField] protected BonusText bonusText;
    [SerializeField] protected CoinText coinText; 
    [SerializeField] protected MissionText missionText;
    [SerializeField] protected VehicalText vehicalText;
    [SerializeField] protected PedestrialText peedestrialText;
    [SerializeField] protected ScoreText scoreText;
    [SerializeField] protected LicenseText licenseText;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    protected virtual void OnEnable()
    {
        MissionManager.EndGame += UpdateLastScore;
        PlayerScore.ChangeStatusPlayer += UpdateLicense;
        PlayerScore.CallScorePlayer += ShowWinUI;
    }


    protected virtual void OnDisable()
    {
        MissionManager.EndGame -= UpdateLastScore;
        PlayerScore.ChangeStatusPlayer -= UpdateLicense;
        PlayerScore.CallScorePlayer -= ShowWinUI;
    }

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBonusText();
        this.LoadCoinText();
        this.LoadMissionText();
        this.LoadScoreText();
        this.LoadVehicalText();
        this.LoadPedestrialText();
        this.LoadWinPannelUI();
        this.LoadLicenseText();
    }
    protected virtual void LoadWinPannelUI()
    {
        if (winPannel != null) return;
        winPannel = GetComponentInChildren<WinPannelUI>();
        Debug.Log(transform.name + ": LoadWinPannelUI", gameObject);
    }
    protected virtual void LoadLicenseText()
    {
        if (licenseText != null) return;
        licenseText = GetComponentInChildren<LicenseText>();
        Debug.Log(transform.name + ": LoadLicenseText", gameObject);
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
    #endregion

    public virtual void Hide()
    {
        isShow = false;
        winPannel.HidePannelWin();
        //gameObject.SetActive(isShow);
    }

    public virtual void Show()
    {
        isShow = true;
        winPannel.ShowPannelWin();
        //this.UpdateLastScore();
        //gameObject.SetActive(isShow);
    }
    protected void ShowWinUI(float bonus, int score, int car, int npc, int coin)
    {
        int bonusInt = Mathf.RoundToInt(bonus);
        bonusText.UpdateBonus(bonusInt);
        scoreText.UpdateScore(score);
        vehicalText.UpdateVehical(car);
        peedestrialText.UpdatePedestrial(npc);
        coinText.UpdateCoin(coin);
        Debug.Log("WinUI");
    }
    protected void UpdateLicense(int license, int star, int status, bool isWanted)
    {
        licenseText.UpdateLicense(license);
    }
     
    protected virtual void UpdateLastScore(bool stateGame, int completeMission, int maxMission)
    {
        if (stateGame)
        {
            this.Show();
            missionText.UpdateMission(completeMission, maxMission);
        }
        else this.Hide();
    }
}
