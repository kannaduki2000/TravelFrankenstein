using UnityEngine;

public class FlagGenerator : MonoBehaviour
{
    void Start()
    {
        for (int flag = 0; flag < (int)EventFlagName.FlagEnd; flag++)
        {
            EventFlagManager.Instance.SetFlagState((EventFlagName)flag, false);
        }
    }
}
