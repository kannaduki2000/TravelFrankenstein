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

    // Player側がTagで管理してOntriggerEnterとかから呼んであげてください
    // Destination_Position_XXX　の位置適当なんで調整オネシャス

    /// <summary>
    /// バスに乗ったり降りたりする処理
    /// </summary>
    /// <param name="player">乗せたいobject</param>
    public void BusEvent(GameObject player)
    {
        // EventColliderに応じてplayerを移動させる
        player.transform.position = movingPoint[(int)eventCollider].transform.position;

        // EventColliderの応じてplayerのLayerを蛙
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
