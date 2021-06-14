using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneDebug : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;


    void Update()
    {
        if (!debugMode) return;
        #if UNITY_EDITOR
            // ���R���g���[���L�[
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                // �V�[���̍ēǂݍ���
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // ���̃V�t�g�L�[
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                // �Q�[��������3�{�ɕύX
                Time.timeScale = 3;
            }

            // �E�̃V�t�g�L�[
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                // �Q�[��������0.5�{�ɕύX
                Time.timeScale = 0.5f;
            }

            // �V�t�g�L�[�𗣂�
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                // �Q�[�������ԓ����ɖ߂�
                Time.timeScale = 1;
            }
        #endif
    }
}
