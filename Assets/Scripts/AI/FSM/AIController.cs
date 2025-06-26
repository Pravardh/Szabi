using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    private State currentState;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent navmeshAgent;

    [SerializeField]
    private List<Transform> waypoints;

    public List<Transform> Waypoints {  get { return waypoints; } }

    private void Awake()
    {
        currentState = new IdleState(navmeshAgent, animator, this);
        
    }

    private void Update()
    {

        currentState = currentState.Process();
        Debug.Log(currentState);
    }




}
