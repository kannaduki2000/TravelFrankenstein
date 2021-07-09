using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PCController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	float distance = 0.0f;		// 距離をしまっちゃおうねしておく変数

	bool Y_ButtonPushFlag = false;		// Yボタンを押しちゃおうねしてもいいかのフラグ

	int player_hp = 0;			// 別のスクリプトの変数をしまっちゃおうねしておく変数
	int power_cn = 30;			// PCを点けたときの消費電力

	GameObject Player;		// プレイヤーをしまっちゃおうねしておくオブジェクト
	GameObject PC;				// PCをしまっちゃおうねしておくオブジェクト

	test_playerController PlayerScript;		// プレイヤーのスクリプトをしまっちゃおうねしておくやつ

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*↓↓↓どちらもテストのオブジェクトが入っているので、統合の際は差し替えをお願いします↓↓↓*/
		Player = GameObject.Find("test_player");			// プレイヤーのオブジェクトをしまっちゃおうね
		PC = GameObject.Find("test_PC");					// PCのオブジェクトをしまっちゃうおうね

		PlayerScript = Player.GetComponent<test_playerController>();		// プレイヤーの中にあるスクリプトをしまっちゃおうね
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		Y_ButtonFlagChange();

		Y_ButtonPush();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		 // 座標の取得と距離の測定する関数
	{
		// プレイヤーとPCの座標を取得しちゃおうね
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);       // プレイヤーとPCの距離を測ろうね
	}

	private void Y_ButtonFlagChange()			// Y_ButtonPushFlag を変える関数
	{
		if (distance <= 400)        // プレイヤーとPCの距離が400m以下ですか？
		{
			// YES
			Y_ButtonPushFlag = true;        // Yボタンを押すことを許した
		}
		else
		{
			// NO
		}
	}

	private void Y_ButtonPush()						// Y_Button を押したときの処理の関数 
	{
		/*↓↓↓コントローラーのYボタンではなく、テストでエンターキーとしているので、統合の際は差し替えをお願いします↓↓↓*/
		if (Y_ButtonPushFlag == true && Input.GetKeyDown(KeyCode.Return))       // Y_ButtonPushFlag が True 且つ、Yボタンが押されましたか？
		{
			// YES
			PlayerHPProc();
		}
		else
		{
			// NO
		}

	}

	private void PlayerHPProc()				// プレイヤーのHPに関する関数
	{
		player_hp = PlayerScript.HP;            // プレイヤーのHPをしまっちゃおうね
		player_hp -= power_cn;                  // プレイヤーのHPを消費電力分、減らす
		PlayerScript.HP = player_hp;            // 減ったプレイヤーのHPをvalueに入れる
	}

	/*----------------------------------------------------------------------------------------------------*/
}
