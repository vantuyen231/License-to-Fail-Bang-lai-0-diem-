using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCtrl : MonoBehaviour
{
    [SerializeField] protected GameObject buildingHoldel;


    public virtual void OnArea()
    {
        this.SetBuildingsActive(true);
    }

    public virtual void OffArea()
    {
        this.SetBuildingsActive(false);
    }
    public void SetBuildingsActive(bool isAreaActive)
    {
        if(buildingHoldel != null)
        {
            buildingHoldel.SetActive(isAreaActive);
        }
    }
}
