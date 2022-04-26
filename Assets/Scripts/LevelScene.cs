using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class LevelScene : MonoBehaviour
{
   
    void Start()
    {
        
    }


  
    public void LoadLevel()
    {
        string levelIndex = EventSystem.current.currentSelectedGameObject.name;
        PlayerPrefs.SetInt("Level", int.Parse(levelIndex));
        SceneManager.LoadScene("GameScene");
    }
}
