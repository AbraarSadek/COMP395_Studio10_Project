using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

    public Transform counterPoint;
    public List<Transform> queuePositions;

    private Queue<Customer> queue = new Queue<Customer>();

    private Customer currentCustomer = null;
    private bool counterBusy = false;

    public void JoinQueue(Customer customer) {

        queue.Enqueue(customer);

        UpdateQueuePositions();

        if (!counterBusy) {
            ServeNext();
        }

    }

    void ServeNext() {

        if (queue.Count == 0) {
            counterBusy = false;
            currentCustomer = null;
            return;
        }

        counterBusy = true;

        currentCustomer = queue.Peek();

        currentCustomer.MoveTo(counterPoint.position);
    
    }

    public void CustomerFinished() {

        if (queue.Count > 0) {
            queue.Dequeue();
        }

        counterBusy = false;
        currentCustomer = null;

        UpdateQueuePositions();
        ServeNext();

    }

    void UpdateQueuePositions() {

        int index = 0;

        foreach (Customer customer in queue) {
        
            if (customer == currentCustomer)
                continue;

            if (index < queuePositions.Count) {
                customer.MoveTo(queuePositions[index].position);
                index++;
            }
        
        }
    
    }

    public bool IsAtCounter(Customer customer) {
        return customer == currentCustomer;
    }

}