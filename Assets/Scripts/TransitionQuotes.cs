using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionQuotes : MonoBehaviour
{
    public GameObject canvasGO;
    public Sprite[] quoteSprites;
    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, quoteSprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = quoteSprites[rnd];
    }


    public void ContunieTransitionQuote()
    {
        canvasGO.SetActive(true);
        gameObject.SetActive(false);
    }
}
