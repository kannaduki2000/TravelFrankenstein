using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	float distance = 0.0f;		// ���������܂����Ⴈ���˂��Ă����ϐ�

	GameObject Player;		// �v���C���[�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject PC;				// PC�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject Textobj;		// �e�L�X�g�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g

	Text Message;			// �e�L�X�g�����܂����Ⴈ���˂��Ă����e�L�X�g

	PC_Controller PC_Script;			// PC��Script�����܂����Ⴈ���˂��Ă������

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*�������ǂ�����e�X�g�̃I�u�W�F�N�g�������Ă���̂ŁA�����̍ۂ͍����ւ������肢���܂�������*/
		Player = GameObject.Find("test_player");			// �v���C���[�̃I�u�W�F�N�g�����܂����Ⴈ����
		PC = GameObject.Find("test_PC");					// PC�̃I�u�W�F�N�g�����܂����Ⴄ������
		Textobj = GameObject.Find("test_text");			// �e�L�X�g�̃I�u�W�F�N�g�����܂����Ⴈ����

		Message = Textobj.GetComponent<Text>();			// Text�R���|�l�����܂����Ⴈ����

		PC_Script = PC.GetComponent<PC_Controller>();			// �X�N���v�g�����܂����Ⴄ��������
	}

	// Update is called once per frame
	void Update()
	{
		GetCoord_And_DistMeas();

		TextChange();
	}

	/*----------------------------------------------------------------------------------------------------*/

	private void GetCoord_And_DistMeas()		// ���W�̎擾�Ƌ����̑��肷��֐�
	{       
		// �v���C���[��PC�̍��W���擾�����Ⴈ����
		Vector3 PlayerPos = Player.transform.position;
		Vector3 PCPos = PC.transform.position;

		distance = Vector3.Distance(PlayerPos, PCPos);			// �v���C���[��PC�̋����𑪂낤��
	}

	private void TextChange()		// �e�L�X�g�̓��e��ς���֐�
	{
		if (distance <= PC_Script.Dist_Setting && PC_Script.pc_switch_flag == false)			// �v���C���[��PC�̋�����dist_setting�ȉ��ŁA���APCSwitchFlag �� False �ł����H
		{
			// YES
			Message.text = "���{�^��:�d��������";
		}
		else if (distance <= PC_Script.Dist_Setting && PC_Script.pc_switch_flag == true)			// �v���C���[��PC�̋�����dist_setting�ȉ��ŁA���APCSwitchFlag �� True �ł����H
		{
			// YES
			Message.text = "���{�^��:�d����؂�";
		}
		else
		{
			// NO
			Message.text = "";
		}
	}

	/*----------------------------------------------------------------------------------------------------*/
}
