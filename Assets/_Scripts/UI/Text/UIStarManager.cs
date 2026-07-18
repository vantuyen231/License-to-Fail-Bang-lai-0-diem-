using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStarManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<StarPlayer> starPlayers = new List<StarPlayer>();

    protected virtual void Update()
    {
        this.LoadStartStar();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadListStar();
    }

    protected virtual void LoadStartStar()
    {
        foreach(StarPlayer star in this.starPlayers)
        {
            star.gameObject.SetActive(false);
        }
    }

    protected virtual void LoadListStar()
    {
        if (starPlayers.Count > 0) return;
        this.starPlayers.AddRange(GetComponentsInChildren<StarPlayer>());
    }

    protected virtual void AddStar()
    {

    }
}
