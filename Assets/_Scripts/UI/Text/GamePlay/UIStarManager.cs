using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIStarManager : TuyenMonoBehaviour
{
    [SerializeField] protected List<StarPlayer> starPlayers = new List<StarPlayer>();
    [SerializeField] protected int starCount = 0;
    [SerializeField] protected bool isShow;


    protected override void Start()
    {
        base.Start();
        this.LoadStartStar();
        this.Hide();

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

    public virtual void Hide()
    {
        isShow = false;
        gameObject.SetActive(isShow);

    }

    public virtual void Show()
    {
        isShow = true;
        gameObject.SetActive(isShow);
        this.ShowStar();
    }

    protected virtual void ShowStar()
    {
        if (GameManager.Instance == null) return;
        starCount = GameManager.Instance.CurrentStars;
        for (int i = 0; i < starPlayers.Count; i++)
        {
            starPlayers[i].gameObject.SetActive(i < starCount);
        }
    }
}
