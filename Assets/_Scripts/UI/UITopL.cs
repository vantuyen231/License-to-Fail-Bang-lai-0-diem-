using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UITopL : TuyenMonoBehaviour
{
    [SerializeField] protected ScoreLicense scoreLicense;
    [SerializeField] protected StatusManager statusManager;
    [SerializeField] protected UIScoreMission scoreMission;


    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScoreLicense();
        this.LoadStatusManager();
        this.LoadUIScoreMission();
    }

    protected virtual void LoadScoreLicense()
    {
        if (scoreLicense != null) return;
        scoreLicense = GetComponentInChildren<ScoreLicense>();
        Debug.Log(transform.name + ": LoadScoreLicense", gameObject);
    }

    protected virtual void LoadStatusManager()
    {
        if (statusManager != null) return;
        statusManager = GetComponentInChildren<StatusManager>();
        Debug.Log(transform.name + ": LoadStatusManager", gameObject);
    }

    protected virtual void LoadUIScoreMission()
    {
        if (scoreMission != null) return;
        scoreMission = GetComponentInChildren<UIScoreMission>();
        Debug.Log(transform.name + ": LoadUIScoreMission", gameObject);
    }
    #endregion

    public virtual void UpdateUITopLeft(int license, int status)
    {
        if (scoreLicense == null && statusManager == null) return;
        this.scoreLicense.SetScoreText(license.ToString());
        this.statusManager.SetStatusPlayer(status);
    }

    public virtual void UpdateScoreUITL(int score)
    {
        this.scoreMission.UpdateScorePlayer(score);
    }
}
