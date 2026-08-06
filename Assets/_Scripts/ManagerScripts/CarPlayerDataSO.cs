using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CarData", menuName = "GameData/CartableObjectData")]
public class CarPlayerDataSO : ScriptableObject
{
    [Header("NameCar")]
    public string nameCar;

    [Header("Roll Car")]
    public int rollAngel;
    public int yallAngel;

    [Header("Drive")]
    public int motorForce;
    public int steerWheel;
    public int brakeForce;
}
