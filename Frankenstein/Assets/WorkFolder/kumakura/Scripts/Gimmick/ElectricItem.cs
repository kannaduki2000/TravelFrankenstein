using UnityEngine;
using UnityEngine.UI;

// �d�C������I�u�W�F�N�g�̐e�N���X
public class ElectricItem : MonoBehaviour
{
    public int Power { get; set; } = 0;                 // ����d��
    public bool ChargeFlag { get; set; } = false;       // �d�C�����Ă��邩�ǂ����@OnOff�\
    public bool IsCharge { get; set; } = false;         // Player���[�d�ł��邩
    public bool IsChargeEvent { get; set; } = false;    // Event���s�ς��ǂ����@On�̂�
    public bool IsThrow { get; set; } = false;          // �������Object���ǂ���

    public virtual void Event() { }
    public virtual void ChargeEvent() { }

}