using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        IDLE,
        PATROL
    }

    public enum EVENT
    {
        ENTER,
        TICK,
        EXIT
    }

    protected NavMeshAgent navMesh;
    protected Animator animator;
    protected AIController npc;

    protected STATE stateName;
    protected EVENT stateEvent = EVENT.ENTER;

    protected State nextState;

    public State(NavMeshAgent navMesh, Animator animator, AIController aiController)
    {
        this.navMesh = navMesh;
        this.animator = animator;
        this.npc = aiController;
    }


    public virtual void OnEnter()
    {
        stateEvent = EVENT.TICK;
    }


    public virtual void OnTick()
    {
        stateEvent = EVENT.TICK;
    }


    public virtual void OnExit()
    {
        stateEvent = EVENT.EXIT;
    }

    public State Process()
    {
        switch (stateEvent)
        {
            case EVENT.ENTER:
                OnEnter();
                break;
            case EVENT.TICK:
                OnTick();
                break;
            case EVENT.EXIT:
                OnExit();
                return nextState;
        }

        return this;
    }

    protected void SwitchState(State newState)
    {
        Debug.Log("Switching state");
        nextState = newState;
        stateEvent = EVENT.EXIT;
    }
}

public class IdleState : State
{
    private float elapsedTime;
    private float maxWaitTime = 5.0f;

    public IdleState(NavMeshAgent navMesh, Animator animator, AIController npc) : base(navMesh, animator, npc)
    {

    }

    public override void OnEnter()
    {
        animator.SetTrigger("isIdle");
        navMesh.isStopped = true;
        Debug.Log("Transitioning to idle");
        base.OnEnter();

    }

    public override void OnTick()
    {
        base.OnTick();

        elapsedTime += Time.deltaTime;
        if (elapsedTime > maxWaitTime)
        {
            SwitchState(new PatrolState(navMesh, animator, npc));
        }
    }

    public override void OnExit()
    {
        animator.ResetTrigger("isIdle");
        base.OnExit();
    }


}

public class PatrolState : State
{
    int currentIndex = -1;

    private float elapsedTime;
    private float maxWaitTime = 5.0f;


    public PatrolState(NavMeshAgent navMesh, Animator animator, AIController npc) : base(navMesh, animator, npc)
    {

    }

    public override void OnEnter()
    {
        animator.SetTrigger("isPatrolling");
        navMesh.speed = 4.0f;
        navMesh.isStopped = false;
        
        
        base.OnEnter();
    }

    public override void OnTick()
    {
        base.OnTick();


        if (navMesh.remainingDistance < 1)
        {
            if (currentIndex != -1)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime < maxWaitTime)
                {
                    return;
                }
                else
                {
                    elapsedTime = 0.0f;
                }
            }
            if (currentIndex >= npc.Waypoints.Count - 1) //If I have reached the last waypoint
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            navMesh.SetDestination(npc.Waypoints[currentIndex].position);

        }
    }

    public override void OnExit()
    {

        animator.ResetTrigger("isPatrolling");
        base.OnExit();
    

    }


}