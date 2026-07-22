using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITop : TuyenMonoBehaviour
{
    [SerializeField] protected UIStarManager uIStarManager;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIStarManager();

    }

    protected virtual void LoadUIStarManager()
    {
        if (uIStarManager != null) return;
        uIStarManager = GetComponentInChildren<UIStarManager>();
        Debug.Log(transform.name + ": LoadUIStarManager", gameObject);
    }

    public virtual void UITopUpdate()
    {
        if (uIStarManager != null)
        {
            this.uIStarManager.AddStar();
        }
    }
}
