using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	[SerializeField]			// エフェクトに使用する画像をしまっておく配列
	Sprite[] effect_sprites;
	[SerializeField]			// 画像がアップデートされる時間を指定する
	float speed;

	Image effect_image;		// イメージをしまっちゃおうねしておくやつ

	float current = 0.0f;		// 現在のタイム

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		effect_image = GetComponent<Image>();		// effect_imageにイメージをしまっちゃおう
		effect_image.sprite = effect_sprites[0];				// effect_sprites の0番目のものを、effect_image に入っているイメージのスプライトにしまっちゃおう



	}

	// Update is called once per frame
	void Update()
	{
		
	}
	/*----------------------------------------------------------------------------------------------------*/


}
