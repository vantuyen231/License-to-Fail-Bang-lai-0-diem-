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
        if (uIStatusPlayers.Count > 0) return;
        uIStatusPlayers.AddRange(GetComponentsInChildren<UIStatusPlayer>());
    }

    protected virtual void StartStatus()
    {
        foreach (var player in uIStatusPlayers)
        {
            player.gameObject.SetActive(false);
        }
    }

    public virtual void SetStatusPlayer(int status)
    {
        if (this.uIStatusPlayers == null || this.uIStatusPlayers.Count == 0) return;

        this.statusNow = Mathf.Clamp(status, 0, this.uIStatusPlayers.Count - 1);

        for (int i = 0; i < this.uIStatusPlayers.Count; i++)
        {
            if (this.uIStatusPlayers[i] == null) continue;

            bool isActive = (i == this.statusNow);
            this.uIStatusPlayers[i].gameObject.SetActive(isActive);
        }
    }
}