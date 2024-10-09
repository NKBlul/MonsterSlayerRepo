using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] float moveXSpeed = 2f;
    [SerializeField] float moveYSpeed = 1f;
    [SerializeField] float popUpTime = 2f;

    public TextMeshPro damageText;
    Color textColor;

    public static void CreateDamagePopup(Vector3 pos, float damage)
    {
        GameObject damagePopup = Instantiate(GameManager.instance.damagePopupPrefab, new Vector2(pos.x + 1.0f, pos.y + 1.0f), Quaternion.identity);
        damagePopup.GetComponent<DamagePopup>().damageText.text = $"-{UIManager.instance.FormatNumber(damage)}";
    }

    void Start()
    {
        
    }

    void Update()
    {

        transform.position += new Vector3(moveXSpeed, moveYSpeed) * Time.deltaTime;
        popUpTime -= Time.deltaTime;
        float dissapearSpeed = 5f;
        damageText.fontSize -= dissapearSpeed * Time.deltaTime;
        if (popUpTime < 0)
        {
            textColor.a -= dissapearSpeed * Time.deltaTime;
            damageText.color = textColor;
            if (damageText.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
