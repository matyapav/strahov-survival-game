using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class LocomotionAgent : MonoBehaviour
{
    public float movementSpeed = 2.7f;
    public float lastStepLength = 0.4f;
    public float stoppingDistance = 1.0f;
    public float angleTurnLimit = 60;

    private Animator anim;
    private NavMeshAgent agent;

    public Transform target;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = movementSpeed;
        agent.angularSpeed = 900; //huge number helps with clumsy turning
        agent.acceleration = 5;

        // Removes ugly sliding on the last few steps
        // but causes the agent to overshoot the target position sometimes
        agent.autoBraking = false;

        // For debug purposes start navigatin if the target is not empty
        if (target != null) {
            SetDestination(target.position);
        }
    }

    void Debg() {
        // Kills the zombie on pressed K key
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("die");
            agent.isStopped = true;
        }

        // Navigate to mouse cursor position on LMB click
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.isStopped = false;
                agent.SetDestination(hit.point);
            }
        }
    }

    void AnimationControls() {
        // Stop pathfinding on the last step
        if (Vector3.Magnitude(transform.position - agent.destination) < lastStepLength)
        {
            agent.isStopped = true;
        }

        // Limit movement speed when doing sharp turns
        float turnAngle = Vector3.Angle(transform.forward, agent.destination);
        if (turnAngle > angleTurnLimit)
        {
            agent.speed = 0.3f;
        }
        else
        {
            agent.speed = movementSpeed;
        }

        // Call the correct animation
        // TODO add obstacle detection
        anim.SetBool("isWalking", Vector3.Magnitude(transform.position - agent.destination) > stoppingDistance && turnAngle <= angleTurnLimit);
        anim.SetBool("isAttacking", Vector3.Magnitude(transform.position - agent.destination) <= stoppingDistance); //TODO && nearObstacle && pathIncomplete
    }

    void SetDestination(Vector3 _target)
    {
        agent.isStopped = false;
        agent.SetDestination(_target);

        /*
        NavMeshPath navMeshPath = new NavMeshPath();    // Create a new navmesh path
        agent.CalculatePath(_target, navMeshPath);      // Calculate the path

        // Check if the path exist
        if (navMeshPath.status == NavMeshPathStatus.PathComplete) {
            Debug.Log("Path complete");
        }
        else if (navMeshPath.status == NavMeshPathStatus.PathPartial) {
            Debug.Log("Path partial");
        }
        else if (navMeshPath.status == NavMeshPathStatus.PathInvalid) {
            Debug.Log("Path invalid");
        }

        // Assign the path to the agent
        agent.path = navMeshPath;
        */
    }

    void Update()
    {
        // Debug stuff
        Debg();

        // Animation controlling
        AnimationControls();
    }

    // Set the debug walk after one second so that the path is already calculated and everything works
    IEnumerator NavigateAfterASecond(Vector3 _target) {
        yield return new WaitForSeconds(1);
        SetDestination(_target); 
    }

}