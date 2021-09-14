using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCableArray : MonoBehaviour
{
    public GameObject[] pointArray;
    public int cableNum;

    void Start()
    {
        pointArray = new GameObject[gameObject.transform.childCount];
        for (int n = 0; n < gameObject.transform.childCount; n++)
        {
            // 各経過地点の情報を配列に格納
            pointArray[n] = gameObject.transform.GetChild(n).gameObject;
        }
    }
}
