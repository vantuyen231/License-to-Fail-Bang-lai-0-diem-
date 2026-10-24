using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarData",menuName = "GameData/SoundCarSO")]
public class SoundCarPlayerSO : ScriptableObject
{
    [Header("Start Car Sound")]
    public AudioClip startCarSound;

    [Header("Brake Sound")]
    public AudioClip brakeCarSound;

    [Header("HornCarSound")]
    public AudioClip hornCarSound;

    [Header("EngineSound")]
    public AudioClip[] engineSound;

    [Header("Engine Configurations")]
    public float minRPM = 1000f;
    public float rpmStep = 500f;

}
