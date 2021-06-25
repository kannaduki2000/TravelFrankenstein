using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    public ElectricCable electricCable;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            electricCable.CablePointMove(gameObject);
        }
    }
}
