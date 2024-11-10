using TMPro;
using UnityEngine;

public class CoinPopup : MonoBehaviour
{
    [SerializeField] float moveYSpeed = 1f;
    [SerializeField] float popUpTime = 2f;

    public TextMeshPro coinText;
    Color textColor;

    public static void CreateCoinPopup(Vector3 pos, float coin)
    {
        GameObject coinPopup = Instantiate(GameManager.instance.coinPopupPrefab, new Vector2(pos.x, pos.y + 1.0f), Quaternion.identity);
        coinPopup.GetComponent<CoinPopup>().coinText.text = $"+{UIManager.instance.FormatNumber(coin)}";
    }

    void Start()
    {

    }

    void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        popUpTime -= Time.deltaTime;
        float dissapearSpeed = 5f;
        coinText.fontSize -= dissapearSpeed * Time.deltaTime;
        if (popUpTime < 0)
        {
            textColor.a -= dissapearSpeed * Time.deltaTime;
            coinText.color = textColor;
            if (coinText.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
