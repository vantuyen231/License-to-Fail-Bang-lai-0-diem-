using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSpawning : TuyenMonoBehaviour
{
    [SerializeField] protected DestinationSpawnCtrl destinationSpawnCtrl;
    [SerializeField] protected DestinationManager destinationManager;
    [SerializeField] protected DestinationArea areaSpawnGoal;
    public DestinationSpawnCtrl DestinationSpawnCtrl => destinationSpawnCtrl;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDestinationSpawnCtrl();
        this.LoadDestinationManager();
    }
    protected virtual void LoadDestinationSpawnCtrl()
    {
        if (this.destinationSpawnCtrl != null) return;
        destinationSpawnCtrl = GetComponent<DestinationSpawnCtrl>();
        Debug.Log(transform.name + ": LoadDestinationSpawnCtrl", gameObject);
    }

    protected virtual void LoadDestinationManager()
    {
        if (this.destinationManager != null) return;
        destinationManager = FindAnyObjectByType<DestinationManager>();
        Debug.Log(transform.name + ": LoadDestinationManager", gameObject);
    }
    #endregion

    public void GetPointSpawnGoal(DestinationArea area)
    {
        areaSpawnGoal = area;
    }
}
