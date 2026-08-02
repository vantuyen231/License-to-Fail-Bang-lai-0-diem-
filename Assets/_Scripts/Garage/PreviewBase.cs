using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBase : TuyenMonoBehaviour
{ 
    [SerializeField] protected float currentSpin = 10f;
    [SerializeField] protected int indexCar = 0;

    [SerializeField] protected List<CarPlayer> carPlayers = new List<CarPlayer>();

    protected override void Start()
    {
        this.ShowCar(indexCar);
    }
    protected virtual void FixedUpdate()
    {
        this.SpinBase();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCar();
    }

    protected virtual void SpinBase()
    {
        float speedSpin = currentSpin * Time.deltaTime;
        transform.Rotate(0, speedSpin,0);
    }

    protected virtual void LoadCar()
    {
        if (carPlayers.Count > 0) return;

        foreach (Transform player in transform)
        {
            CarPlayer classPrefab = player.GetComponent<CarPlayer>();
            if (classPrefab != null)
            {
                carPlayers.Add(classPrefab);
            }
        }
    }

    public virtual void ShowCar(int index)
    {

        for(int i = 0;  i < carPlayers.Count; i++)
        {
            carPlayers[i].gameObject.SetActive(i == index);
        }
    }

    public virtual void NextCar()
    {
        int index = GameManager.Instance.CurrentCar;
        ShowCar(index);
        Debug.Log("Base");
    }
}
