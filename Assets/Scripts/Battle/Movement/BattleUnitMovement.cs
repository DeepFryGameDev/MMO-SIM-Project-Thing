using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BattleUnitMovement : MonoBehaviour
{
    NavMeshAgent agent;

    bool isRunningToTarget = false, isRunningToOrigin;
    public bool GetIsRunningToTarget() { return isRunningToTarget; }
    public bool GetIsRunningToOrigin() { return isRunningToOrigin; }

    Vector3 originPosition;
    Quaternion originRotation;
    public void SetOriginRotation() { originRotation = transform.rotation; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        originPosition = transform.position;
    }

    private void Update()
    {
        if (isRunningToTarget && agent.remainingDistance <= agent.stoppingDistance)
        {
            isRunningToTarget = false;
            agent.ResetPath();
        }

        if (isRunningToOrigin && agent.remainingDistance <= agent.stoppingDistance)
        {
            isRunningToOrigin = false;
            agent.ResetPath();

            transform.rotation = originRotation; // reset rotation when returning to origin
        }
    }

    public void RunToTarget(BaseAttackableUnit targetUnit)
    {
        isRunningToTarget = true;

        agent.speed = BattleSettings.agentRunToTargetRunSpeed;

        agent.stoppingDistance = BattleSettings.agentStoppingDistanceToTarget;

        transform.LookAt(targetUnit.transform.position);       

        agent.SetDestination(targetUnit.transform.position);        
    }

    public void RunToOrigin()
    {
        isRunningToOrigin = true;

        agent.stoppingDistance = BattleSettings.agentStoppingDistanceToDestination;

        transform.LookAt(originPosition);
        agent.SetDestination(originPosition);
    }
}
