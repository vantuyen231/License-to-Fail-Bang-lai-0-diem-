using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleNoDMission : TuyenMonoBehaviour
{
    [SerializeField] protected NumCountDown countDown;

    public NumCountDown CountDown => countDown;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNumCD();
    }

    protected virtual void LoadNumCD()
    {
        if (countDown != null) return;
        countDown = GetComponentInChildren<NumCountDown>();
        Debug.Log(transform.name + " LoadNumCD:", gameObject);
    }
}
