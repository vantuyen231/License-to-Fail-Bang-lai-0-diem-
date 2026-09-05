using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : TuyenMonoBehaviour
{
    [SerializeField] protected UITopL topLeft;
    [SerializeField] protected UITopR topRight;
    [SerializeField] protected UITop topUI;
    [SerializeField] protected UIBottomR bottomRight;
    [SerializeField] protected UIBottomL bottomLeft;


    protected virtual void FixedUpdate()
    {
        this.UpdateUI();
    }

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUITopL();
        this.LoadUITopR();
        this.LoadUITop();
        this.LoadUIBottomL();
        this.LoadUIBottomR();
    }

    private void LoadUITopL()
    {
        if (topLeft != null) return;
        topLeft = GetComponentInChildren<UITopL>();
        Debug.Log(transform.name + ": LoadUITopL", gameObject);
    }

    private void LoadUITopR()
    {
        if (topRight != null) return;
        topRight = GetComponentInChildren<UITopR>();
        Debug.Log(transform.name + ": LoadUITopR", gameObject);
    }

    private void LoadUITop()
    {
        if (topUI != null) return;
        topUI = GetComponentInChildren<UITop>();
        Debug.Log(transform.name + ": LoadUITop", gameObject);
    }

    private void LoadUIBottomR()
    {
        if (bottomRight != null) return;
        bottomRight = GetComponentInChildren<UIBottomR>();
        Debug.Log(transform.name + ": LoadUIBottomR", gameObject);
    }

    private void LoadUIBottomL()
    {
        if (bottomLeft != null) return;
        bottomLeft = GetComponentInChildren<UIBottomL>();
        Debug.Log(transform.name + ": LoadUIBottomL", gameObject);
    }
    #endregion


    protected virtual void UpdateUI()
    {
        if(GameManager.Instance == null) return;
        this.topLeft.UpdateUITopLeft();
        //this.topRight.UpdateUITopR();
        this.topUI.UITopUpdate();
    }
}
