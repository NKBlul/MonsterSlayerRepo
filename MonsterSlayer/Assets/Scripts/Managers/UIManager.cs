using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI enemyNameText;

    public Image healthBar;

    public int coin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnemyName(string enemyName)
    {
        enemyNameText.text = enemyName;
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
        Debug.Log("Healthbar fill amount: " + healthBar.fillAmount);
    }

    public void UpdateCoinText(int coinReceived)
    {
        coin += coinReceived;
        coinText.text = FormatNumber(coin);
    }

    public string FormatNumber(float numberToFormat, int decimalPlaces = 2)
    {
        string numberString = numberToFormat.ToString();

        foreach (Suffixes suffix in Enum.GetValues(typeof(Suffixes)))
        {
            var currentValue = 1 * Math.Pow(10, (int)suffix * 3);

            var suffixValue = Enum.GetName(typeof(Suffixes), (int)suffix);

            if ((int)suffix == 0)
            {
                suffixValue = string.Empty;
            }

            if (numberToFormat >= currentValue)
            {
                numberString = $"{Math.Round(numberToFormat / currentValue, decimalPlaces, MidpointRounding.ToEven)}{suffixValue}";
            }
        }

        return numberString;
    }

    private enum Suffixes
    {
        P,
        K,
        M,
        B,
        T,
        Q,
    }
}
