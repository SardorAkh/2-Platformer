using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Lives : MonoBehaviour
{

    [SerializeField] private Image lifePrefab;

    private List<Image> livesList = new List<Image>();
    [SerializeField] private GameObject UIpanel;
    [SerializeField] private GameObject gameOverPanel;
    public int lives = 3;

    void Start()
    {
        for(int i = 0; i < lives; i++)
        {
            livesList.Add(Instantiate(lifePrefab, UIpanel.transform.position, Quaternion.identity, UIpanel.transform));
        }
        GlobalEvent.OnDamage += DeacreaseLives;

    }

    public void DeacreaseLives()
    {
        Destroy(livesList[0]);
        livesList.RemoveAt(0);
        lives--;
        if (lives <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void OnDestroy()
    {
        GlobalEvent.OnDamage -= DeacreaseLives;
    }
}
