using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectController : MonoBehaviour
{
	// �C���X�y�N�^�[��ōŏ��ɑI������{�^�������܂��Ă������߂̂���
	[SerializeField]
	Button FirstSelectButton;

	// Start is called before the first frame update
	void Start()
	{
		// FirstSelectButton�̃{�^����I����Ԃɂ���
		FirstSelectButton.Select();
	}
}
