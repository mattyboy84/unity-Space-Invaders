using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    private float speed = 0.045f;
    private Vector3 direction = Vector3.right;

    private float leftBound;
    private float rightBound;

    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        leftBound = leftEdge.x;
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        rightBound = rightEdge.x;

        this.transform.position = new Vector3(leftEdge.x - 2f, 13, 0);
    }


    void Update()
    {
        this.transform.position += (direction * speed);

        if (this.transform.position.x > rightBound + 2f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Destroy(this.gameObject);
        }
    }
}
