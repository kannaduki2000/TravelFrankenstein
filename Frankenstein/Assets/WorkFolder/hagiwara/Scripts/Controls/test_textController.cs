using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_textController : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	float distance = 0.0f;		// ���������܂����Ⴈ���˂��Ă����ϐ�

	GameObject Player;		// �v���C���[�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject PC;              // PC�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g
	GameObject Textobj;		// �e�L�X�g�����܂����Ⴈ���˂��Ă����I�u�W�F�N�g

	Text Message;                   // �e�L�X�g�����܂����Ⴈ���˂��Ă����e�L�X�g

	/*----------------------------------------------------------------------------------------------------*/

	// Start is called before the first frame update
	void Start()
	{
		/*�������ǂ�����e�X�g�̃I�u�W�F�N�g�������Ă���̂ŁA�����̍ۂ͍����ւ������肢���܂�������*/
		Player = GameObject.Find("test_player");			// �v���C���[�̃I�u�W�F�N�g�����܂����Ⴈ����
		PC = GameObject.Find("test_PC");					// PC�̃I�u�W�F�N�g�����܂����Ⴄ������
		Textobj = GameObject.Find("test_text");			// �e�L�X�g�̃I�u�W�F�N�g�����܂����Ⴈ����

		Message = Textobj.GetComponent<Text>();		// Text�R���|�l�����܂����Ⴈ����
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

		distance = Vector3.Distance(PlayerPos, PCPos);       // �v���C���[��PC�̋����𑪂낤��
	}

	private void TextChange()		// �e�L�X�g�̓��e��ς���֐�
	{
		if (distance <= 400)        // �v���C���[��PC�̋�����400m�ȉ��ł����H
		{
			// YES
			Message.text = "Y�{�^��:�d��������";
		}
		else
		{
			// NO
			Message.text = "";
		}
	}

	/*----------------------------------------------------------------------------------------------------*/
}
