using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@�ł͎g���
/// </summary>
public class PlayerMoveTest : MonoBehaviour
{
    public ElectricCableController electricCableCon;
    [SerializeField] private bool startPoint = true;
    [SerializeField] private int id = 0;
    [SerializeField] private GameObject[] camera = new GameObject[2];

    void Start()
    {
        
    }

    void Update()
    {
        // �d���f�o�b�O
        if (Input.GetKeyDown(KeyCode.Return))
        {
            electricCableCon.CablePointMove(gameObject, id, startPoint);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            EventFlagManager.Instance.DumpAllFlag();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {

        }

        // �P�[�u���J�[�f�o�b�O
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventFlagManager.Instance.SetFlagState(EventFlagName.cableCarStart, true);
        }
    }
}
