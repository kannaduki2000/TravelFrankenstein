using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_PCController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	float distance = 0.0f;		// ���������܂����Ⴈ���˂��Ă����ϐ�

	bool Y_ButtonPushFlag = false;		// Y�{�^�����������Ⴈ���˂��Ă��������̃t���O

	int player_hp = 0;			// �ʂ̃X�N���v�g�̕ϐ������܂����Ⴈ���˂��Ă����ϐ�
	int power_cn = 30;			// PC��_�����Ƃ��̏���d��

	GameObject Player;		// �v���C���[�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject PC;				// PC�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g

	test_playerController PlayerScript;		// �v���C���[�̃X�N���v�g�����܂����Ⴈ���˂��Ă������

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*�������ǂ�����e�X�g�̃I�u�W�F�N�g�������Ă���̂ŁA�����̍ۂ͍����ւ������肢���܂�������*/
		Player = GameObject.Find("test_player");			// �v���C���[�̃I�u�W�F�N�g�����܂����Ⴈ����
		PC = GameObject.Find("test_PC");					// PC�̃I�u�W�F�N�g�����܂����Ⴄ������

		PlayerScript = Player.GetComponent<test_playerController>();		// �v���C���[�̒��ɂ���X�N���v�g�����܂����Ⴈ����
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		Y_ButtonFlagChange();

		Y_ButtonPush();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		 // ���W�̎擾�Ƌ����̑��肷��֐�
	{
		// �v���C���[��PC�̍��W���擾�����Ⴈ����
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);       // �v���C���[��PC�̋����𑪂낤��
	}

	private void Y_ButtonFlagChange()			// Y_ButtonPushFlag ��ς���֐�
	{
		if (distance <= 400)        // �v���C���[��PC�̋�����400m�ȉ��ł����H
		{
			// YES
			Y_ButtonPushFlag = true;        // Y�{�^�����������Ƃ�������
		}
		else
		{
			// NO
		}
	}

	private void Y_ButtonPush()						// Y_Button ���������Ƃ��̏����̊֐� 
	{
		/*�������R���g���[���[��Y�{�^���ł͂Ȃ��A�e�X�g�ŃG���^�[�L�[�Ƃ��Ă���̂ŁA�����̍ۂ͍����ւ������肢���܂�������*/
		if (Y_ButtonPushFlag == true && Input.GetKeyDown(KeyCode.Return))       // Y_ButtonPushFlag �� True ���AY�{�^����������܂������H
		{
			// YES
			PlayerHPProc();
		}
		else
		{
			// NO
		}

	}

	private void PlayerHPProc()				// �v���C���[��HP�Ɋւ���֐�
	{
		player_hp = PlayerScript.HP;            // �v���C���[��HP�����܂����Ⴈ����
		player_hp -= power_cn;                  // �v���C���[��HP������d�͕��A���炷
		PlayerScript.HP = player_hp;            // �������v���C���[��HP��value�ɓ����
	}

	/*----------------------------------------------------------------------------------------------------*/
}
