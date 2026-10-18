using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDestinationWP : TuyenMonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] protected Transform target;
    [SerializeField] protected float heightOffset = 1.7f;

    [SerializeField] protected UIDistanceDestination distanceText;
    [SerializeField] protected UIWayPoint wayPointUI;

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

        if (wayPointUI != null && wayPointUI.ArrowWayPoint != null)
        {
            float currentDistance = wayPointUI.ArrowWayPoint.DistancePoint;

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
        this.LoadUIWayPoint();
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

    protected virtual void LoadUIWayPoint()
    {
        if (this.wayPointUI != null) return;
        wayPointUI = GetComponentInParent<UIWayPoint>();
        Debug.Log(transform.name + ": LoadUIWayPoint", gameObject);
    }
}
