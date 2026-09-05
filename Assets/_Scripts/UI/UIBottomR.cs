using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBottomR : TuyenMonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasG => canvasGroup;
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(canvasGroup != null ) return;
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log(gameObject.name + "LoadCanvasGroup",transform);
    }
}
