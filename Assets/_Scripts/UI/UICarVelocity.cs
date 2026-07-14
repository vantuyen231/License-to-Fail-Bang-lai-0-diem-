using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICarVelocity : TuyenMonoBehaviour
{
    [SerializeField] protected CarController carController;
    [SerializeField] protected TextMeshProUGUI uiCarVelocity;

    protected virtual void LateUpdate()
    {
        this.CarVelocity();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarCtrl();
        this.LoadUIVelocity();
    }


    protected virtual void LoadCarCtrl()
    {
        if (carController != null) return;
        carController = GetComponentInParent<CarController>();
        Debug.Log(transform.name + ": LoadCarCtrl", gameObject);
    }

    protected virtual void LoadUIVelocity()
    {
        if(uiCarVelocity != null) return;
        uiCarVelocity = GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadUIVelocity", gameObject);
    }

    protected virtual void CarVelocity()
    {
        string speed = carController.PlayerSpeed.ToString();
        this.uiCarVelocity.text = speed;
    }
}
