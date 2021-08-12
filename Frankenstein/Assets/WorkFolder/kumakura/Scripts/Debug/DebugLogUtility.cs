using UnityEngine;

namespace DebugLogUtility
{
    // Debug���i��N���X
    public class DLUtility
    {
        public static void DebugLog(object _message)
        {
            Debug.Log(_message);
        }                                           // �ʏ��Debug.Log�Ɠ���
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
        }         // �z�������ƒ��g��S�ăR���\�[����ɕ\������(object)
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
        }            // �z�������ƒ��g��S�ăR���\�[����ɕ\������(int)
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
        }         // �z�������ƒ��g��S�ăR���\�[����ɕ\������(string)
        public static void DebugLogError(object _message)
        {
            Debug.LogError(_message);
        }                                      // �ʏ��Debug.LogError�Ɠ���
        public static void DebugLogWarninng(object _message)
        {
            Debug.LogError(_message);
        }                                   // �ʏ��Debug.LogWarninng�Ɠ���
        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            Debug.DrawLine(start, end, color, duration);
        }   // �ʏ��Debug.DrawLine�Ɠ���
        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            Debug.DrawLine(start, end, color);
        }                   // �ʏ��Debug.DrawLine�Ɠ���
        public static void DrawLine(Vector3 start, Vector3 end)
        {
            Debug.DrawLine(start, end);
        }                                // �ʏ��Debug.DrawLine�Ɠ���
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
        {
            Debug.DrawRay(start, dir, color, duration);
        }    // �ʏ��Debug.DrawRay�Ɠ���
        public static void DrawRay(Vector3 start, Vector3 dir, Color color)
        {
            Debug.DrawRay(start, dir, color);
        }                    // �ʏ��Debug.DrawRay�Ɠ���
        public static void DrawRay(Vector3 start, Vector3 dir)
        {
            Debug.DrawRay(start, dir);
        }                                 // �ʏ��Debug.DrawRay�Ɠ���
        public static void Break()
        {
            Debug.Break();
        }                                                             // �ʏ��Debug.Break�Ɠ���
    }
}
