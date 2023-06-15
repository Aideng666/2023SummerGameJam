using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float moveSpeed = 5;

    CharacterController characterController;
    Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = Vector3.zero;

        if (InputManager.Instance.Move().magnitude > 0.1f)
        {
            Move();
        }

        characterController.SimpleMove(moveDir * moveSpeed);

        if (moveDir.magnitude > 0.1f)
        {
            transform.forward = moveDir;
        }
    }

    void Move()
    {
        //Gets movement direction based on input and camera rotation
        moveDir = new Vector3(InputManager.Instance.Move().x, 0, InputManager.Instance.Move().y).normalized;
        moveDir = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0) * moveDir;
    }
}
