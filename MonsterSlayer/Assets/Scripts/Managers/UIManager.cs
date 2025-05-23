using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyHpText;

    public Image healthBar;

    public RectTransform upgradeGO;
    public Image arrow;
    public Sprite upArrow;
    public Sprite downArrow;
    public Vector3 upgradeOpenPos;
    public Vector3 upgradeClosedPos;
    public bool upgradeUp = false;

    public float coin;
    public float coinMultiplier;

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

        coinMultiplier = 1f;
    }

    public void SetEnemyName(string enemyName)
    {
        enemyNameText.text = enemyName;
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
        enemyHpText.text = $"{FormatNumber(health)}/{FormatNumber(maxHealth)}";
        Debug.Log("Healthbar fill amount: " + healthBar.fillAmount);
    }

    public void UpdateCoinText(float coinReceived)
    {
        Debug.Log("Coin received: " + coinReceived);
        coin += coinReceived;
        coinText.text = FormatNumber(coin);
    }

    public void OnUpgradePressed()
    {
        if (upgradeUp) 
        {
            upgradeGO.DOAnchorPos(upgradeClosedPos, 0.5f);
            arrow.sprite = upArrow;
            upgradeUp = false;
        }
        else
        {
            upgradeGO.DOAnchorPos(upgradeOpenPos, 0.5f);
            arrow.sprite = downArrow;
            upgradeUp = true;
        }
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
