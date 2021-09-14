using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectController : MonoBehaviour
{
	// 最初に選択しておくボタンをしまっちゃおうねしておくやつ
	[SerializeField]
	Button FirstSelectButton;

	// Start is called before the first frame update
	void Start()
	{
		// FirstSelectButtonに入れたボタンを選択状態にする
		FirstSelectButton.Select();
	}
}
