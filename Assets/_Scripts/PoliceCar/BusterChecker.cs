using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterChecker : MonoBehaviour
{
    [SerializeField] protected float radiosCheckBuster = 2f;
    [SerializeField] protected LayerMask targetCheck;
    [SerializeField] private float maxBustedSpeedKmh = 15f;
    [SerializeField] protected Collider[] hitPlayer = new Collider[1];
    [SerializeField] protected bool isHitPlayer = false;

    private Coroutine checkCoroutine;

    public static event Action<BusterChecker, bool> OnCheckPlayerBustedStatus;

    protected void OnEnable()
    {
        checkCoroutine = StartCoroutine(CheckRoutine());
    }
    protected void OnDisable()
    {
        if (checkCoroutine != null) StopCoroutine(checkCoroutine);
        if (isHitPlayer)
        {
            isHitPlayer = false;
            OnCheckPlayerBustedStatus?.Invoke(this, false);
        }
    }
    protected virtual void CheckPlayer()
    {
        int playerBuster = Physics.OverlapSphereNonAlloc(transform.position, radiosCheckBuster, hitPlayer, targetCheck);
        bool isHit = false;
        Debug.Log("playerBuster "+playerBuster);
        if (playerBuster > 0 && hitPlayer != null)
        {
            Rigidbody playerRb = hitPlayer[0].attachedRigidbody;
            Debug.Log("playerRb: " + playerRb);
            if (playerRb != null)
            {
                float speedKmh = playerRb.velocity.magnitude * 3.6f;
                if (speedKmh < maxBustedSpeedKmh)
                {
                    isHit = true;
                }
            }
        }
        if (isHit != isHitPlayer)
        {
            isHitPlayer = isHit;
            OnCheckPlayerBustedStatus?.Invoke(this,isHitPlayer);
        }
    }

    protected virtual IEnumerator CheckRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            Debug.Log("Check Routine");
            CheckPlayer();
            yield return wait;
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiosCheckBuster);
    }
}
