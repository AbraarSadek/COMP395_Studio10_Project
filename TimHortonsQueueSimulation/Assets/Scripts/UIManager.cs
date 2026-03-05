using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public TMP_Text arrivalText;
    public TMP_Text orderWaitText;
    public TMP_Text foodWaitText;

    public TMP_Text speedLabel;
    public Slider speedSlider;

    void Start() {
    
        speedSlider.onValueChanged.AddListener(UpdateSpeed);

    }

    public void DisplayCustomerData(CustomerData data) {

        arrivalText.text = "Arrival Time: " + data.arrivalTime.ToString("F1");
        orderWaitText.text = "Order Wait Time: " + data.orderWaitTime.ToString("F1");
        foodWaitText.text = "Food Wait Time: " + data.foodWaitTime.ToString("F1");
    
    }

    public void UpdateSpeed(float value) {
    
        Time.timeScale = value;

        speedLabel.text = "Sim Speed: " + value.ToString("F1") + "x";
    
    }

}