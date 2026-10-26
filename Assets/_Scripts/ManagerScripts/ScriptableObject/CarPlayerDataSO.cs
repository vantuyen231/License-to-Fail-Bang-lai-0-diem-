using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CarData", menuName = "GameData/CartableObjectData")]
public class CarPlayerDataSO : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject carPrefabs;

    [Header("Car Details:")]
    public string nameCar;
    public string carID;

    [Header("Cost")]
    public int costCar;

    [Header("Roll Car")]
    public int rollAngel;
    public int yallAngel;

    [Header("Drive")]
    public int motorForce;
    public int steerWheel;
    public int brakeForce;

}
