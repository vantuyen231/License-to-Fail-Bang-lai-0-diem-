using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UITopL : TuyenMonoBehaviour
{
    [SerializeField] protected ScoreLicense scoreLicense;

    [SerializeField] protected StatusManager statusManager;


    protected virtual void FixedUpdate()
    {
        //this.UpdateUITopLeft(score,status);
    }

    #region LoadComponent
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScoreLicense();
        this.LoadStatusManager();
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
    #endregion

    public virtual void UpdateUITopLeft(int score, int status)
    {
        if (scoreLicense == null && statusManager == null) return;
        this.scoreLicense.SetScoreText(score.ToString());
        this.statusManager.SetStatusPlayer(status);
    }
}
