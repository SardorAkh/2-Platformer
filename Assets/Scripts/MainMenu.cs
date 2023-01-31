using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] Transform panel;
    [SerializeField] Transform panelTarget;

    [SerializeField] Transform image;
    [SerializeField] Transform imageTarget;
    [SerializeField] float duration;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void StartGame()
    {
        DOTween.To(() => panel.position, x => panel.position= x, panelTarget.position, duration);
        DOTween.To(() => image.position, v => image.position = v, imageTarget.position, duration).OnComplete(() => SceneManager.LoadScene(1));
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void MusicToggle()
    {
        m_AudioSource.mute = !m_AudioSource.mute;
    }
}
