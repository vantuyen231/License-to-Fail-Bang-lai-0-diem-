using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIArrowWayPoint : TuyenMonoBehaviour
{
    [SerializeField] protected Transform playerPos;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] protected float heightOffset = 1.7f;
    [SerializeField] protected Transform targetDestinationPos;
    [SerializeField] protected Vector3 dirToTarget;
    [SerializeField] protected float rotationSpeed = 15f;
    [SerializeField] protected float distancePoint = 0f;
    public float DistancePoint => distancePoint;

    protected void OnEnable()
    {
        //GoToPointMission.OnPositionPlayer += GetPlayerPos ;
        DestinationSpawning.CallDestinationPos += GetDestinationPos;
        this.GetPlayerPos();

    }

    protected void OnDisable()
    {
        //GoToPointMission.OnPositionPlayer -= GetPlayerPos;
        DestinationSpawning.CallDestinationPos -= GetDestinationPos;
    }
    void Update()
    {
        this.SetPosArrow();
        this.RotationArrow();
    }

    protected void SetPosArrow()
    {
        if(playerPos == null) return;
        Vector3 arrowPos = playerPos.transform.position + new Vector3(0, heightOffset, 0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(arrowPos);

        rectTransform.position = screenPos;
    }

    protected void RotationArrow()
    {
        if (targetDestinationPos == null) return;

        Vector3 tranformPos = playerPos.transform.position;
        Vector3 targetPos = targetDestinationPos.transform.position;
        dirToTarget = targetPos - tranformPos;

        Vector3 flatDir = targetPos - tranformPos;
        flatDir.y = 0;

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        flatDir.Normalize();

        float targetAngle = Vector3.SignedAngle(camForward, flatDir, Vector3.up);

        Vector3 initialEuler = transform.localEulerAngles;
        Quaternion targetRot = Quaternion.Euler(initialEuler.x, initialEuler.y, -targetAngle);

        gameObject.transform.localRotation = Quaternion.Slerp(gameObject.transform.localRotation, targetRot, Time.deltaTime * rotationSpeed);

        distancePoint = Vector3.Distance(tranformPos, targetPos);
    }

    protected void GetPlayerPos()//Transform player, float index)
    {
        if(CarManager.Instance == null) return;
        playerPos = CarManager.Instance.transform;
    }

    protected void GetDestinationPos(Transform destinationPos)
    {
        targetDestinationPos = destinationPos;
    }

    protected override void LoadComponents()
    {
        this.LoadRectTransform();
    }

    protected virtual void LoadRectTransform()
    {
        if (this.rectTransform != null) return;
        rectTransform = GetComponentInChildren<RectTransform>();
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }
}
