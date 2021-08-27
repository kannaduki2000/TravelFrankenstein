using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// プレハブをしまっておくやつ
	[SerializeField]
	GameObject GameoverUIPrefab;

	// インスタンスをしまっておくやつ
	GameObject GameoverUIInstance;
	// プレイヤーをしまっておくやつ
	GameObject PlayerObj;
	// スクリプトをしまっておくやつ
	TestPlayerController_2 PlayerScr;

	// TestPlayerControlloer_2 の PlayerHp をしまっておくやつ
	int hp = 0;

	// Gameoverか否かのフラグ
	bool gameoverFlag = false;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		PlayerObj = GameObject.Find("TestPlayer");											// オブジェクトを見つけてしまっちゃう
		PlayerScr = PlayerObj.GetComponent<TestPlayerController_2>();		// スクリプトを見つけてしまっちゃう
	}

	// Update is called once per frame
	void Update()
	{
		hp = PlayerScr.PlayerHp;			// hp に PlayerHp をしまっちゃう

		if(hp == 0 && gameoverFlag == false)			// hp が0で且つ、gameoverFlag は False ですか？
		{
			// YES
			GameoverUIInstance = GameObject.Instantiate(GameoverUIPrefab);
			Time.timeScale = 0f;			// ｻﾞ･ﾜｰﾙﾄﾞｯ！”世界”の時を止めるｯ！！
			gameoverFlag = true;			// gameoverFlag を true にする
		}
		else if(hp > 0)			// hp が 0 より上ですか？
		{
			// YES
			Time.timeScale = 1f;			// 甥の木村、加速します(時間が止まらない)
			gameoverFlag = false;			// gameoverFlag を false にする
		}
	}

	/*----------------------------------------------------------------------------------------------------*/
}
