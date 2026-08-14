using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<UIStatusPlayer> uIStatusPlayers = new List<UIStatusPlayer>();

    [SerializeField] protected int statusNow;

    protected override void Start()
    {
        base.Start();
        this.StartStatus();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIStatusPlayers();
    }

    protected virtual void LoadUIStatusPlayers()
    {
        uIStatusPlayers.AddRange(GetComponentsInChildren<UIStatusPlayer>());
    }

    protected virtual void StartStatus()
    {
        foreach(var player in uIStatusPlayers)
        {
            player.gameObject.SetActive(false);
        }
    }

    public virtual void SetStatusPlayer(int status)
    {
        statusNow = status;
        uIStatusPlayers[statusNow].gameObject.SetActive(true);
    }
}
