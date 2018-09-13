using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f) && Input.GetMouseButtonDown(0))
        {
            hit.collider.GetComponent<Node>().LeftClick();
        }

        if (Physics.Raycast(ray, out hit, 100f) && Input.GetMouseButtonDown(1))
        {
            hit.collider.GetComponent<Node>().RightClick();
        }

    }
}
