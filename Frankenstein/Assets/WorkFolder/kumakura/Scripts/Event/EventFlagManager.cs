using UnityEngine;


public class EventFlag
{
    public EventFlagName flagName;   // �t���O�̖��O
    public bool isTrue;              // �t���O�̏��
    public EventFlag(EventFlagName _flagName, bool _isTrue = true)
    {
        this.flagName = _flagName;
        this.isTrue = _isTrue;
    }
}

public class EventFlagManager : MonoBehaviour
{
    private static string objectName = "EventFlagManager";
    private static EventFlagManager instance = null;
    public static EventFlagManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject mamagerObject = new GameObject(objectName);
                instance = mamagerObject.AddComponent<EventFlagManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // �C�x���g���s�����ǂ����𔻕ʂ���t���O
    public bool EventFlag { get; set; }

    private EventFlag[] flags = new EventFlag[0];

    /// <summary>
    /// �w�肵�����O�̃t���O��Ԃ��擾����
    /// </summary>
    /// <param name="flagName"></param>
    /// <returns></returns>
    public bool GetFlagState(EventFlagName flagName)
    {
        bool state = true;
        EventFlag targetFlag = FindFlag(flagName);

        if (targetFlag == null)
        {
            Debug.Log(flagName + "�Ƃ������O�̃t���O�͂Ȃ���I");
        }
        else
        {
            state = targetFlag.isTrue;
        }

        return state;
    }

    /// <summary>
    /// �w�肵�����O�̃t���O�̏�Ԃ�ݒ肷��
    /// </summary>
    /// <param name="flagName"></param>
    /// <param name="flag"></param>
    public void SetFlagState(EventFlagName flagName, bool flag)
    {
        EventFlag targetFlag = FindFlag(flagName);
        targetFlag = targetFlag ?? AddFlag(flagName);

        targetFlag.isTrue = flag;
    }

    /// <summary>
    /// �S�Ẵt���O�̏�Ԃ��R���\�[����ɕ\������
    /// </summary>
    public void DumpAllFlag()
    {
        Debug.Log("----- FlagDump Start -----");
        foreach (EventFlag flag in flags)
        {
            Debug.Log($"{flag.flagName} : {flag.isTrue}");
        }
        Debug.Log("----- FlagDump End -----");
    }

    /// <summary>
    /// �t���O��S�ď�������
    /// </summary>
    public void ClearAllFlag()
    {
        flags = new EventFlag[0];
    }

    /// <summary>
    /// �t���O��S��false�ɂ���
    /// </summary>
    public void ResetAllFlag()
    {
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i].isTrue = false;
        }
    }

    /// <summary>
    /// �w�肵�����O�̃t���O����������
    /// </summary>
    /// <param name="flagName"></param>
    /// <returns></returns>
    private EventFlag FindFlag(EventFlagName flagName)
    {
        EventFlag resultFlag = null;
        foreach (EventFlag flag in flags)
        {
            if (flag.flagName == flagName)
            {
                resultFlag = flag;
                break;
            }
        }

        return resultFlag;
    }

    /// <summary>
    /// �w�肵�����O�̃t���O��ǉ�����
    /// </summary>
    /// <param name="flagName"></param>
    /// <returns></returns>
    public EventFlag AddFlag(EventFlagName flagName)
    {
        System.Array.Resize(ref flags, flags.Length + 1);
        int newElement = flags.Length - 1;
        flags[newElement] = new EventFlag(flagName);

        return flags[newElement];
    }
}
