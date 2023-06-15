using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] protected float moveSpeed = 5;

    protected CharacterController characterController;
    protected Vector3 moveDir = Vector3.zero;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        moveDir = Vector3.zero;

        if (InputManager.Instance.Move().magnitude > 0.1f)
        {
            Move();
        }

        //apply the move in the derived animal classes
        //characterController.SimpleMove(moveDir * moveSpeed);

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
