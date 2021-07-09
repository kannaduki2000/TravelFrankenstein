using UnityEngine;
using System.Collections;
public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private CameraScript cameraScript = null;

    private GameObject Player;
    private Vector3 PlayerPosition;
    private Vector3 newPosition;

    void Start()
    {
        if (this.cameraScript == null)
        {
            this.cameraScript = this.gameObject.GetComponent<CameraScript>();
        }

        Player = this.gameObject;

        PlayerPosition = Player.transform.position;
        newPosition = transform.position;
    }

    // Update is called once per frame
    public void Update()
    {
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cameraScript.GoTo(newPosition);
        }
    }
}