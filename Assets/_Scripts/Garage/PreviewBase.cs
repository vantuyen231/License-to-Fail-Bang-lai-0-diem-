using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBase : TuyenMonoBehaviour
{ 
    [SerializeField] protected float currentSpin = 10f;
    [SerializeField] protected int indexCar = 0;
    [SerializeField] protected int index = 0;

    [SerializeField] protected string nameC;

    [SerializeField] protected List<CarPlayer> carPlayers = new List<CarPlayer>();


    protected override void Start()
    {
        this.ShowCar(indexCar);
        if(ShopManager.Instance != null)
        {
            ShopManager.Instance.SetMaxCar(carPlayers.Count);
        }
    }
    protected virtual void FixedUpdate()
    {
        this.SpinBase();
        this.GetCurrentCarData();
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

    protected virtual void OnEnable()
    {
        ShopManager.OnCarChanged += this.UpdateDisplayedCar;
    }

    protected virtual void OnDisable()
    {
        ShopManager.OnCarChanged -= this.UpdateDisplayedCar;

    }

    public virtual void ShowCar(int index)
    {

        for(int i = 0;  i < carPlayers.Count; i++)
        {
            carPlayers[i].gameObject.SetActive(i == index);

        }
    }

    public virtual void UpdateDisplayedCar()
    {
        index = ShopManager.Instance.CurrentCar;
        ShowCar(index);
        Debug.Log("Base");
    }


    public virtual void GetCurrentCarData()
    {
        if (this.index < 0 || this.index >= this.carPlayers.Count) return;

        nameC = this.carPlayers[this.index].CarP.nameCar;
    }
}
