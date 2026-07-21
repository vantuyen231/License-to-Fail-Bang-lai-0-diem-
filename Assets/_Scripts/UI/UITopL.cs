using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UITopL : TuyenMonoBehaviour
{
    [SerializeField] protected ScoreLicense scoreLicense;

    [SerializeField] protected StatusManager statusManager;


    protected virtual void FixedUpdate()
    {
        this.UpdateUITopLeft();
    }
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

    public virtual void UpdateUITopLeft()
    {
        int score = GameManager.Instance.CurrentScore;
        int status = GameManager.Instance.CurrentStatus;

        if (scoreLicense == null && statusManager == null) return;
        this.scoreLicense.SetScoreText(score.ToString());
        this.statusManager.SetStatusPlayer(status);
    }
}
