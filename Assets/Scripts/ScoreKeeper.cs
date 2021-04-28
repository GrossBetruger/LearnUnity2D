using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public int coins;

    public int score;

    public Text scoreKeep;

    private bool levelClear = false;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        score = 0;
        scoreKeep = GetComponent<Text>();
        scoreKeep.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {   
        if (levelClear && coins > 0 ) {
            coins--;
            score += 100;
            // while (score > 0) {
            //     scoreKeep.text = "Coins: " + score;
            //     score--;
            System.Threading.Thread.Sleep(50);
            // }
        } 
        scoreKeep.text = "Coins: " + coins;
        if (score > 0) {
            scoreKeep.text += " Score: " + score;
        }
    }

    public void clearLevel() {
        levelClear = true;
    }
}
