using UnityEngine;

public class DebugPoint : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField, Range(0, 1)] float r = 1;
    [SerializeField, Range(0, 1)] float g = 1;
    [SerializeField, Range(0, 1)] float b = 1;
    [SerializeField, Range(0, 1)] float a = 0.6f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(r, g, b, a);
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }
#endif
}
