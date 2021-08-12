using UnityEngine;

namespace DebugLogUtility
{
    // Debugを司るクラス
    public class DLUtility
    {
        public static void DebugLog(object _message)
        {
            Debug.Log(_message);
        }                                           // 通常のDebug.Logと同じ
        public static void DebugLogArray(object[] _message, string _context = "Array")
        {
            int arrayNum = 0;
            Debug.Log("--- Start ---");
            foreach (var data in _message)
            {
                Debug.Log($"{ _context + "[" + arrayNum + "]" } : { data }");
                arrayNum++;
            }
            Debug.Log("--- End ---");
        }         // 配列を入れると中身を全てコンソール上に表示する(object)
        public static void DebugLogArray(int[] _message, string _context = "Array")
        {
            int arrayNum = 0;
            Debug.Log("--- Start ---");
            foreach (var data in _message)
            {
                Debug.Log($"{ _context + "[" + arrayNum + "]" } : { data }");
                arrayNum++;
            }
            Debug.Log("--- End ---");
        }            // 配列を入れると中身を全てコンソール上に表示する(int)
        public static void DebugLogArray(string[] _message, string _context = "Array")
        {
            int arrayNum = 0;
            Debug.Log("--- Start ---");
            foreach (var data in _message)
            {
                Debug.Log($"{ _context + "[" + arrayNum + "]" } : { data }");
                arrayNum++;
            }
            Debug.Log("--- End ---");
        }         // 配列を入れると中身を全てコンソール上に表示する(string)
        public static void DebugLogError(object _message)
        {
            Debug.LogError(_message);
        }                                      // 通常のDebug.LogErrorと同じ
        public static void DebugLogWarninng(object _message)
        {
            Debug.LogError(_message);
        }                                   // 通常のDebug.LogWarninngと同じ
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            Debug.DrawLine(start, end, color, duration);
        }   // 通常のDebug.DrawLineと同じ
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Debug.DrawLine(start, end, color);
        }                   // 通常のDebug.DrawLineと同じ
        public static void DrawLine(Vector3 start, Vector3 end)
        {
            Debug.DrawLine(start, end);
        }                                // 通常のDebug.DrawLineと同じ
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
        {
            Debug.DrawRay(start, dir, color, duration);
        }    // 通常のDebug.DrawRayと同じ
        public static void DrawRay(Vector3 start, Vector3 dir, Color color)
        {
            Debug.DrawRay(start, dir, color);
        }                    // 通常のDebug.DrawRayと同じ
        public static void DrawRay(Vector3 start, Vector3 dir)
        {
            Debug.DrawRay(start, dir);
        }                                 // 通常のDebug.DrawRayと同じ
        public static void Break()
        {
            Debug.Break();
        }                                                             // 通常のDebug.Breakと同じ
    }
}
