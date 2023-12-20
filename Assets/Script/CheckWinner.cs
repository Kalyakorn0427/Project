using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera defaultCamera;
    public Camera winnerCamera;
    public bool isWinner = false;

    public Transform target;
    public float smoothSpeed = 1.0f;

    public Transform playerRotation;
    
    public static CheckWinner instance;

    public void Awake()
    {
        instance = this;
    }


    void Start()
    {
        defaultCamera.enabled = true;
        winnerCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isWinner) {
            defaultCamera.enabled = false;
            winnerCamera.enabled=true;
        }
    }

    private void LateUpdate()
    {
        if (target != null && isWinner)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, target.position.z - 3.0f);

            Vector3 smoothPosition = Vector3.Lerp(winnerCamera.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            winnerCamera.transform.position = smoothPosition;

            playerRotation.LookAt(new Vector3(playerRotation.position.x, playerRotation.position.y, winnerCamera.transform.position.z));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && PlayerController.instance.groundedPlayer) 
        {
            isWinner = true;
        }
    }
}
