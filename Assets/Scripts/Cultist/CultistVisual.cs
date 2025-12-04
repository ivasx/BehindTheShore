using System;
using UnityEngine;

public class CultistVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemyAI;
    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string CHASING_SPEED_MULTIPLIER = "ChasingSpeedMultiplier";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, _enemyAI.IsRunning);
        animator.SetFloat(CHASING_SPEED_MULTIPLIER, _enemyAI.GetRoamingAnimationSpeed());
    }
}
