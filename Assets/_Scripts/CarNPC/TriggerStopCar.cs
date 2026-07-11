using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStopCar : TuyenMonoBehaviour
{
    [SerializeField] protected CarNPCMoving carNPCMoving;

    protected virtual void OnTriggerEnter(Collider other)
    {
        CarNPCCtrl carNPCCtrl = other.gameObject.GetComponentInParent<CarNPCCtrl>();

        if (carNPCCtrl != null)
        {
            Debug.Log(carNPCCtrl.gameObject);
            this.StopCarNPC();
        }


        BodyCar player = other.gameObject.GetComponentInParent<BodyCar>();
        if (player != null)
        {
            //Debug.Log(player.gameObject);
            this.StopCarNPC();
        }

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        CarNPCCtrl carNPCCtrl = other.gameObject.GetComponentInParent<CarNPCCtrl>();

        if (carNPCCtrl != null)
        {
            Debug.Log(carNPCCtrl.gameObject);
            this.MoveCar();
        }


        BodyCar player = other.gameObject.GetComponentInParent<BodyCar>();
        if (player != null)
        {
            //Debug.Log(player.gameObject);
            this.MoveCar();
        }

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCarNPCMoving();
    }

    protected virtual void LoadCarNPCMoving()
    {
        if (carNPCMoving != null) return;
        carNPCMoving = transform.parent.GetComponent<CarNPCMoving>();
        Debug.Log(transform.name + ": LoadCarNPCMoving", gameObject);
    }

    protected virtual void StopCarNPC()
    {
        if (carNPCMoving == null) return;
        
        this.carNPCMoving.SetStopByTrigger(true);
    }

    protected virtual void MoveCar()
    {
        if (carNPCMoving == null) return;
        this.carNPCMoving.SetStopByTrigger(false);
    }
}
