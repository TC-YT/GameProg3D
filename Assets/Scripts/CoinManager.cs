using System.Xml.Schema;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; //singleton for global access

    public TextMeshProUGUI TxtCoin;

    public TextMeshProUGUI TxtCoinTotal;

    public int totalCoins;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount;
        Debug.Log("Coin: " + totalCoins);
        TxtCoin.text = totalCoins.ToString();
        TxtCoinTotal.text = totalCoins.ToString();
    }

    public void Continue(int amount)
    {
        totalCoins -= amount;
    }

    public void CoinRestart()
    {
        totalCoins = 0;
    }
}
