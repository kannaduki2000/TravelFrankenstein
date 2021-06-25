using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{

    public Vector3 StartPos; // 開始位置
    public Vector3 EndPos; 　// 終了位置
    public float time;       // 時間指定
    private Vector3 deltaPos;
    private float elapsedTime;
    private bool bStartToEnd = true;
    private float UpdateTimer = 0f;
    private float TimeRimit = 2.0f;
    private bool move = false;

    //private NavMeshAgent navMeshAgent;

    public Vector3 playerPos;
    public float moveSpeed = 1.0f;
    bool tossinFlag = false;


    private Rigidbody2D rd;
    // Start is called before the first frame update
    void Start()
    {

        //navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく

        // StartPosを初期位置に設定
        transform.position = StartPos;
        // １秒当たりの移動量を算出
        deltaPos = (EndPos - StartPos) / time;
        elapsedTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (tossinFlag)
        {
            float speed = moveSpeed * Time.deltaTime;
            Vector2.MoveTowards(transform.position, playerPos, speed);
            if (this.transform.position == playerPos)
            {
                Debug.Log("loopEnd");
                tossinFlag = false;
            }
            else
            {
                Debug.Log("loop");
                return;
            }
        }
        Debug.Log("looooooooop");
        if(move == false)
        {
            UpdateTimer += Time.deltaTime;
        }
        if(UpdateTimer >= TimeRimit)
        {
            move = true;
            UpdateTimer = 0;
        }
        // １秒当たりの移動量にTime.deltaTimeを掛けると1フレーム当たりの移動量となる
        if (move == true)
        {
            // Time.deltaTimeは前回Updateが呼ばれてからの経過時間
            transform.position += deltaPos * Time.deltaTime;
            // 往路復路反転用経過時間
            elapsedTime += Time.deltaTime;
        }
        // 移動開始してからの経過時間がtimeを超えると往路復路反転
        if(elapsedTime > time)
        {
            if(bStartToEnd)
            {
                
                // StartPos→EndPosだったので反転してEndPos→StartPosにする
                // 現在の位置がEndPosなのでStartPos - EndPosでEndPos→StartPosの移動量になる
                deltaPos = (StartPos - EndPos) / time;
                // 誤差があるとずれる可能性があるため念のためにオブジェクトの位置をEndPosに設定
                transform.position = EndPos;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // 上の逆
                deltaPos = (EndPos - StartPos) / time;
                transform.position = StartPos;
                transform.localScale = new Vector3(1, 1, 1);

            }
            // 往路復路のフラグ反転
            bStartToEnd = !bStartToEnd;
            // 往路復路反転用経過時間クリア
            elapsedTime = 0;
            move = false;
        }
    }




    // CollisionDetector.csのonTriggerStayにセットし、衝突中に実行される。
    public void OnDetectObject(Collider collider)
    {
        Debug.Log("start");
        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
            //playerPos = collider.transform.position;
            //enemy.StartCoroutine(Tossin());
            //navMeshAgent.destination = collider.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !tossinFlag)
        {
            tossinFlag = true;
            playerPos = collision.transform.position;
            //StartCoroutine(Tossin());
        }

    }

    //public IEnumerator Tossin()
    //{
    //    tossinFlag = true;
    //    Debug.Log("PlayerPos : " + playerPos);
    //    while (this.transform.position != playerPos)
    //    {
    //        Debug.Log("bbb");
    //        Vector2.MoveTowards(transform.position, playerPos, moveSpeed);
    //        yield return null;
    //    }
    //    tossinFlag = false;
    //    yield break;
    //}

    // 使い分けができる便利な関数
    //public override void Following()
    //{
    //    int a = 0;
    //}

}

// エネミー自身のテリトリー間を行き来する〇
// 待機時間あり（2秒間）〇
// フランケンが近づいてきたら突進する
// フランケンの視線察知はエネミーを中心にする（エネミーが動いたら視線察知も一緒に動く）多分〇
// 突進成功したら４秒間待機　そのあと自分のテリトリーに戻る
// ４秒間待機後、視線察知内にフランケンがいたらフランケンに再び突進
// 突進した場所にフランケンがいなかったら２秒間待機して自分のテリトリーに戻る
// 
