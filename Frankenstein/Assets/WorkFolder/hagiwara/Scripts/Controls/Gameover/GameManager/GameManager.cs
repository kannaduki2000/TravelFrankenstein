using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	/*----------------------------------------------------------------------------------------------------*/

	/*----------------------------------------------------------------------------------------------------*/

	/*----------------------------------------------------------------------------------------------------*/

	/// <summary>
	/// �R���e�j���[���������Ƃ��ɁA�V�[�����ēǂݍ��݂���֐�
	/// </summary>
	public void Continue()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// �^�C�g�����������Ƃ��ɁA�^�C�g���V�[���ւƖ߂�֐�
	/// </summary>
	public void Title()
	{
		SceneManager.LoadScene("Test_TitleScene");
	}

	/*----------------------------------------------------------------------------------------------------*/

}
