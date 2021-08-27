using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	// �v���n�u�����܂��Ă������
	[SerializeField]
	GameObject GameoverUIPrefab;

	// �C���X�^���X�����܂��Ă������
	GameObject GameoverUIInstance;
	// �v���C���[�����܂��Ă������
	GameObject PlayerObj;
	// �X�N���v�g�����܂��Ă������
	TestPlayerController_2 PlayerScr;

	// TestPlayerControlloer_2 �� PlayerHp �����܂��Ă������
	int hp = 0;

	// Gameover���ۂ��̃t���O
	bool gameoverFlag = false;

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		PlayerObj = GameObject.Find("TestPlayer");											// �I�u�W�F�N�g�������Ă��܂����Ⴄ
		PlayerScr = PlayerObj.GetComponent<TestPlayerController_2>();		// �X�N���v�g�������Ă��܂����Ⴄ
	}

	// Update is called once per frame
	void Update()
	{
		hp = PlayerScr.PlayerHp;			// hp �� PlayerHp �����܂����Ⴄ

		if(hp == 0 && gameoverFlag == false)			// hp ��0�Ŋ��AgameoverFlag �� False �ł����H
		{
			// YES
			GameoverUIInstance = GameObject.Instantiate(GameoverUIPrefab);
			Time.timeScale = 0f;			// �ޥܰ��ޯ�I�h���E�h�̎����~�߂鯁I�I
			gameoverFlag = true;			// gameoverFlag �� true �ɂ���
		}
		else if(hp > 0)			// hp �� 0 ����ł����H
		{
			// YES
			Time.timeScale = 1f;			// ���̖ؑ��A�������܂�(���Ԃ��~�܂�Ȃ�)
			gameoverFlag = false;			// gameoverFlag �� false �ɂ���
		}
	}

	/*----------------------------------------------------------------------------------------------------*/
}
