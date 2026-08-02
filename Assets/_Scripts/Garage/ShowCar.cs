using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCar : TuyenMonoBehaviour
{
    [SerializeField] protected PreviewBase preview;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPreview();
    }

    protected virtual void LoadPreview()
    {
        if(preview != null) return;
        preview = GetComponentInChildren<PreviewBase>();
        Debug.Log(transform.name + "LoadPreview", gameObject);
    }
}
