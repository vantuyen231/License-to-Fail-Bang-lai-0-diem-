using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestinationWP : TuyenMonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] protected Transform target;
    [SerializeField] protected float heightOffset;

    [SerializeField] protected UIDistanceDestination distanceText;
    [SerializeField] protected UIGoToPointMission goToPointUI;

    protected void OnEnable()
    {
        
        //BaseMission.UpdateStateMission += GetOnMission;
        DestinationSpawning.CallDestinationPos += GetDestinationPos;
    }

    protected void OnDisable()
    {
        //BaseMission.UpdateStateMission -= GetOnMission;
        DestinationSpawning.CallDestinationPos -= GetDestinationPos;
    }
    void Update()
    {
        if(target == null) return;
        Vector3 targetPos = target.position + new Vector3(0,heightOffset,0);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPos);
        if (screenPos.z < 0)
        {
            screenPos *= -1f;
        }
        rectTransform.position = screenPos;

        if (goToPointUI != null && goToPointUI.ArrowWayPoint != null)
        {
            float currentDistance = goToPointUI.ArrowWayPoint.DistancePoint;

            if (distanceText != null)
            {
                distanceText.DistanceDestination(Mathf.RoundToInt(currentDistance));
            }
        }
    }

    protected void GetDestinationPos(Transform destinationPos)
    {
        target = destinationPos;
    }

    //protected void UpdateDistanceText()
    //{
    //    float distance = goToPointUI.arrowWayPointUI.
    //    distanceText.DistanceDestination()
    //}

    protected override void LoadComponents()
    {
        this.LoadRectTransform();
        this.LoadUIDistanceDestination();
        this.LoadUIGoToPointMission();
    }

    protected virtual void LoadUIDistanceDestination()
    {
        if (this.distanceText != null) return;
        distanceText = GetComponentInChildren<UIDistanceDestination>();
        Debug.Log(transform.name + ": LoadUIDistanceDestination", gameObject);
    }

    protected virtual void LoadRectTransform()
    {
        if (this.rectTransform != null) return;
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }

    protected virtual void LoadUIGoToPointMission()
    {
        if (this.goToPointUI != null) return;
        goToPointUI = GetComponentInParent<UIGoToPointMission>();
        Debug.Log(transform.name + ": LoadUIGoToPointMission", gameObject);
    }
}
