using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : TuyenMonoBehaviour
{
    [SerializeField] protected List<CityCtrl> cityCtrls = new List<CityCtrl>();




    protected virtual void OnTriggerEnter(Collider other)
    {
        AreaCollider areaCollider = other.GetComponent<AreaCollider>();
        if (areaCollider != null)
        {
            areaCollider.TriggerMapEnter();
            this.AddCity(other);
        }

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        AreaCollider areaCollider = other.GetComponent<AreaCollider>();
        if(areaCollider != null)
        {
            areaCollider.TriggerMapExit();
            this.RemoveCity(other);
        }

    }

    protected virtual void AddCity(Collider other)
    {
        AreaCollider areaCollider = other.GetComponent<AreaCollider>();
        if (areaCollider == null) return;
        CityCtrl cityCtrl = areaCollider.GetComponentInParent<CityCtrl>();
        this.cityCtrls.Add(cityCtrl);
        Debug.Log(areaCollider );
    }

    protected virtual void RemoveCity(Collider other)
    {
        CityCtrl cityCtrl = other.GetComponentInParent<CityCtrl>();
        if(cityCtrl == null) return;
        this.cityCtrls.Remove(cityCtrl);
    }
}
