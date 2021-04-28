using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int score;
    public Text scoreKeep;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreKeep = GetComponent<Text>();
        scoreKeep.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        scoreKeep.text = "Coins: " + score;
    }
}
