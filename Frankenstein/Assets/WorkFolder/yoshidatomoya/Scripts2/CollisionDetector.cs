using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 使わないけど記念に残しておく
/*
[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    [SerializeField] private Enemy enemy;

    /// <summary>
    /// Is TriggerがONで他のColliderと重なっているときに呼ばれ続ける
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        
    }
*/


/*
    // UnityEventを継承したクラスに[Serializable]属性を付与することで、Inspectorウインドウ上に表示できるようになる。
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
}
*/