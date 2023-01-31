using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class EndGame : MonoBehaviour
{
    [SerializeField] private Transform winPanel;
    [SerializeField] private Transform winPanelTarget;
    [SerializeField] private Coins coins;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int scoreForCoin;

    private void Start()
    {
        scoreForCoin = 500;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.CompareTag("Player"))
        {
            winPanel.gameObject.SetActive(true);
            DOTween.To(() => winPanel.position, v => winPanel.position = v, winPanelTarget.position, 2).OnComplete(() =>
            {
                Time.timeScale = 0.8f;
                int number = 0;
                DOTween.To(() => number, (x) => number = x, coins.coin * scoreForCoin, 2.0f).OnUpdate(() =>
                {
                    scoreText.text = "Score: " + number;
                    if (number % scoreForCoin == 0 && coins.coin != 0)
                    {
                        GlobalEvent.InvokeOnCoinDecrease();
                    }
                });
            });
        }
    }

    public void NextLevel()
    {
        winPanel.gameObject.SetActive(false);

        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
