using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<UIStatusPlayer> uIStatusPlayers = new List<UIStatusPlayer>();

    [SerializeField] protected int statusNow;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIStatusPlayers();
    }

    protected virtual void LoadUIStatusPlayers()
    {
        uIStatusPlayers.AddRange(GetComponentsInChildren<UIStatusPlayer>());
    }

    public virtual void SetStatusPlayer(int status)
    {
        statusNow = status;
    }
}
