using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAbstract : TuyenMonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected TextMeshProUGUI textBtn;

    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
        this.LoadTextMeshProUGUI();
    }

    protected virtual void LoadButton()
    {
        if (button != null) return;
        button = GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }

    protected virtual void LoadTextMeshProUGUI()
    {
        if (textBtn != null) return;
        textBtn = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTextMeshProUGUI", gameObject);
    }

    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
