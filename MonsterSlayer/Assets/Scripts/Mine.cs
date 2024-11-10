using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int coinIncrease = 3;
    public float mineTime = 5f;

    private void Start()
    {
        StartCoroutine(GatherCoin());
    }

    private IEnumerator GatherCoin()
    {
        yield return new WaitForSeconds(mineTime);
        MineCoin();
        StartCoroutine(GatherCoin());
    }

    private void MineCoin()
    {
        CoinPopup.CreateCoinPopup(transform.position, coinIncrease);
        UIManager.instance.coin += coinIncrease;
    }
}
