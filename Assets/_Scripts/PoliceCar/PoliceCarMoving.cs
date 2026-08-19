using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarMoving : TuyenMonoBehaviour
{
    [SerializeField] protected CarManager carPlayer;

    protected override void Start()
    {
        base.Start();
        this.LoadComponents();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTargetPlayer();
    }

    protected virtual void LoadTargetPlayer()
    {
        if(carPlayer != null) return;
        carPlayer = FindObjectOfType<CarManager>();
        Debug.Log(transform.name + ": LoadTargetPlayer ", gameObject);
    }
}
