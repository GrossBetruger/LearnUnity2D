using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string sceneName;
    IDictionary<int, string> levelCodeToLevel = new Dictionary<int, string>();
    
    private int numLevels;
    // numberNames.Add(3,"Three");

    void initLevels() {
        levelCodeToLevel.Add(0,"Level1"); //adding a key/value using the Add() method
        levelCodeToLevel.Add(1,"Level2");
        // levelCodeToLevel.Add(3,"end");
        numLevels = levelCodeToLevel.Count;

    }

    int getCodeByLevel(string level) {
        return levelCodeToLevel.FirstOrDefault(x => x.Value == level).Key;
    }

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        initLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn() {
        Debug.Log("respawning...");
        SceneManager.LoadScene(this.sceneName);
    }

    public void ClearLevel()
    {
        Debug.Log("clearing level...");
        Debug.Log("current level name: " + this.sceneName);
        Debug.Log("# of levels: " + this.numLevels);
        int currentLevelCode = getCodeByLevel(this.sceneName);
        Debug.Log("current level: " + currentLevelCode);
        int nextLevelCode = (currentLevelCode + 1) % numLevels; 
        Debug.Log("next level: " + nextLevelCode);
        string nextLevel;
        this.levelCodeToLevel.TryGetValue(nextLevelCode, out nextLevel);
        SceneManager.LoadScene(nextLevel);
    }
}
