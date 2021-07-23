using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AnnounceName
{
    // Tutorial
    T_Put_Electric_Enemy,           // �G�l�~�[�̎��̂ɓd�C������
    T_Throw_Having,                 // ������A����
    T_Come_Out_Home,                // �Ƃ���o��
    T_Release_Object,               // �������ŏ�ɓ�����A����
    T_SquareButton,                 // �l�p�{�^��
    T_CircleButton_StartUp,         // �ۃ{�^���F�N������
    T_Leftstick_Move,               // ���X�e�B�b�N�F�ړ�
    T_SquareButton_Absorption,      // �l�p�{�^���F�d�C���z������
    T_SquareButton_Input,           // �l�p�{�^���F�d�C������

    // Stage1
    S1_TriangleButton_Gear,         // �O�p�{�^���F���ԂɂȂ�
    S1_CircleButton_EnemyCall,      // �ۃ{�^���F�G�l�~�[���Ă�
    S1_SquareButton_ElectricCable,  // �l�p�{�^���F�d����`��
    S1_LButton_EnemyChange,         // L�{�^���F����؂�ւ�
    S1_RButton_Push,                // R�{�^���F����
    S1_SquareButton,                // �l�p�{�^��
    S1_SquareButton_Input,          // �l�p�{�^���F�d�C������
    S1_SquareButton_Absorption,     // �l�p�{�^���F�d�C���z������

}

[System.Serializable]
public enum ReportName
{
    eveReport,          // �C�����|�[�g
    gearHertsReport,    // �M�A�n�[�c���|�[�g

}

public class ImageData : MonoBehaviour
{
    public Sprite[] AnnounceImageArray;
    public Sprite[] ReportImageArray;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �w���\���̉摜���Z�b�g
    /// </summary>
    /// <param name="annouceSprite">�Z�b�g����Object</param>
    /// <param name="announceName">�Z�b�g�������摜�̖��O</param>
    public Sprite GetAnnounceImage(AnnounceName announceName)
    {
        return AnnounceImageArray[(int)announceName];
    }

    public Sprite GetReportImage(ReportName reportName)
    {
        return ReportImageArray[(int)reportName];
    }
}
