using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
enum EventCollider
{
    strat,
    end,
}
public class BusEventCollider : MonoBehaviour
{
    [SerializeField] private GameObject[] movingPoint = new GameObject[2];
    [SerializeField] private EventCollider eventCollider;
    [SerializeField] private SpriteRenderer playerCon;
    int playerNowLayer;

    private void Start()
    {
        playerNowLayer = playerCon.sortingOrder;
    }

    // Player����Tag�ŊǗ�����OntriggerEnter�Ƃ�����Ă�ł����Ă�������
    // Destination_Position_XXX�@�̈ʒu�K���Ȃ�Œ����I�l�V���X

    /// <summary>
    /// �o�X�ɏ������~�肽�肷�鏈��
    /// </summary>
    /// <param name="player">�悹����object</param>
    public void BusEvent(GameObject player)
    {
        // EventCollider�ɉ�����player���ړ�������
        player.transform.position = movingPoint[(int)eventCollider].transform.position;

        // EventCollider�̉�����player��Layer���^
        switch (eventCollider)
        {
            case EventCollider.strat:
                player.GetComponent<SpriteRenderer>().sortingOrder = 2;
                break;

            case EventCollider.end:
                player.GetComponent<SpriteRenderer>().sortingOrder = playerNowLayer;
                break;

            default:
                break;
        }
    }
}
