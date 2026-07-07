using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitData", menuName = "GameData/HittableObjectData")]
public class HitObjectDataSO : ScriptableObject
{
    public HitObjectType hitObjectType;
    public string hitObjectName;

    public int hitCount;

}
