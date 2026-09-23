using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawnCtrl : TuyenSingleton<PoliceSpawnCtrl>
{
    [SerializeField] protected PoliceCarSpawner policeSpawner;
    [SerializeField] protected PoliceSpawning policeSpawning;

    public PoliceCarSpawner PoliceSpawner => policeSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoliceCarSpawner();
        this.LoadPoliceSpawning();
    }

    protected virtual void LoadPoliceCarSpawner()
    { 
        if (this.policeSpawner != null) return;
        policeSpawner = GetComponent<PoliceCarSpawner>();
        Debug.Log(transform.name + ": LoadPoliceCarSpawner", gameObject);
    }
    protected virtual void LoadPoliceSpawning()
    {
        if (this.policeSpawning != null) return;
        policeSpawning = GetComponent<PoliceSpawning>();
        Debug.Log(transform.name + ": LoadPoliceSpawning", gameObject);
    }

    protected virtual void OnEnable()
    {
        PlayerScore.ChangeStatusPlayer += UpdateSpawnPolice;
    }

    protected virtual void OnDisable()
    {
        PlayerScore.ChangeStatusPlayer -= UpdateSpawnPolice;
    }

    private void UpdateSpawnPolice(int license, int star, int status, bool isWanted)
    {
        if(policeSpawning != null) policeSpawning.CheckCurrentPolice(star);
    }
}
