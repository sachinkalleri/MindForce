using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    public float moveMultiplier = 5.0f;
    public float mouseSensitivity = 5.0f;
    float clamp = 0.0f;

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotation, 0);

        float upDown = Input.GetAxis("Mouse Y") * mouseSensitivity * -1;

        if (clamp + upDown > 80 || clamp + upDown < -80)
        {
            upDown = 0;
        }
        Camera.main.transform.Rotate(upDown, 0, 0);
        clamp += upDown;

        float forwardSpeed = Input.GetAxis("Vertical") * moveMultiplier;

        float lateralSpeed = Input.GetAxis("Horizontal") * moveMultiplier;

        CharacterController characterController = GetComponent<CharacterController>();

        Vector3 speed = new Vector3(lateralSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        characterController.Move(speed * Time.deltaTime);
    }
}
