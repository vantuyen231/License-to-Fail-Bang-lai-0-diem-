using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] protected string bgmSound;
    private void Start()
    {
        AudioManager.Instance.StarBgm(bgmSound);
    }


}
