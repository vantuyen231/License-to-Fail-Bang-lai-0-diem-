using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreLicense : TuyenMonoBehaviour
{
    [SerializeField] protected CarManager carMng;
    [SerializeField] protected TextMeshProUGUI textMeshProUGUI;

    protected virtual void  LateUpdate()
    {
        this.UpdateScoreUI();
    }

    protected virtual void UpdateScoreUI()
    {
        string scorePlayer = carMng.PlayerScore.CurrentScore.ToString();
        textMeshProUGUI.text = scorePlayer;
    }
}
