using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{

    GameObject top;
    GameObject bottom;
    GameObject left;
    GameObject right;


    void Awake()
    {
        top = new GameObject("Top");
        bottom = new GameObject("Bottom");
        left = new GameObject("Left");
        right = new GameObject("Right");

        top.transform.parent = transform;
        bottom.transform.parent = transform;
        left.transform.parent = transform;
        right.transform.parent = transform;

        top.tag = tag;
        bottom.tag = tag;
        left.tag = tag;
        right.tag = tag;

        top.layer = gameObject.layer;
        bottom.layer = gameObject.layer;
        left.layer = gameObject.layer;
        right.layer = gameObject.layer;
    }


    void Start()
    {
        CreateScreenColliders();
    }


    void CreateScreenColliders()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        //// Create top collider
        BoxCollider2D collider = top.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;

        top.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, topRightScreenPoint.y, 0f);


        // Create bottom collider
        collider = bottom.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;

        //** Bottom needs to account for collider size
        bottom.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - collider.size.y, 0f);


        // Create left collider
        collider = left.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;

        //** Left needs to account for collider size
        left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);


        // Create right collider
        collider = right.AddComponent<BoxCollider2D>();
        collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
        collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
        collider.isTrigger = true;

        right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);
    }
}
