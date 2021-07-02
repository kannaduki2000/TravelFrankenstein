using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ŽÀ‹@‚Å‚ÍŽg‚í‚ñ
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
