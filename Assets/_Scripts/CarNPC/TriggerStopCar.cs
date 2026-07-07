using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStopCar : TuyenMonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        CarNPCCtrl carNPCCtrl = other.gameObject.GetComponentInParent<CarNPCCtrl>();

        if (carNPCCtrl != null)
        {
            Debug.Log(carNPCCtrl.gameObject);
        }


        BodyCar player = other.gameObject.GetComponentInParent<BodyCar>();
        if (player != null)
        {
            Debug.Log(player.gameObject);

        }

    }
}
