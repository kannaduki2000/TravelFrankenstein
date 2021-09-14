using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaterController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// コルーチンでどのぐらい処理を止めるか(default:0.5)
	[SerializeField]
	float stopTime;

	// プレイヤーをしまっておくやつ
	GameObject PlayerObj;
	// プレイヤーのスクリプトをしまっておくやつ
	TestPlayerController_2 PlayerScr;

	// test_player_controlloer の PlayerHp をしまっておくやつ
	int hp = 0;

	/*----------------------------------------------------------------------------------------------------*/
	// Start is called before the first frame update
	void Start()
	{
		PlayerObj = GameObject.Find("TestPlayer");									// オブジェクトを見つけてしまっちゃう
		PlayerScr = PlayerObj.GetComponent<TestPlayerController_2>();		// スクリプトを見つけてしまっちゃう
	}

	/*----------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// 水の部分に侵入したときに実行しちゃうよ～ん関数
	/// </summary>
	/// <param name="other">使わないにょ・。・</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		StartCoroutine("Drowned");
	}

	/// <summary>
	/// 水へいい感じに全身が浸るよう調整するコルーチン
	/// </summary>
	/// <returns></returns>
	IEnumerator Drowned()
	{
		hp = PlayerScr.PlayerHp;			// hp に PlayerHp をしまっちゃう

		yield return new WaitForSeconds(stopTime);			// stopTime 分、処理を止める

		hp -= PlayerScr.PlayerHp;			// hp を PlayerHp 分、減らしちゃう
		PlayerScr.PlayerHp = hp;			// PlayerHp に hp をしまっちゃう
	}
}
