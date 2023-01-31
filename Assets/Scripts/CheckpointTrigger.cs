using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    private bool _state;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GlobalEvent.OnChangeCheckpoint += ResetCheckpoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!_state)
            {   
                GlobalEvent.InvokeReachCheckpoint(this);
                _state = true;
                _animator.SetBool("CheckPoint", true);
            }
        }
    }
    private void ResetCheckpoint()
    {
        _spriteRenderer.sprite = _sprites[0];
        _animator.SetBool("CheckPoint", false);

    }

    private void OnDestroy()
    {
        GlobalEvent.OnChangeCheckpoint -= ResetCheckpoint;
    }
}
