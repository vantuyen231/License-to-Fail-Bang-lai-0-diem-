using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu (menuName = "GameData/CarPlayer Base")]
public class CarPlayerBaseDataSO : ScriptableObject
{
    public List<CarPlayerDataSO> allCars;

    private Dictionary<string, CarPlayerDataSO> carPlayerCollection;

    private void OnEnable()
    {
        this.InitializeCache();
    }
    private void InitializeCache()
    {
        carPlayerCollection = new Dictionary<string, CarPlayerDataSO>();

        foreach (var car in allCars)
        {
            if (car != null && !string.IsNullOrEmpty(car.carID))
            {
                if (!carPlayerCollection.ContainsKey(car.carID))
                {
                    carPlayerCollection.Add(car.carID, car);
                }
                else
                {
                    Debug.LogWarning($"Duplicate detection carID: {car.carID} in {car.nameCar}!");
                }
            }
        }
    }

    public CarPlayerDataSO GetCarByID(string carID)
    {
        if (carPlayerCollection == null)
        {
            InitializeCache();
        }

        return carPlayerCollection.TryGetValue(carID, out var carData) ? carData : null;
    }
}
