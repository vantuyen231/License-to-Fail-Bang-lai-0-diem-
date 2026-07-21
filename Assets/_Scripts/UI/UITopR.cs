using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITopR : TuyenMonoBehaviour
{
    [SerializeField] protected UICarVelocity carVelocity;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUICarVelocity();

    }

    protected virtual void LoadUICarVelocity()
    {
        if (carVelocity != null) return;
        carVelocity = GetComponentInChildren<UICarVelocity>();
        Debug.Log(transform.name + ": LoadUICarVelocity", gameObject);
    }


    public virtual void UpdateUITopR()
    {
        int velocity = GameManager.Instance.CurrentVelocity;
        this.carVelocity.SetCarVelocity(velocity.ToString());
    }
}
