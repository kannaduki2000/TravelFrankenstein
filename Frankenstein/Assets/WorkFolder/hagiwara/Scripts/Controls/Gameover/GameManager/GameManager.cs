using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	/*----------------------------------------------------------------------------------------------------*/

	/*----------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// コンテニューを押したときに、シーンを再読み込みする関数
	/// </summary>
	public void Continue()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// タイトルを押したときに、タイトルシーンへと戻る関数
	/// </summary>
	public void Title()
	{
		SceneManager.LoadScene("Test_TitleScene");
	}

	/*----------------------------------------------------------------------------------------------------*/

}
