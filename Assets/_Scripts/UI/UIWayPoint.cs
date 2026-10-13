using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIWayPoint : TuyenMonoBehaviour
{
    [SerializeField] protected UIArrowWayPoint arrowWayPoint;
    [SerializeField] protected Transform targetDestination;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected bool isWayPoint = false;

    [SerializeField] protected float rotationSpeed = 15f;
    [SerializeField] protected Vector3 dirToTarget;

    protected virtual void Update()
    {
        if (!isWayPoint) return;
        //if (targetDestination == null || !targetDestination.gameObject.activeSelf)
        //{
        //    gameObject.SetActive(false);
        //    return;
        //}
        this.KeepFacingCamera();
        this.RotationArrow();
    }

    protected void OnEnable()
    {
        BaseMission.UpdateStateMission += GetOnMission;
        DestinationSpawning.CallDestinationPos += GetDestinationPos;
    }

    protected void OnDisable()
    {
        BaseMission.UpdateStateMission -= GetOnMission;
        DestinationSpawning.CallDestinationPos -= GetDestinationPos;
    }

    protected virtual void GetOnMission(MissionType missionType,MissionState state,float duration)
    {
        if (missionType == MissionType.ReachLocation && state == MissionState.ActiveGameplay)
        {
            isWayPoint = true;
            canvasGroup.alpha = 1;

        }
        else
        {
            isWayPoint = false;
            canvasGroup.alpha = 0;
        }
    }

    protected void GetDestinationPos(Transform destinationPos)
    {
        targetDestination = destinationPos;
    }

    protected virtual void KeepFacingCamera()
    {
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    protected void RotationArrow()
    {
        if (targetDestination == null || arrowWayPoint == null) return;

        Vector3 playerPos = transform.position;
        Vector3 targetPos = targetDestination.position;
        dirToTarget = targetPos - playerPos;

        Vector3 flatDir = targetPos - playerPos;
        flatDir.y = 0; 

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        flatDir.Normalize();

        float targetAngle = Vector3.SignedAngle(camForward, flatDir, Vector3.up);

        Vector3 initialEuler = arrowWayPoint.transform.localEulerAngles;
        Quaternion targetRot = Quaternion.Euler(initialEuler.x, initialEuler.y, -targetAngle);

        arrowWayPoint.transform.localRotation = Quaternion.Slerp(arrowWayPoint.transform.localRotation, targetRot, Time.deltaTime * rotationSpeed);
    }



    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRectTransform();
        this.LoadCanvasGroup();
    }
    protected virtual void LoadRectTransform()
    {
        if (this.arrowWayPoint != null) return;
        arrowWayPoint = GetComponentInChildren<UIArrowWayPoint>();
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log(transform.name + ": LoadCanvasGroup", gameObject);
    }
}
