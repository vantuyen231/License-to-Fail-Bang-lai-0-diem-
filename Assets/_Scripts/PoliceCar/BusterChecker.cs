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

    private Coroutine checkCoroutine;

    public static event Action<bool> OnCheckPlayerBustedStatus;

    protected void OnEnable()
    {
        checkCoroutine = StartCoroutine(CheckRoutine());
    }
    protected void OnDisable()
    {
        if (checkCoroutine != null) StopCoroutine(checkCoroutine);
        OnCheckPlayerBustedStatus?.Invoke(false);
    }
    protected virtual void CheckPlayer()
    {
        int playerBuster = Physics.OverlapSphereNonAlloc(transform.position, radiosCheckBuster, hitPlayer, targetCheck);
        Debug.Log("playerBuster "+playerBuster);
        if (playerBuster > 0)
        {
            Rigidbody playerRb = hitPlayer[0].attachedRigidbody;
            Debug.Log("playerRb: " + playerRb);
            if (playerRb != null)
            {
                float speedKmh = playerRb.velocity.magnitude * 3.6f;
                if (speedKmh < maxBustedSpeedKmh)
                {
                    OnCheckPlayerBustedStatus?.Invoke(true);
                    return;
                }
            }
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
