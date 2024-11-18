using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DetectClickedObject(Input.mousePosition);
        }
    }

    void DetectClickedObject(Vector3 touchedPos)
    {
        Vector2 clickedPos = Camera.main.ScreenToWorldPoint(touchedPos);

        // Kiểm tra va chạm của Raycast2D
        RaycastHit2D hit = Physics2D.Raycast(clickedPos, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            Card curCard = clickedObject.GetComponent<Card>();
            if(curCard is CardCon)
            {
                curCard.DoTapped();
            }
        }
    }
}
