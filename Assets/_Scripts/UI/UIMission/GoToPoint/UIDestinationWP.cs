using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestinationWP : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] protected Transform target;


    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        rectTransform.position = screenPos;
    }
}
