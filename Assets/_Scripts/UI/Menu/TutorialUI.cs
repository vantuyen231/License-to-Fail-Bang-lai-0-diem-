using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : TuyenSingleton<TutorialUI>
{
    [SerializeField] protected bool isShow;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    public virtual void Hide()
    {
        isShow = false;
        gameObject.SetActive(isShow);
    }

    public virtual void Show()
    {
        isShow = true;
        gameObject.SetActive(isShow);
    }
}
