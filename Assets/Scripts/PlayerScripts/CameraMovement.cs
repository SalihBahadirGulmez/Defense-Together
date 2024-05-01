using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject mainCamera;
    public GameObject player;
    public Vector3 offset = new Vector3(0, 5.5f, -4);
    public float cameraMovementSpeed = 10;


    float delayBetweenPresses = 0.25f;
    bool pressedFirstTime = false;
    float lastPressedTime;



    float x;
    float y;

    private void Start()
    {
        mainCamera= GameObject.Find("Main Camera");
        mainCamera.transform.position = transform.position + offset;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (pressedFirstTime)
            {
                if (Time.time - lastPressedTime <= delayBetweenPresses)
                {
                    pressedFirstTime = false;
                    mainCamera.transform.position = transform.position + offset;
                }
                else
                {
                    lastPressedTime = Time.time;
                }
            }
            else
            {
                pressedFirstTime = true;
            }
            lastPressedTime = Time.time;
        }

        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
        if (x < 10)
        {
            mainCamera.transform.position += Vector3.left * Time.deltaTime * cameraMovementSpeed;
        }
        else if (x > Screen.width - 10)
        {
            mainCamera.transform.position += Vector3.right * Time.deltaTime * cameraMovementSpeed;
        }
        if (y < 10)
        {
            mainCamera.transform.position += Vector3.back * Time.deltaTime * cameraMovementSpeed;
        }
        else if (y > Screen.width - 10)
        {
            mainCamera.transform.position += Vector3.forward * Time.deltaTime * cameraMovementSpeed;
        }
    }
}
