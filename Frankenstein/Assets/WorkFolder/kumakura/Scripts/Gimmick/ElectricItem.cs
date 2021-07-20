using UnityEngine;
using UnityEngine.UI;

public class ElectricItem : MonoBehaviour
{
    public int Power { get; set; } = 0;                 // ����d��
    public bool ChargeFlag { get; set; } = false;       // �d�C������/�[�d�@OnOff�\
    public bool IsElectricEvent { get; set; } = false;  // Event�����ǂ���
    public bool IsChargeEvent { get; set; } = false;    // Event���s�ς��ǂ����@On�̂�
    public Image AnnounceImage { get; set; } = null;    // �w���\���摜

    public virtual void Event() { }

}
