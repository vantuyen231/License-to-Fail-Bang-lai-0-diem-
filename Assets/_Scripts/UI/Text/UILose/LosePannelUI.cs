using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePannelUI : TuyenMonoBehaviour
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
    public virtual void HidePannelLose()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    public virtual void ShowPannelLose()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
