using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class effectController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	[SerializeField]			// �G�t�F�N�g�Ɏg�p����摜�����܂��Ă����z��
	Sprite[] effect_sprites;
	[SerializeField]			// �摜���A�b�v�f�[�g����鎞�Ԃ��w�肷��
	float speed;

	Image effect_image;		// �C���[�W�����܂����Ⴈ���˂��Ă������

	float current = 0.0f;		// ���݂̃^�C��

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		effect_image = GetComponent<Image>();		// effect_image�ɃC���[�W�����܂����Ⴈ��
		effect_image.sprite = effect_sprites[0];				// effect_sprites ��0�Ԗڂ̂��̂��Aeffect_image �ɓ����Ă���C���[�W�̃X�v���C�g�ɂ��܂����Ⴈ��



	}

	// Update is called once per frame
	void Update()
	{
		
	}
	/*----------------------------------------------------------------------------------------------------*/


}
