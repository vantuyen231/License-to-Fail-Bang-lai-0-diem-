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

    [Header("EngineSound")]
    public AudioClip engineSound_1;
    public AudioClip engineSound_2;
    public AudioClip engineSound_3;
    public AudioClip engineSound_4;
    public AudioClip engineSound_5;

    [Header("HornCarSound")]
    public AudioClip hornCarSound;
    
}
