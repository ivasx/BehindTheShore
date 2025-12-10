using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    
    private Animator animator;
    
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    private const string ATTACK = "Attack";
    private const string TAKEHIT = "TakeHit";
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += EnemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= EnemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit -= EnemyEntity_OnTakeHit;
    }
    
    private void Update()
    {
        animator.SetBool(IS_RUNNING, enemyAI.IsRunning);
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, enemyAI.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonCollider2DTurnOff();
    }
    
    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonCollider2DTurnOn();
    }
    
    private void EnemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }
    
    private void EnemyEntity_OnTakeHit(object sender, EventArgs e)
    {
        animator.SetTrigger(TAKEHIT);
    }
}