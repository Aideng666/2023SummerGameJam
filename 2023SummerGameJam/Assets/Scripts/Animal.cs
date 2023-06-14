using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.Move().magnitude > 0.1f)
        {
            Move();
        }
        else
        {
            body.velocity = Vector3.down * GameManager.Instance.worldGravity;
        }
    }

    void Move()
    {
        body.velocity = new Vector3(InputManager.Instance.Move().x, 0, InputManager.Instance.Move().y) * moveSpeed;

        body.velocity += Vector3.down * GameManager.Instance.worldGravity;
    }
}
