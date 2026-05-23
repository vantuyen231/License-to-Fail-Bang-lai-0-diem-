using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCtrl : MonoBehaviour
{
    [SerializeField] protected GameObject buildingHoldel;
    //[SerializeField] protected bool isAreaActive;

    public void SetBuildingsActive(bool isAreaActive)
    {
        if(buildingHoldel != null)
        {
            buildingHoldel.SetActive(isAreaActive);
        }
    }
}
