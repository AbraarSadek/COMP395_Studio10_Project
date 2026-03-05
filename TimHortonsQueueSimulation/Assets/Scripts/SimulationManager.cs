using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour {

    public GameObject customerPrefab;

    public Transform spawnPoint;
    public Transform exitPoint;

    public QueueManager queueManager;

    public UIManager uiManager;

    public List<CustomerData> customers = new List<CustomerData>();

    private float simulationTimer = 0f;
    private int nextCustomerIndex = 0;

    private static SimulationManager _instance;
    public static SimulationManager Instance => _instance;

    private void Awake() {

        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    
    }

    void Start() {
        StartCoroutine(RunSimulation());
    }

    IEnumerator RunSimulation() {

        while (nextCustomerIndex < customers.Count) {

            simulationTimer += Time.deltaTime;

            CustomerData data = customers[nextCustomerIndex];

            if (simulationTimer >= data.arrivalTime) {
                SpawnCustomer(data);
                nextCustomerIndex++;
            }

            yield return null;

        }

    }

    void SpawnCustomer(CustomerData data) {

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        Customer customer = obj.GetComponent<Customer>();

        if (customer == null) {
            Debug.LogError("Customer script missing on prefab.");
            return;
        }

        customer.Initialize(queueManager, exitPoint, data.orderWaitTime, data.foodWaitTime);

        if (uiManager != null) {
            uiManager.DisplayCustomerData(data);
        }

    }

}