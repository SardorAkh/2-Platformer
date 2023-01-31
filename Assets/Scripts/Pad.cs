using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pad : MonoBehaviour
{   
    [SerializeField] protected internal AudioClip clip;
    [SerializeField] protected internal KeyCode keyCode;
    public Action<Pad> onClick;

    public void Press()
    {
        onClick?.Invoke(this);
    }
}
