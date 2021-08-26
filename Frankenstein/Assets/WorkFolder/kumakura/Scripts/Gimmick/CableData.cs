using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CablePoint
{
    start,
    end,
}
public class CableData : MonoBehaviour
{
    [SerializeField] private ElectricCableArray cableArray;
    public CablePoint point; // 開始地点
    public int CableNum;     // 電線の番号


    void Start()
    {
        CableNum = cableArray.cableNum;
    }

    void Update()
    {
        
    }
}
