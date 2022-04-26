using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonLogic : MonoBehaviour
{
    private bool buttonsSelected = false;

    [SerializeField]private GameObject activatedButton0;
    [SerializeField]private GameObject activatedButton1;

    private int score;
    private int maxScore;

    private void Start()
    {
        maxScore = GameManager.Instance.GetObjectCount() /2 ;
    }
    public void ActivateButton()
    {
        if (!buttonsSelected)
        {
            activatedButton0 = EventSystem.current.currentSelectedGameObject.gameObject;
            activatedButton0.transform.Find("image").gameObject.SetActive(false);
            activatedButton0.GetComponent<Image>().raycastTarget = false;
            buttonsSelected = true;
        }
        else if (buttonsSelected)
        {
            activatedButton1 = EventSystem.current.currentSelectedGameObject.gameObject;
            activatedButton1.transform.Find("image").gameObject.SetActive(false);
            
            //ayný adsa açýk kal 
            if (activatedButton0.name == activatedButton1.name)
            {
                //Skoru arttýr
                score++;
                Debug.Log(score);
                //Resimleri açýk býrak ve týklanamaz yap
                activatedButton0.GetComponent<Button>().interactable = false;
                activatedButton1.GetComponent<Button>().interactable = false;
            }
            else
            {
                //Delay ver Resimleri geri kapat
                activatedButton0.GetComponent<Image>().raycastTarget = true;
                StartCoroutine(AddDelay());
                
            }
            buttonsSelected = false;

        }

        //Score == objCount/2 level scene açýlýr
        if (score == maxScore)
        {
            //hadis göster
            //SceneManager.LoadScene("LevelsScene");
            //Sonraki levele load et
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            SceneManager.LoadScene("GameScene");
        }
  
    }

    IEnumerator AddDelay()
    {
        yield return new WaitForSeconds(0.3f);
        activatedButton0.transform.Find("image").gameObject.SetActive(true);
        activatedButton1.transform.Find("image").gameObject.SetActive(true);
    }
    public void GoToLevelScene()
    {
        SceneManager.LoadScene("LevelsScene");
    }
    public void SoundButton()
    {
        GameObject.Find("SoundManager").GetComponent<AudioSource>().enabled = !GameObject.Find("SoundManager").GetComponent<AudioSource>().enabled;
    }
}
