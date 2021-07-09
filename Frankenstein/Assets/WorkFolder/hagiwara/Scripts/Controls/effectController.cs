using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	RectTransform effect_size;

	float width = 100f;
	float height = 100f;
	float timer = 0.0f;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		effect_size = GameObject.Find("effect").GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		width += 10;
		height += 10;

		effect_size.sizeDelta = new Vector2(width, height);
	}
	/*----------------------------------------------------------------------------------------------------*/

}
