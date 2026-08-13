using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CarData", menuName = "GameData/CartableObjectData")]
public class CarPlayerDataSO : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject carPrefabs;

    [Header("NameCar")]
    public string nameCar;

    [Header("Cost")]
    public int costCar;

    [Header("Roll Car")]
    public int rollAngel;
    public int yallAngel;

    [Header("Drive")]
    public int motorForce;
    public int steerWheel;
    public int brakeForce;

    [Header("Player")]
    public bool isBuy;
}
