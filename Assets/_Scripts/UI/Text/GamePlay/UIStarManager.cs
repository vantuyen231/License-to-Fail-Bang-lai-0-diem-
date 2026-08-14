using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStarManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<StarPlayer> starPlayers = new List<StarPlayer>();
    [SerializeField] protected int starCount = 0;

    protected override void Start()
    {
        base.Start();
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

    public virtual void AddStar()
    {
        if(starPlayers == null && starCount >4) return;

        starPlayers[starCount].gameObject.SetActive(true);


    }
}
