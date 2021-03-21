using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextUpdate : MonoBehaviour
{

    private string formatText = "SPAWN CHANCE: {0}%";
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SliderValueChanged(float value)
    {
        text.text = string.Format(formatText, value);
    }

}
