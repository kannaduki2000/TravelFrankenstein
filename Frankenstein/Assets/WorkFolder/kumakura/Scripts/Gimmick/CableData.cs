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
    public CablePoint point; // �J�n�n�_
    public int CableNum;     // �d���̔ԍ�


    void Start()
    {
        CableNum = cableArray.cableNum;
    }

    void Update()
    {
        
    }
}
