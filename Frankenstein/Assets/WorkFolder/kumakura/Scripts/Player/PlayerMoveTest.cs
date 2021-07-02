using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実機では使わん
/// </summary>
public class PlayerMoveTest : MonoBehaviour
{
    public ElectricCableController electricCableCon;
    [SerializeField] private bool startPoint = true;
    [SerializeField] private int id = 0;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            electricCableCon.CablePointMove(gameObject, id, startPoint);
        }
    }
}
