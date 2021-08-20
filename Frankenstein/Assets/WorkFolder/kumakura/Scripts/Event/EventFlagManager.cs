using UnityEngine;


public class EventFlag
{
    public EventFlagName flagName;   // フラグの名前
    public bool isTrue;              // フラグの状態
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

    // イベント実行中かどうかを判別するフラグ
    public bool EventFlag { get; set; }

    private EventFlag[] flags = new EventFlag[0];

    /// <summary>
    /// 指定した名前のフラグ状態を取得する
    /// </summary>
    /// <param name="flagName"></param>
    /// <returns></returns>
    public bool GetFlagState(EventFlagName flagName)
    {
        bool state = true;
        EventFlag targetFlag = FindFlag(flagName);

        if (targetFlag == null)
        {
            Debug.Log(flagName + "という名前のフラグはないよ！");
        }
        else
        {
            state = targetFlag.isTrue;
        }

        return state;
    }

    /// <summary>
    /// 指定した名前のフラグの状態を設定する
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
    /// 全てのフラグの状態をコンソール上に表示する
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
    /// フラグを全て消去する
    /// </summary>
    public void ClearAllFlag()
    {
        flags = new EventFlag[0];
    }

    /// <summary>
    /// フラグを全てfalseにする
    /// </summary>
    public void ResetAllFlag()
    {
        for (int i = 0; i < flags.Length; i++)
        {
            flags[i].isTrue = false;
        }
    }

    /// <summary>
    /// 指定した名前のフラグを検索する
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
    /// 指定した名前のフラグを追加する
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
