using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Controller : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	[SerializeField]
	float dist_setting;			// どのくらいの距離でテキストが出たり、画像の切り替えができるようになるか決められるよ
	public float Dist_Setting			// dis_setting のプロパティ
	{
		get { return dist_setting; }
	}
	[SerializeField]
	Sprite PC_ON_Sprite;			// PCがONになったスプライトをしまっちゃおうねしておくやつ
	[SerializeField]
	Sprite PC_OFF_Sprite;			// PCがOFFになったスプライトをしまっちゃおうねしておくやつ

	float distance = 0.0f;		// 距離をしまっちゃおうねしておく変数

	bool Shikaku_ButtonPushFlag = false;		// □ボタンを押しちゃおうねしてもいいかのフラグ
	bool PCSwitchFlag = false;							// PCがオンかオフかのフラグ
	public bool pc_switch_flag			// PCSwitchFlag のプロパティ
	{
		get { return PCSwitchFlag; }
	}

	int player_hp = 0;			// 別のスクリプトの変数をしまっちゃおうねしておく変数
	int power_cn = 30;			// PCを点けたときの消費電力

	GameObject Player;		// プレイヤーをしまっちゃおうねしておくオブジェクト
	GameObject PC;              // PCをしまっちゃおうねしておくオブジェクト

	SpriteRenderer PC_SpriteRenderer;			// スプライトレンダラーを閉まっちゃおうねやつ

	test_playerController PlayerScript;		// プレイヤーのスクリプトをしまっちゃおうねしておくやつ

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*↓↓↓どちらもテストのオブジェクトが入っているので、統合の際は差し替えをお願いします↓↓↓*/
		Player = GameObject.Find("test_player");			// プレイヤーのオブジェクトをしまっちゃおうね
		PC = GameObject.Find("test_PC");					// PCのオブジェクトをしまっちゃうおうね

		PlayerScript = Player.GetComponent<test_playerController>();			// プレイヤーの中にあるスクリプトをしまっちゃおうね

		PC_SpriteRenderer = PC.GetComponent<SpriteRenderer>();			// PC のスプライトレンダラーをしまっちゃおうね
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		Shikaku_ButtonFlagChange();

		Shikaku_ButtonPush();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		 // 座標の取得と距離の測定する関数
	{
		// プレイヤーとPCの座標を取得しちゃおうね
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);       // プレイヤーとPCの距離を測ろうね
	}

	private void Shikaku_ButtonFlagChange()			// Shikaku_ButtonPushFlag を変える関数
	{
		if (distance <= dist_setting)		// プレイヤーとPCの距離がdist_setting以下ですか？
		{
			// YES
			Shikaku_ButtonPushFlag = true;		// □ボタンを押すことを許した
		}
		else
		{
			// NO
			Shikaku_ButtonPushFlag = false;		// □ボタンを押すことを許さない
		}
	}

	private void Shikaku_ButtonPush()						// □ボタンを押したときの処理の関数 
	{
		/*↓↓↓コントローラーの□ボタンとキーボードのエンターキーで反応するようになってます。統合のときは、なんかいい感じにしてください↓↓↓*/
		if (Shikaku_ButtonPushFlag == true && Input.GetKeyDown(KeyCode.Return))			// Shikaku_ButtonPushFlag が True 且つ、エンターキーが押されましたか？
		{
			// YES
			PlayerHPProc();

			/*なんかここら辺にいい感じにエフェクトを入れてください*/

			PCSwitch();
		}
		else if (Shikaku_ButtonPushFlag == true && Input.GetButtonDown("Fire1"))            // Shikaku_ButtonPushFlag が True 且つ、□が押されましたか？
		{
			// YES
			PlayerHPProc();

			/*なんかここら辺にいい感じにエフェクトを入れてください*/

			PCSwitch();
		}
		else
		{
			// NO
		}

	}

		private void PlayerHPProc()			// プレイヤーのHPに関する関数
		{
			if (PCSwitchFlag == false)          // PCSwitchFlag が false ですか？
			{
				// YES
				PlayerHPMinus();
			}
			else if (PCSwitchFlag == true)              // PCSwitchFlag が true ですか？
			{
				// NO
				PlayerHPPlus();
			}
		}

			private void PlayerHPMinus()			// プレイヤーのHPを減らす関数
		{
			player_hp = PlayerScript.HP;			// プレイヤーのHPをしまっちゃおうね
			player_hp -= power_cn;					// プレイヤーのHPを消費電力分、減らす
			PlayerScript.HP = player_hp;			// 減ったプレイヤーのHPをvalueに入れる
		}

			private void PlayerHPPlus()			// プレイヤーのHPを増やす関数
		{
			player_hp = PlayerScript.HP;			// プレイヤーのHPをしまっちゃおうね
			player_hp += power_cn;				// プレイヤーのHPを消費電力分、増やす(元に戻す)
			PlayerScript.HP = player_hp;			// 増えたプレイヤーのHPをvalueに入れる
		}

		private void PCSwitch()			// PCの電源に関する関数
		{
			if (PCSwitchFlag == false)          // PCSwitchFlag が false ですか？
			{
				// YES
				PCSwitch_ON();
			}
			else if (PCSwitchFlag == true)          // PCSwitchFlag が true ですか？
			{
				// YES
				PCSwitch_OFF();
			}
		}

			private void PCSwitch_ON()			// PCに電源が入る関数
		{
				PC_SpriteRenderer.sprite = PC_ON_Sprite;            // PC_ON_Sprite にしまっちゃおうねしておいたスプライトを PC_SpriteRenderer のスプライトに入れる
				PCSwitchFlag = true;
		}

			private void PCSwitch_OFF()			// PCの電源が切れる関数
		{
				PC_SpriteRenderer.sprite = PC_OFF_Sprite;            // PC_ON_Sprite にしまっちゃおうねしておいたスプライトを PC_SpriteRenderer のスプライトに入れる
				PCSwitchFlag = false;
		}

	/*----------------------------------------------------------------------------------------------------*/
}
