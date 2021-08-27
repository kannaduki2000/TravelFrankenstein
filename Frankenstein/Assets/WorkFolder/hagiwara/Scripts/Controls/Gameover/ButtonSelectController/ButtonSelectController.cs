using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectController : MonoBehaviour
{
	// インスペクター上で最初に選択するボタンをしまっておくためのもの
	[SerializeField]
	Button FirstSelectButton;

	// Start is called before the first frame update
	void Start()
	{
		// FirstSelectButtonのボタンを選択状態にする
		FirstSelectButton.Select();
	}
}
