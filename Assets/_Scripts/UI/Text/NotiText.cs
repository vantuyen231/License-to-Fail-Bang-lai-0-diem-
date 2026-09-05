using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotiText : TextAbstract
{
    [SerializeField] protected float displayDuration = 2.5f;
    [SerializeField] protected UIBottomR uiBottomR;
    private Coroutine hideCoroutine;

    protected override void Start()
    {
        if (uiBottomR.CanvasG != null) uiBottomR.CanvasG.alpha = 0f;
    }

    protected virtual void OnEnable()
    {
        GameManager.OnObjectHitNoti += ShowHitNoti;
    }

    protected virtual void OnDisable()
    {
        GameManager.OnObjectHitNoti -= ShowHitNoti;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(uiBottomR != null) return;
        uiBottomR = transform.GetComponentInParent<UIBottomR>();
        Debug.Log(gameObject.name + "LoadCanvasGroup", transform);
    }

    protected virtual void ShowHitNoti(HitObjectType hitType, int scoreHit, string nameHit)
    {
        string customMessage = SetTextNoti(hitType, scoreHit, nameHit);

        this.UpdateNotiText(customMessage);
        if (uiBottomR.CanvasG != null) uiBottomR.CanvasG.alpha = 1f;

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(AutoHideRoutine());
    }

    protected virtual string SetTextNoti(HitObjectType hitType, int scoreHit, string nameHit)
    {
        switch (hitType)
        {
            case HitObjectType.CarNPC:
                return $"<color=red>[VA CHAM]</color>\n Dam phai xe: <b>{nameHit}</b>\nTru: <color=yellow>-{scoreHit} diem</color>";
            case HitObjectType.Pedestrian:
                return $"<color=red>[VA CHAM]</color>\n Dam phai nguoi di bo \nTru: <color=yellow>-{scoreHit} diem</color>";

            default:
                return $"Va cham doi tuong (-{scoreHit} diem)";
        }
    }
    protected virtual void UpdateNotiText(string noti)
    {
        textMeshProUGUI.text = noti;
    }

    private IEnumerator AutoHideRoutine()
    {
        yield return new WaitForSeconds(displayDuration);
        if (uiBottomR.CanvasG != null) uiBottomR.CanvasG.alpha = 0f;
    }
}
