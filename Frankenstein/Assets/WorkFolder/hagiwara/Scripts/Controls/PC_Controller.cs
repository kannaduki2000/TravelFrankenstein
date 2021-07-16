using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Controller : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	[SerializeField]
	float dist_setting;			// �ǂ̂��炢�̋����Ńe�L�X�g���o����A�摜�̐؂�ւ����ł���悤�ɂȂ邩���߂����
	public float Dist_Setting			// dis_setting �̃v���p�e�B
	{
		get { return dist_setting; }
	}
	[SerializeField]
	Sprite PC_ON_Sprite;			// PC��ON�ɂȂ����X�v���C�g�����܂����Ⴈ���˂��Ă������
	[SerializeField]
	Sprite PC_OFF_Sprite;			// PC��OFF�ɂȂ����X�v���C�g�����܂����Ⴈ���˂��Ă������

	float distance = 0.0f;		// ���������܂����Ⴈ���˂��Ă����ϐ�

	bool Shikaku_ButtonPushFlag = false;		// ���{�^�����������Ⴈ���˂��Ă��������̃t���O
	bool PCSwitchFlag = false;							// PC���I�����I�t���̃t���O
	public bool pc_switch_flag			// PCSwitchFlag �̃v���p�e�B
	{
		get { return PCSwitchFlag; }
	}

	int player_hp = 0;			// �ʂ̃X�N���v�g�̕ϐ������܂����Ⴈ���˂��Ă����ϐ�
	int power_cn = 30;			// PC��_�����Ƃ��̏���d��

	GameObject Player;		// �v���C���[�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject PC;              // PC�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g

	SpriteRenderer PC_SpriteRenderer;			// �X�v���C�g�����_���[��܂����Ⴈ���˂��

	test_playerController PlayerScript;		// �v���C���[�̃X�N���v�g�����܂����Ⴈ���˂��Ă������

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*�������ǂ�����e�X�g�̃I�u�W�F�N�g�������Ă���̂ŁA�����̍ۂ͍����ւ������肢���܂�������*/
		Player = GameObject.Find("test_player");			// �v���C���[�̃I�u�W�F�N�g�����܂����Ⴈ����
		PC = GameObject.Find("test_PC");					// PC�̃I�u�W�F�N�g�����܂����Ⴄ������

		PlayerScript = Player.GetComponent<test_playerController>();			// �v���C���[�̒��ɂ���X�N���v�g�����܂����Ⴈ����

		PC_SpriteRenderer = PC.GetComponent<SpriteRenderer>();			// PC �̃X�v���C�g�����_���[�����܂����Ⴈ����
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		Shikaku_ButtonFlagChange();

		Shikaku_ButtonPush();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		 // ���W�̎擾�Ƌ����̑��肷��֐�
	{
		// �v���C���[��PC�̍��W���擾�����Ⴈ����
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);       // �v���C���[��PC�̋����𑪂낤��
	}

	private void Shikaku_ButtonFlagChange()			// Shikaku_ButtonPushFlag ��ς���֐�
	{
		if (distance <= dist_setting)		// �v���C���[��PC�̋�����dist_setting�ȉ��ł����H
		{
			// YES
			Shikaku_ButtonPushFlag = true;		// ���{�^�����������Ƃ�������
		}
		else
		{
			// NO
			Shikaku_ButtonPushFlag = false;		// ���{�^�����������Ƃ������Ȃ�
		}
	}

	private void Shikaku_ButtonPush()						// ���{�^�����������Ƃ��̏����̊֐� 
	{
		/*�������R���g���[���[�́��{�^���ƃL�[�{�[�h�̃G���^�[�L�[�Ŕ�������悤�ɂȂ��Ă܂��B�����̂Ƃ��́A�Ȃ񂩂��������ɂ��Ă�������������*/
		if (Shikaku_ButtonPushFlag == true && Input.GetKeyDown(KeyCode.Return))			// Shikaku_ButtonPushFlag �� True ���A�G���^�[�L�[��������܂������H
		{
			// YES
			PlayerHPProc();

			/*�Ȃ񂩂�����ӂɂ��������ɃG�t�F�N�g�����Ă�������*/

			PCSwitch();
		}
		else if (Shikaku_ButtonPushFlag == true && Input.GetButtonDown("Fire1"))            // Shikaku_ButtonPushFlag �� True ���A����������܂������H
		{
			// YES
			PlayerHPProc();

			/*�Ȃ񂩂�����ӂɂ��������ɃG�t�F�N�g�����Ă�������*/

			PCSwitch();
		}
		else
		{
			// NO
		}

	}

		private void PlayerHPProc()			// �v���C���[��HP�Ɋւ���֐�
		{
			if (PCSwitchFlag == false)          // PCSwitchFlag �� false �ł����H
			{
				// YES
				PlayerHPMinus();
			}
			else if (PCSwitchFlag == true)              // PCSwitchFlag �� true �ł����H
			{
				// NO
				PlayerHPPlus();
			}
		}

			private void PlayerHPMinus()			// �v���C���[��HP�����炷�֐�
		{
			player_hp = PlayerScript.HP;			// �v���C���[��HP�����܂����Ⴈ����
			player_hp -= power_cn;					// �v���C���[��HP������d�͕��A���炷
			PlayerScript.HP = player_hp;			// �������v���C���[��HP��value�ɓ����
		}

			private void PlayerHPPlus()			// �v���C���[��HP�𑝂₷�֐�
		{
			player_hp = PlayerScript.HP;			// �v���C���[��HP�����܂����Ⴈ����
			player_hp += power_cn;				// �v���C���[��HP������d�͕��A���₷(���ɖ߂�)
			PlayerScript.HP = player_hp;			// �������v���C���[��HP��value�ɓ����
		}

		private void PCSwitch()			// PC�̓d���Ɋւ���֐�
		{
			if (PCSwitchFlag == false)          // PCSwitchFlag �� false �ł����H
			{
				// YES
				PCSwitch_ON();
			}
			else if (PCSwitchFlag == true)          // PCSwitchFlag �� true �ł����H
			{
				// YES
				PCSwitch_OFF();
			}
		}

			private void PCSwitch_ON()			// PC�ɓd��������֐�
		{
				PC_SpriteRenderer.sprite = PC_ON_Sprite;            // PC_ON_Sprite �ɂ��܂����Ⴈ���˂��Ă������X�v���C�g�� PC_SpriteRenderer �̃X�v���C�g�ɓ����
				PCSwitchFlag = true;
		}

			private void PCSwitch_OFF()			// PC�̓d�����؂��֐�
		{
				PC_SpriteRenderer.sprite = PC_OFF_Sprite;            // PC_ON_Sprite �ɂ��܂����Ⴈ���˂��Ă������X�v���C�g�� PC_SpriteRenderer �̃X�v���C�g�ɓ����
				PCSwitchFlag = false;
		}

	/*----------------------------------------------------------------------------------------------------*/
}
