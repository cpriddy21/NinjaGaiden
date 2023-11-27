using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInformation : MonoBehaviour
{
    int timerSeconds;
    public GameObject timer;
    TextMeshProUGUI timerReadout;

    int ryuHealth;
    int shurikens;
    int power;
    // Start is called before the first frame update
    void Start()
    {
        timerSeconds = 300;
        timerReadout = timer.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("timerTiming", 0, 1.0f);
        Debug.Log(timerSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void timerTiming()
    {
        timerSeconds = timerSeconds - 1;
        timerReadout.text = timerSeconds.ToString();
        Debug.Log(timerSeconds);
    }
}
