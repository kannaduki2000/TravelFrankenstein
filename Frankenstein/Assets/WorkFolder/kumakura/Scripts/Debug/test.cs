using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugLogUtility;

public class test : MonoBehaviour
{
    // テスト
    int[] sss = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

    // Start is called before the first frame update
    void Start()
    {
        int aaa = 1000;
        DLUtility.DebugLog("aaa" + aaa);
        DLUtility.DebugLogArray(sss);
    }
}
