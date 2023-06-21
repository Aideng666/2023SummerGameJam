using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coyote : MonoBehaviour
{
    [SerializeField] float animalSightRadius = 8f;
    [SerializeField] float attackRange = 2;
    [SerializeField] float moveSpeed = 5;

    CharacterController characterController;
    Animator animator;
    CoyoteStates currentState = CoyoteStates.Wander;

    //For Wandering
    Vector3 chosenDirection = Vector3.zero;
    float moveTime = 1f;
    float elaspedMoveTime = 0;
    bool isMoving;

    //For Chasing
    Animal animalToChase;
    float chaseTimer = 5f;
    float elaspedchaseTime = 0;

    //For Attacking
    float attackDuration = 1;
    float elaspedAttackTime = 0;
    bool isAttacking;

    //For Idling
    float idleTime = 3;
    float elaspedIdleTime;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Choose current state
        Collider[] collidersHit; 
        List<Animal> animalsInView = new List<Animal>();

        //Apply current state
        switch (currentState)
        {
            case CoyoteStates.Wander:

                //Check for chase state
                collidersHit = Physics.OverlapSphere(transform.position, animalSightRadius);
                foreach (Collider collider in collidersHit)
                {
                    if (collider.gameObject.TryGetComponent(out Animal animal))
                    {
                        animalsInView.Add(animal);
                    }
                }

                if (animalsInView.Count > 0)
                {
                    currentState = CoyoteStates.Chase;

                    Animal closestAnimal = animalsInView[0];

                    foreach (var animal in animalsInView)
                    {
                        if (Vector3.Distance(animal.transform.position, transform.position) < Vector3.Distance(closestAnimal.transform.position, transform.position))
                        {
                            closestAnimal = animal;
                        }
                    }

                    animalToChase = closestAnimal;
                    animator?.SetBool("Running", true);
                    elaspedchaseTime = 0;

                    return;
                }

                //Wandering around
                if (elaspedMoveTime >= moveTime)
                {
                    isMoving = !isMoving;
                    elaspedMoveTime = 0;

                    if (isMoving)
                    {
                        chosenDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                        transform.forward = chosenDirection;
                    }
                }

                if (isMoving)
                {
                    characterController.SimpleMove(chosenDirection * moveSpeed);

                    animator?.SetBool("Running", true);
                }
                else
                {
                    //Still use the move function so gravity gets applied
                    characterController.SimpleMove(chosenDirection * 0);

                    animator?.SetBool("Running", false);
                }

                elaspedMoveTime += Time.deltaTime;

                break;

            case CoyoteStates.Chase:

                Vector3 direction = (animalToChase.transform.position - transform.position).normalized;
                characterController.SimpleMove(direction * moveSpeed);
                characterController.Move(Vector3.down * Time.deltaTime * moveSpeed);
                transform.forward = direction;

                if (Vector3.Distance(animalToChase.transform.position, transform.position) > animalSightRadius)
                {
                    currentState = CoyoteStates.Wander;

                    return;
                }

                if (elaspedchaseTime >= chaseTimer)
                {
                    currentState = CoyoteStates.Idle;
                    elaspedchaseTime = 0;
                    animator.SetBool("Running", false);

                    return;
                }

                if (Vector3.Distance(animalToChase.transform.position, transform.position) <= attackRange)
                {
                    currentState = CoyoteStates.Attack;
                    animator.SetTrigger("Attack");
                    animator.SetBool("Running", false);
                    isAttacking = true;
                    elaspedAttackTime = 0;

                    return;
                }

                elaspedchaseTime += Time.deltaTime;

                break;

            case CoyoteStates.Attack:

                characterController.SimpleMove(Vector3.zero);

                if (elaspedAttackTime >= attackDuration)
                {
                    Collider[] attackHits = Physics.OverlapSphere(transform.position, attackRange);

                    foreach (Collider hit in attackHits)
                    {
                        if (hit.TryGetComponent(out Animal animal))
                        {
                            animal.Die();
                        }
                    }

                    currentState = CoyoteStates.Wander;
                    isAttacking = false;
                    elaspedAttackTime = 0;
                    
                    return;
                }

                elaspedAttackTime += Time.deltaTime;

                break;

            case CoyoteStates.Idle:

                characterController.SimpleMove(Vector3.zero);

                if (elaspedIdleTime >= idleTime)
                {
                    elaspedIdleTime = 0;

                    currentState = CoyoteStates.Wander;

                    return;
                }

                elaspedIdleTime += Time.deltaTime;

                break;
        }
    }
}

public enum CoyoteStates
{
    Wander,
    Chase,
    Attack,
    Idle
}

