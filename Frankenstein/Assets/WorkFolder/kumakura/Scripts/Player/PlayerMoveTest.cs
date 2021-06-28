using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ŽÀ‹@‚Å‚ÍŽg‚í‚ñ
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
