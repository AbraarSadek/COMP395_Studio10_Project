using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationReset : MonoBehaviour {

    public void ResetSimulation() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}