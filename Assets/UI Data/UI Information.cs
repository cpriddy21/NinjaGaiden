using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInformation : MonoBehaviour
{
    int timerSeconds;
    public GameObject timer;
    TextMeshProUGUI timerReadout;

    int scorePoints;
    public GameObject score;
    TextMeshProUGUI scoreReadout;

    int ryuHealth;
    int shurikens;
    int power;
    // Start is called before the first frame update
    void Start()
    {
        timerSeconds = 300;
        timerReadout = timer.GetComponent<TextMeshProUGUI>();
    
        scorePoints = 0;
        scoreReadout = score.GetComponent<TextMeshProUGUI>();
        updateScore(100);
        
        InvokeRepeating("timerTiming", 0, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void timerTiming()
    {
        timerSeconds = timerSeconds - 1;
        timerReadout.text = timerSeconds.ToString();
        
    }

    public void updateScore(int points) {
        scorePoints = scorePoints + points;
    
        string s = scorePoints.ToString();

        for(int i = s.Length; i < 8; i++){
            s = "0" + s;
        }

        Debug.Log(s);
        
        scoreReadout.text = s;

        Debug.Log(scoreReadout.text);
    }
}
