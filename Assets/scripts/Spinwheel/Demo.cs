using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;

public class Demo : MonoBehaviour
{

    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI uiSpinbuttonText;
        void Start()
    { pickerWheel.OnSpinStart(() => {
        spinButton.interactable = false;
            Debug.Log("Spin started!");
        });
      pickerWheel.OnSpinEnd((wheelPiece) => {
            Debug.Log("Spin ended! Landed on: " + wheelPiece.Label + " with chance: " + wheelPiece.Chance);
            uiSpinbuttonText.text = "SPIN";
        });  
        spinButton.interactable = true;
        uiSpinbuttonText.text = "SPIN";
        spinButton.onClick.AddListener(() =>
        {   
                pickerWheel.Spin();
            uiSpinbuttonText.text = "SPINING";
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
