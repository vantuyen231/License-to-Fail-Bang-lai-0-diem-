using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWayPoint : TuyenMonoBehaviour
{
    [SerializeField] protected RectTransform arrowWayPoint;
    [SerializeField] protected Transform targetDestination;

    [SerializeField] protected float rotationSpeed = 15f;

    protected virtual void Update()
    {
        if (targetDestination == null || !targetDestination.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }

        this.KeepFacingCamera();
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
        Vector3 playerPos = transform.position;
        Vector3 targetPos = targetDestination.position;
        Vector3 dirToTarget = targetPos - playerPos;

        Vector2 dir2D = new Vector2(dirToTarget.x, dirToTarget.z);
        Vector3 camForward = Camera.main.transform.forward;
        Vector2 camForward2D = new Vector2(camForward.x, camForward.z);

        float angle = Vector2.SignedAngle(camForward2D, dir2D);

        Quaternion targetRot = Quaternion.Euler(0, 0, -angle);
        arrowWayPoint.localRotation = Quaternion.Slerp(arrowWayPoint.localRotation, targetRot, Time.deltaTime * rotationSpeed);
    }
}
