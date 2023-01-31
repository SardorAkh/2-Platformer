using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Coins : MonoBehaviour
{
    private TextMeshProUGUI textCoin;
    [SerializeField] public int coin { get; private set; }
    void Start()
    {
        textCoin = GetComponent<TextMeshProUGUI>();
        GlobalEvent.OnCoinCollection += CollectionCoin;
        GlobalEvent.OnCoinDecrease += DecreaseCoin;
    }
    public void CollectionCoin()
    {
        coin++;
        textCoin.text = "Coin: " + coin;
    }
    public void DecreaseCoin()
    {
        coin--;
        textCoin.text = "Coin: " + coin;
    }
    private void OnDestroy()
    {
        GlobalEvent.OnCoinCollection -= CollectionCoin;
        GlobalEvent.OnCoinDecrease -= DecreaseCoin;

    }
}
