using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearLoop : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed;//回るスピード
    public bool ClockwiseRotation;//時計回り
    void Update()
    {
             if(ClockwiseRotation==false)
                 {
                     rotZ += Time.deltaTime * RotationSpeed;
                 }

             else
             {
                 rotZ += Time.deltaTime * RotationSpeed;
             }
             transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

}
