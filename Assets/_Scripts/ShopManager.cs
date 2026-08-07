using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : TuyenSingleton<ShopManager>
{
    [Header("ShopCar")]
    [SerializeField] protected int maxCar = 0;
    [SerializeField] protected int currentCar = 0;
    [SerializeField] protected string nameCar;
    public static Action OnCarChanged;

    public int CurrentCar => currentCar;
    public string NameCar => nameCar;

    public void SetMaxCar(int max)
    {
        this.maxCar = max;
    }

    public void NextCar()
    {
        currentCar++;
        if (currentCar > maxCar - 1)
        {
            currentCar = 0;
        }
        //Debug.Log("nextCar");
        OnCarChanged?.Invoke();
    }

    public void PrevCar()
    {
        currentCar--;
        if (currentCar < 0)
        {
            currentCar = maxCar - 1;
        }
        //Debug.Log("prevCar");
        OnCarChanged?.Invoke();
    }

    public virtual void SetNameCar(string car)
    {
        this.nameCar = car;
    }
}
