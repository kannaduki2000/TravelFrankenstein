using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCableArray : MonoBehaviour
{
    public GameObject[] pointArray;

    void Start()
    {
        pointArray = new GameObject[gameObject.transform.childCount];
        for (int n = 0; n < gameObject.transform.childCount; n++)
        {
            // �e�o�ߒn�_�̏���z��Ɋi�[
            pointArray[n] = gameObject.transform.GetChild(n).gameObject;
        }
    }
}
