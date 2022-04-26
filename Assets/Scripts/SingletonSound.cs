using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSound : MonoBehaviour
{
    public static SingletonSound singletonSound;
    private void Awake()
    {
        if(singletonSound == null)
        {
            singletonSound = this;
        }
        else if(singletonSound != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SoundButton()
    {
        gameObject.GetComponent<AudioSource>().enabled = !gameObject.GetComponent<AudioSource>().enabled;
    }

}
