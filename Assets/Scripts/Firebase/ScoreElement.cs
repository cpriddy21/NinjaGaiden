using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text presentScoreText;

    

    public void NewScoreElement (string _username, int _presentScore)
    {
        usernameText.text = _username;
        presentScoreText.text = _presentScore.ToString();
    }

}