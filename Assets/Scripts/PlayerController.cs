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
    public float keyboardRotationSensitivity = 5.0f;
    public uint isSeenBy = 0;

    public RogueLevelManager rlm;

    float clamp = 0.0f;

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotation, 0);

        float upDown = Input.GetAxis("Mouse Y") * mouseSensitivity * -1;

        //To rotate with Keyboards keys instead of mouse.

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(0, -1 * keyboardRotationSensitivity, 0);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(0, 1 * keyboardRotationSensitivity, 0);
        //}

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

        if(isSeenBy > 0)
        {
            if(rlm.goingRogue != true)
            {
                rlm.goingRogue = true;
                rlm.SeenBy = isSeenBy;
            }
        }

        else
        {
            isSeenBy = 0;
            rlm.goingRogue = false;
            rlm.SeenBy = isSeenBy;
        }
    }
}
