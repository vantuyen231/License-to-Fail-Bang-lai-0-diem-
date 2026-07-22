using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TextAbstract : TuyenMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected  virtual void LoadText()
    {
        if(textMeshProUGUI != null) return;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadText", gameObject);
    }
}
