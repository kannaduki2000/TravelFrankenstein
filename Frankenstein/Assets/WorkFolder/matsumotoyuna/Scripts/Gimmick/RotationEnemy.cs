using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEnemy : MonoBehaviour
{
    private float speed = 0;
    private bool kasokuuuuuu = false;
    public bool kasokudekiru = false;
    public bool rothaguruma = false;

    void Update()
    {
        //�������ۂ�
        if (rothaguruma == false && kasokudekiru == false)
        {
            kasokuuuuuu = false;
        }
        
        //�኱���������
        if(rothaguruma == true && kasokudekiru == false)
        {
            this.speed = 0.001f;
            kasokuuuuuu = true;
            kasokudekiru = true;
        }

        //��]�X�s�[�h
        if(kasokuuuuuu == true)
        {
            transform.Rotate(0, 0, this.speed);
        }

        if (speed <= 0.1)
        {
            this.speed += 0.002f;
        }
    }
}