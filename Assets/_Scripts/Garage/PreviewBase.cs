using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBase : TuyenMonoBehaviour
{ 
    [SerializeField] protected float currentSpin = 10f;

    protected virtual void FixedUpdate()
    {
        this.SpinBase();
    }

    protected virtual void SpinBase()
    {
        float speedSpin = currentSpin * Time.deltaTime;
        transform.Rotate(0, speedSpin,0);
    }
}
