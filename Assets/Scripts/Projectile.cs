using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        this.transform.position += (direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
