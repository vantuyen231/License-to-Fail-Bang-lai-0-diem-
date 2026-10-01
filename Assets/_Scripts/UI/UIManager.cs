using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UIManager : TuyenMonoBehaviour
{
    [SerializeField] protected UITopL topLeft;
    [SerializeField] protected UITopR topRight;
    [SerializeField] protected UITop topUI;
    [SerializeField] protected UIMiddle middleUI;
    [SerializeField] protected UIBottomR bottomRight;
    [SerializeField] protected UIBottomL bottomLeft;

    protected virtual void OnEnable()
    {
        PlayerScore.ChangeStatusPlayer += UIPlayerUpdate;
        CarController.SpeedPlayer += UISpeedCar;
        PlayerScore.ScoreMissionPlayer += UIScoreM;
    }

    protected virtual void OnDisable()
    {
        PlayerScore.ChangeStatusPlayer -= UIPlayerUpdate;
        CarController.SpeedPlayer -= UISpeedCar;
        PlayerScore.ScoreMissionPlayer -= UIScoreM;

    }

    private void UISpeedCar(int velocity)
    {
        if (this.topRight != null) topRight.UpdateUITopR(velocity);
    }
    protected virtual void UIPlayerUpdate(int license, int star, int status, bool isWanted)
    {
        if(this.topLeft != null) topLeft.UpdateUITopLeft(license, status);
        if(this.topUI != null) topUI.UITopUpdate(star, isWanted);
    }

    protected virtual void UIScoreM(int score)
    {
        if(this.topLeft != null) topLeft.UpdateScoreUITL(score);
    }

    #region Load Components
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUITopL();
        this.LoadUITopR();
        this.LoadUITop();
        this.LoadUIMiddle();
        this.LoadUIBottomL();
        this.LoadUIBottomR();
    }

    private void LoadUIMiddle()
    {
        if (middleUI != null) return;
        middleUI = GetComponentInChildren<UIMiddle>();
        Debug.Log(transform.name + ": LoadUIMiddle", gameObject);
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


}
