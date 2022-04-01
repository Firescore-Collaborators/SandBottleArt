using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using Tabtale.TTPlugins;
public class LevelManager : MonoBehaviour
{

    public Text levelText;

    void Awake()
    {
        TTPCore.Setup();
    }
    void Start()
    {
        CheckSameLevel();
        SetLevelIndex();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int sceneLength = SceneManager.sceneCountInBuildSettings;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene < sceneLength)
        {
            SetLevelPref(nextScene);
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            IncreaseLoopIndex();
            SetLevelPref(0);
            SceneManager.LoadScene(0);

        }

    }

    [Button]
    public void SetLevelIndex()
    {
        int level = (SceneManager.GetActiveScene().buildIndex+1)+(PlayerPrefs.GetInt("Loop")*SceneManager.sceneCountInBuildSettings);
        levelText.text = "Level " + level;
    }

    public void IncreaseLoopIndex()
    {
        PlayerPrefs.SetInt("Loop",PlayerPrefs.GetInt("Loop")+1);
    }

    public void SetLevelPref(int level)
    {
        PlayerPrefs.SetInt("Level",level);
    }

    void CheckSameLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        }
    }
}
