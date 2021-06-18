using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実機では使わん
/// </summary>
public class PlayerMoveTest : MonoBehaviour
{
    public ElectricCable electricCable;
    [SerializeField] private bool startPoint = true;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            electricCable.CablePointMove(gameObject, startPoint);
        }
    }
}
