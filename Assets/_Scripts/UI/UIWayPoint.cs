using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWayPoint : TuyenMonoBehaviour
{
    [SerializeField] protected RectTransform arrowWayPoint;
    [SerializeField] protected Transform targetDestination;

    [SerializeField] protected float rotationSpeed = 15f;
    [SerializeField] protected Vector3 dirToTarget;

    protected virtual void Update()
    {
        if (targetDestination == null || !targetDestination.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }

        //this.KeepFacingCamera();
        this.RotationArrow();
    }

    protected virtual void KeepFacingCamera()
    {
        if (Camera.main != null)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }

    [ContextMenu("RotationArrow")]
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

        Vector3 initialEuler = arrowWayPoint.localEulerAngles;
        Quaternion targetRot = Quaternion.Euler(initialEuler.x, initialEuler.y, -targetAngle);

        arrowWayPoint.localRotation = Quaternion.Slerp(arrowWayPoint.localRotation, targetRot, Time.deltaTime * rotationSpeed);
    }
}
