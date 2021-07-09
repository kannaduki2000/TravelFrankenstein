using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_textController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	float distance = 0.0f;		// 距離をしまっちゃおうねしておく変数

	GameObject Player;		// プレイヤーをしまっちゃおうねしておくオブジェクト
	GameObject PC;              // PCをしまっちゃおうねしておくオブジェクト
	GameObject Textobj;		// テキストをしまっちゃおうねしておくオブジェクト

	Text Message;                   // テキストをしまっちゃおうねしておくテキスト

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*↓↓↓どちらもテストのオブジェクトが入っているので、統合の際は差し替えをお願いします↓↓↓*/
		Player = GameObject.Find("test_player");			// プレイヤーのオブジェクトをしまっちゃおうね
		PC = GameObject.Find("test_PC");					// PCのオブジェクトをしまっちゃうおうね
		Textobj = GameObject.Find("test_text");			// テキストのオブジェクトをしまっちゃおうね

		Message = Textobj.GetComponent<Text>();		// Textコンポネをしまっちゃおうね
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		TextChange();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		// 座標の取得と距離の測定する関数
	{       
		// プレイヤーとPCの座標を取得しちゃおうね
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);       // プレイヤーとPCの距離を測ろうね
	}

	private void TextChange()		// テキストの内容を変える関数
	{
		if (distance <= 400)        // プレイヤーとPCの距離が400m以下ですか？
		{
			// YES
			Message.text = "Yボタン:電源を入れる";
		}
		else
		{
			// NO
			Message.text = "";
		}
	}

	/*----------------------------------------------------------------------------------------------------*/
}
