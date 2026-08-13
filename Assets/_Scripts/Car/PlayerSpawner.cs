using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSpawner : TuyenMonoBehaviour
{
    [SerializeField] protected CarPlayerDataSO playerData;
    [SerializeField] protected CinemachineVirtualCamera virtualCamera;
    [SerializeField] protected CarManager carManager;

    public CarPlayerDataSO PlayerDataSO => playerData;
    
    protected override void Start()
    {
        GetCarData();
        this.LoadComponents();
        this.SpawnerCar();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVirtualCam();
        this.LoadCarManager();
    }

    protected virtual void LoadVirtualCam()
    {
        if (virtualCamera != null) return;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        Debug.Log(transform.name + ": LoadVirtualCam", gameObject);
    }

    protected virtual void LoadCarManager() 
    { 
        if (carManager != null) return;
        carManager = GetComponentInChildren<CarManager>();
        Debug.Log(transform.name + ": LoadCarManager", gameObject);
    }

    protected virtual void GetCarData()
    {
        playerData = GameManager.Instance.CarPlayerData;
    }

    protected virtual void SetCamera()
    {
        if (virtualCamera == null) return ;
        if(carManager == null) return;
        carManager = GetComponentInChildren<CarManager>();
        virtualCamera.LookAt = carManager.LookAtPoint.transform;
        virtualCamera.Follow = carManager.transform;
        Debug.Log("Set Done");
    }

    protected virtual void SpawnerCar()
    {
        GameObject spawnCar = Instantiate(playerData.carPrefabs, transform.position, transform.rotation);
        spawnCar.transform.parent = transform;
        carManager = GetComponentInChildren<CarManager>();

        this.SetCamera();

    }
}
