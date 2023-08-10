using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;

    private Vector3 direction = Vector2.zero;
    private float speed = 0.16f;

    private Projectile currentBullet = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            direction = Vector2.left;
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            direction = Vector2.right;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            direction = Vector2.zero;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) == false && ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) == false))
        {
            direction = Vector2.zero;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && currentBullet == null)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += direction * speed;
    }

    private void Shoot()
    {
        currentBullet = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
    }
}
