using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;

    private QueueManager queueManager;
    private Transform exitPoint;

    private float orderTime;
    private float foodTime;

    private bool isBeingServed = false;

    void Awake() {

        agent = GetComponent<NavMeshAgent>();

        if (agent == null) {
            Debug.LogError("NavMeshAgent missing on Customer prefab.");
        }

    }

    //Called by SimulationManager when the customer spawns
    public void Initialize(QueueManager manager, Transform exit, float orderWait, float foodWait) {

        queueManager = manager;
        exitPoint = exit;

        orderTime = orderWait;
        foodTime = foodWait;

        if (queueManager == null) {
            Debug.LogError("QueueManager reference missing.");
            return;
        }

        queueManager.JoinQueue(this);
    
    }

    //Move customer to a position (queue spot or counter)
    public void MoveTo(Vector3 position) {
        if (agent != null) {
            agent.SetDestination(position);
        }
    }

    //Called when the customer reaches the counter
    public void StartService() {
 
        if (!isBeingServed) {
            isBeingServed = true;
            StartCoroutine(ProcessOrder());
        }
    
    }

    // Handles ordering and food preparation time
    IEnumerator ProcessOrder() {

        //Ordering time
        yield return new WaitForSeconds(orderTime);

        //Food preparation time
        yield return new WaitForSeconds(foodTime);

        //Inform queue manager that service finished
        queueManager.CustomerFinished();

        //Leave the cafeteria
        if (exitPoint != null)
        {
            agent.SetDestination(exitPoint.position);
        }

        //Wait until customer reaches exit
        while (agent.pathPending || agent.remainingDistance > 0.5f) {
            yield return null;
        }

        Destroy(gameObject);
    }

    //Check if customer reached counter
    void Update() {

        if (isBeingServed)
            return;

        if (queueManager == null)
            return;

        if (agent == null)
            return;

        if (!agent.pathPending && agent.remainingDistance <= 0.5f) {

            if (queueManager.IsAtCounter(this)) {
                StartService();
            }
        
        }
    
    }

}