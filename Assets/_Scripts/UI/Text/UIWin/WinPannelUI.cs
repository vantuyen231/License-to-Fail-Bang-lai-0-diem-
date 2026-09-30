using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPannelUI : TuyenMonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    protected override void LoadComponents()
    {
        this.LoadCanvasGroup();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (canvasGroup != null) return;
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log(transform.name + ": LoadCanvasGroup", gameObject);
    }
    public virtual void Hide()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    public virtual void Show()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}