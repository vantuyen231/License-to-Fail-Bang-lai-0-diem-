using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICarVelocity : TextAbstract
{
    [SerializeField] protected CarController carController;

    protected virtual void LateUpdate()
    {
        this.CarVelocity();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
    }


    protected virtual void LoadCarCtrl()
    {
        if (carController != null) return;
        carController = GetComponentInParent<CarController>();
        Debug.Log(transform.name + ": LoadCarCtrl", gameObject);
    }


    protected virtual void CarVelocity()
    {
        string speed = carController.PlayerSpeed.ToString();
        textMeshProUGUI.text = speed;
    }
}
