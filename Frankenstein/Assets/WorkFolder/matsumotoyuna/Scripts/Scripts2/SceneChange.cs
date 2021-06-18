using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //����
    public FadeManager fm;

    //�F�X
    public bool Title = false;
    public bool Tutorial = false;
    public bool Map1 = false;
    public bool Logo = false;
    public bool Map2 = false;
    Rigidbody2D rigid2D;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //����̏ꏊ��ʉ߂����甭��
        if(collision.gameObject.tag == "GoTitleLogo")
        {
            Logo = true;
        }

        if(collision.gameObject.tag == "GoMap2")
        {
            Map2 = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(����ƃQ�[���I�[�o�[��)
        {
            �^�C�g����ʕ\��
            ��(�Q�[���I�[�o�[���肪������V�[���`�F���W�^�C�g��)
        }
        */

        //�^�C�g����ʌ��菈��
        if(SceneManager.GetActiveScene().name == "TentativeTitle")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                fm.isFadeout = true;
                Tutorial = true;
            }
        }

        if (Map1 == true && SceneManager.GetActiveScene().name == "TentativeMap1")
        {
            fm.isFadein = true;
        }

        if (Logo == true)
        {
            fm.isWhiteout = true;
        }

        if (Map1 == true)
        {
            fm.isWhiteout = true;
        }

        if(Map2 == true)
        {
            fm.isFadeout = true;
        }

        /*
        if(�^�C�g���̌�)
        {
            �^�C�g���̓t�F�[�h�A�E�g���˂��c�H
            fm.isFadeout = !fm.isFadeout;
            �^�C�g���̌�Ƀt�F�[�h�C���Ń`���[�g���A����ʕ\��
            SceneManager.LoadScene("�`���[�g���A��");
            fm.isFadein = !fm.isFadein;
        }
        */

        /*
        if(�E�[�܂ōs����)
        {
            ���Ƀt�F�[�h�A�E�g
            fm.isWhiteout = !fm.isWhiteout;
            
            �t�F�[�h�C���Ń��S
            fm.isFadein = !fm.isFadein;�̃��S

            (���̌��)
            �t�F�[�h�C���ŋ�
            SceneManager.LoadScene("�}�b�v1");
            fm.isFadein = !fm.isFadein;�̋�
        }
        */

        /*
        if(�V���b�s���O���[�������ɐN��)
        {
            �����t�F�[�h�A�E�g
            fm.isFadeout = !fm.isFadeout;
            SceneManager.LoadScene("�}�b�v2?");
        }
        */
    }
}
