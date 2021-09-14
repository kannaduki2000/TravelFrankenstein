﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController_2 : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// フランケンのHP
	[SerializeField]
	int playerHp = 100;
	// フランケンのHPのプロパティ
	public int PlayerHp
	{
		get { return playerHp; }
		set { playerHp = value; }
	}

	/*----------------------------------------------------------------------------------------------------*/

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(-7.5f, 0.0f, 0.0f);
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(7.5f, 0.0f, 0.0f);
		}
	}

	/*----------------------------------------------------------------------------------------------------*/

}
