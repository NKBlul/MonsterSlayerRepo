using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour
{
    [SerializeField] Vector3 touchArea;
    Enemy enemy;
    Player player;

    void Start()
    {
        enemy = GameManager.instance.enemy;
        player = GameManager.instance.player;
        //foreach (Elements element in Enum.GetValues(typeof(Elements)))
        //{
        //    foreach(Elements element2 in Enum.GetValues(typeof(Elements)))
        //    {
        //        ElementInteraction.ElementDamageMultiplier(element, element2);
        //    }
        //}
    }

    void Update()
    {     
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = MouseToWorldPos();

            if (IsInTouchArea(mousePos) && enemy.canBeHurt)
            {
                player.DealDamage(enemy);
            }
        }

        if (Input.touchCount > 0)
        {
            Vector2 touchPos = TouchToWorldPos();
            if (IsInTouchArea(touchPos) && enemy.canBeHurt)
            {
                Debug.Log(touchPos);
            }
        }
    }

    Vector2 MouseToWorldPos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.transform.position.z * -1; // Set Z to the distance from the camera
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        return new Vector2(mouseWorldPos.x, mouseWorldPos.y);
    }

    Vector2 TouchToWorldPos()
    {
        Touch touch = Input.GetTouch(0);

        // Only handle touch when it begins
        if (touch.phase == TouchPhase.Began)
        {
            // Get the touch position in screen space
            Vector2 touchPosition = touch.position;

            // Convert to world space
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, Camera.main.nearClipPlane));

            // Set the Y position to match your game logic (usually ground level for 2D)
            worldPosition.y = 0; // Adjust this if necessary for your game

            return new Vector2(worldPosition.x, worldPosition.y);
        }

        return Vector2.zero;
    }

    private bool IsInTouchArea(Vector2 touchedPos)
    {
        // Get the bounds of the clickable area
        Vector3 areaMin = transform.position - touchArea / 2;
        Vector3 areaMax = transform.position + touchArea / 2;

        // Check if the position is within the bounds
        return (touchedPos.x >= areaMin.x && touchedPos.x <= areaMax.x) &&
        (touchedPos.y >= areaMin.y && touchedPos.y <= areaMax.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, touchArea);
    }
}
