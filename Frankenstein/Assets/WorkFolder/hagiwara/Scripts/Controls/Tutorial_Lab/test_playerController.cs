using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_playerController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// フランケンのHP
	int hp = 100;
	// フランケンのHPのプロパティ
	public int HP
	{
		get { return hp; }
		set { hp = value; }
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
