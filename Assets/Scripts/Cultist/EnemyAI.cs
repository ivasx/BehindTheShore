using UnityEngine;
using UnityEngine.AI;
using BehindTheShore.Utils;
using System;

public class EnemyAI : MonoBehaviour
{
    [Header("Roaming Settings")]
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    
    [Header("Chasing Settings")]
    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingDistance = 4f;
    [SerializeField] private float stopChasingDistance = 10f;
    [SerializeField] private float chasingSpeedMultiplier = 2f;
    [SerializeField] private float pathUpdateInterval = 0.2f;
    
    [Header("Attacking Settings")]
    [SerializeField] private bool isAttackingEnemy = false;
    [SerializeField] private float attackingDistance = 1.5f;
    [SerializeField] private float attackingExitBuffer = 0.5f;
    [SerializeField] private float attackRate = 2f;
    
    private float nextAttackTime = 0f;
    private float nextPathUpdateTime = 0f;
    
    private NavMeshAgent navMeshAgent;
    private State currentState;
    private float roamingTimer;
    private Vector3 roamPosition;
    private Vector3 startingPosition;

    private float roamingSpeed;
    private float chasingSpeed;

    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;
    
    public event EventHandler OnEnemyAttack;
     
    public bool IsRunning => navMeshAgent.velocity.sqrMagnitude > 0.01f;
    
    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death,
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        
        navMeshAgent.acceleration = 1000f;
        navMeshAgent.angularSpeed = 3600f;
        navMeshAgent.stoppingDistance = 0.1f;
        
        currentState = startingState;
        
        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Start() 
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }
                CheckCurrentState();
                break;
                
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
                
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
                
            case State.Death:
                navMeshAgent.ResetPath();
                break;
                
            default:
            case State.Idle:
                CheckCurrentState();
                break;
        }
    }

    private void ChasingTarget()
    {
        if (Player.Instance == null) return;
        
        if (Time.time >= nextPathUpdateTime)
        {
            navMeshAgent.SetDestination(Player.Instance.transform.position);
            nextPathUpdateTime = Time.time + pathUpdateInterval;
        }
    }

    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed / roamingSpeed;
    }
    
    private void CheckCurrentState()
    {
        if (Player.Instance == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = currentState;

        switch (currentState)
        {
            case State.Idle:
            case State.Roaming:
                if (isChasingEnemy && distanceToPlayer <= chasingDistance)
                {
                    newState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (!isChasingEnemy || distanceToPlayer > stopChasingDistance)
                {
                    newState = State.Roaming;
                }
                else if (isAttackingEnemy && distanceToPlayer <= attackingDistance)
                {
                    newState = State.Attacking;
                }
                break;

            case State.Attacking:
                if (distanceToPlayer > (attackingDistance + attackingExitBuffer))
                {
                    newState = State.Chasing;
                }
                break;
        }

        if (newState != currentState)
        {
            ChangeState(newState);
        }
    }

    private void ChangeState(State newState)
    {
        if (newState == State.Chasing)
        {
            navMeshAgent.speed = chasingSpeed;
            nextPathUpdateTime = 0f;
        } 
        else if (newState == State.Roaming)
        {
            roamingTimer = 0f;
            navMeshAgent.speed = roamingSpeed;
            navMeshAgent.ResetPath();
        } 
        else if (newState == State.Attacking)
        {
            navMeshAgent.ResetPath();
            navMeshAgent.velocity = Vector3.zero;
        }
        
        currentState = newState;
    }

    private void AttackingTarget()
    {
        ChangeFacingDirection(transform.position, Player.Instance.transform.position);

        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackRate;
        }
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning || currentState == State.Chasing)
            {
                ChangeFacingDirection(transform.position, navMeshAgent.steeringTarget);
            } 
            
            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }
    
    private void Roaming()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (Mathf.Abs(sourcePosition.x - targetPosition.x) < 0.1f) return;

        if (sourcePosition.x > targetPosition.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}