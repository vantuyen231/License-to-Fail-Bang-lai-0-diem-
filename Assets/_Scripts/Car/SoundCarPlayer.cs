using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCarPlayer : TuyenMonoBehaviour
{
    [SerializeField] protected SoundCarPlayerSO soundCarPlayerSO; //{ get; private set; }
    public SoundCarPlayerSO SoundCarPlayerSO => soundCarPlayerSO;
}
