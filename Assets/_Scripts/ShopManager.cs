 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : TuyenSingleton<ShopManager>
{
    [Header("ShopCar")]
    [SerializeField] protected int maxCar = 0;
    [SerializeField] protected int currentCar = 0;
    [SerializeField] protected CarPlayerDataSO carStats;
    [SerializeField] protected bool isUse;
    [SerializeField] protected bool isUnlocked;
    [SerializeField] protected bool isSelected;
    public static Action OnCarChanged;

    public int CurrentCar => currentCar;
    public CarPlayerDataSO CarStatsTest => carStats;

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


    internal void CarStats(CarPlayerDataSO statsCar)
    {
        this.carStats = statsCar;
        if (this.carStats != null)
        {
            string currentCarID = this.carStats.carID;

            isUnlocked = GameManager.Instance.IsCarUnlocked(currentCarID);
            isSelected = GameManager.Instance.IsCarSelected(currentCarID);

            if (isSelected)
            {
                Debug.Log("(EQUIPPED): " + statsCar.nameCar);
            }
            else if (isUnlocked)
            {
                Debug.Log("(SELECT): " + statsCar.nameCar);
            }
            else
            {
                Debug.Log("(BUY): " + statsCar.costCar);
            }
        }
    }

    public virtual void SetUseCar()
    {
        this.SetIsUseCar();
        if (isUse)
        {
            GameManager.Instance.GetUseCar(carStats);
            Debug.Log("Set Done");
        }
        else
        {
            Debug.Log("Can't Select");
        }
    }

    public bool IsCurrentCarUnlocked()
    {
        if (carStats == null) return false;
        return GameManager.Instance.IsCarUnlocked(carStats.carID);
    }

    public bool IsCurrentCarSelected()
    {
        if (carStats == null) return false;
        return GameManager.Instance.IsCarSelected(carStats.carID);
    }

    public virtual void SetIsUseCar()
    {
        if(carStats.isBuy == true)
        {
            isUse = true;
        }
        else
        {
            isUse = false;
        }
    }
}
