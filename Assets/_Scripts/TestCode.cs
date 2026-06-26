using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    [Header("Int")]
    [SerializeField] protected int i1 = 2;
    [SerializeField] protected int i2 = 5;
    [SerializeField] protected int i3 = -3;

    [SerializeField] protected float d1 = 2.0f;
    [SerializeField] protected float d2 = 5.0f;
    [SerializeField] protected float d3 = 1.0f;



    


    


    void Start()
    {
        this.TinhSo();
    }

    protected void TinhSo()
    {
        float a = i1 + (i2 * i3);
        float b = i1 * (i2 + i3);
        float c = i1 / (i2 + i3);
        float e = i1 / i2 + i3;


        float f = i1 / i2;
        float j = i1 % i2;
        float g =f + i3;
        Debug.Log("i1/i2 " + f  );
        Debug.Log("i1 % i2 " + j);
        Debug.Log("i1 / i2 + i3" +g);
        Debug.Log("i1 / i2 + i3" + e);

    }
}
