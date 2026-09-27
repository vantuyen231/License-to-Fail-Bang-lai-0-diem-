using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteMission : TuyenMonoBehaviour
{
    [SerializeField] protected UIScoreSuccess uIScoreSuccess;

    public UIScoreSuccess UIScoreSuccess => uIScoreSuccess;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIScoreSuccess();
    }
    private void LoadUIScoreSuccess()
    {
        if (uIScoreSuccess != null) return;
        uIScoreSuccess = GetComponentInChildren<UIScoreSuccess>();
        Debug.Log(transform.name + " LoadUIScoreSuccess:", gameObject);
    }



}
