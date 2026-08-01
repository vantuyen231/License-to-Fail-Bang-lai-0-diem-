using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : TuyenSingleton<MenuUI>
{
    [SerializeField] protected bool isShow = true;

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

    //public virtual void Toggle()
    //{
    //    if (this.isShow) this.Hide();
    //    else this.Show();
    //}
}
