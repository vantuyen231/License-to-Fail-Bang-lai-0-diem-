using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyenMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        this.LoadComponents();
    }
    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }
    protected virtual void LoadComponents()
    {

    }

    public virtual void SetActive(bool status)
    {
        gameObject.SetActive(status);
    }

    protected virtual void ResetValue()
    {

    }
}
