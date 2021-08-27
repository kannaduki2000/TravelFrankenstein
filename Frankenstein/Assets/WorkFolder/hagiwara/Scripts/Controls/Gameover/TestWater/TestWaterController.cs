using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaterController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// �R���[�`���łǂ̂��炢�������~�߂邩(default:0.5)
	[SerializeField]
	float stopTime;

	// �v���C���[�����܂��Ă������
	GameObject PlayerObj;
	// �v���C���[�̃X�N���v�g�����܂��Ă������
	TestPlayerController_2 PlayerScr;

	// test_player_controlloer �� PlayerHp �����܂��Ă������
	int hp = 0;

	/*----------------------------------------------------------------------------------------------------*/
	// Start is called before the first frame update
	void Start()
	{
		PlayerObj = GameObject.Find("TestPlayer");									// �I�u�W�F�N�g�������Ă��܂����Ⴄ
		PlayerScr = PlayerObj.GetComponent<TestPlayerController_2>();		// �X�N���v�g�������Ă��܂����Ⴄ
	}

	/*----------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// ���̕����ɐN�������Ƃ��Ɏ��s�����Ⴄ��`��֐�
	/// </summary>
	/// <param name="other">�g��Ȃ��ɂ�E�B�E</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		StartCoroutine("Drowned");
	}

	/// <summary>
	/// ���ւ��������ɑS�g���Z��悤��������R���[�`��
	/// </summary>
	/// <returns></returns>
	IEnumerator Drowned()
	{
		hp = PlayerScr.PlayerHp;			// hp �� PlayerHp �����܂����Ⴄ

		yield return new WaitForSeconds(stopTime);			// stopTime ���A�������~�߂�

		hp -= PlayerScr.PlayerHp;			// hp �� PlayerHp ���A���炵���Ⴄ
		PlayerScr.PlayerHp = hp;			// PlayerHp �� hp �����܂����Ⴄ
	}
}
