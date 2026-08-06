using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : TuyenMonoBehaviour
{
    [SerializeField] protected CarPlayerDataSO carPlayer;

    public CarPlayerDataSO CarP => carPlayer; 
}
