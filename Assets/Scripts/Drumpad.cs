using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Drumpad : MonoBehaviour
{
    private AudioSource source;
    private Pad[] buttons;

    void Start()
    {   
        source = GetComponent<AudioSource>();
        buttons = GetComponentsInChildren<Pad>();
        foreach(var g in buttons)
        {
            g.onClick += PlaySound;
        }
    }
    private void Update()
    {
        PressPad();
    }
    private void PressPad()
    {
        foreach(var g in buttons)
        {
            if(Input.GetKeyDown(g.keyCode))
            {
                PlaySound(g);
            }
        }
    }
    void PlaySound(Pad p)
    {
        source.PlayOneShot(p.clip);
    }
}
