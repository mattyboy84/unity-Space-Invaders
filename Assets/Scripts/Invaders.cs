using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{

    private float rowSpacing = 2.0f;
    private float columnSpacing = 2.0f;

    private float advanceAmount = 0.2f;

    private float rowBoundryPadding = 1.0f;


    private Vector3 direction = Vector3.right;

    public float initialSpeed = 0.035f;
    public float maximumSpeed = 0.1f;
    private float speed { get; set; }

    public int rows = 5;
    public int columns = 11;

    public int missileCooldown = 5;
    public int mysteryShipCooldown = 10;

    public Projectile missilePrefab;

    public Invader[] prefabs;
    public MysteryShip mysteryShipPrefab;

    private int invadersKilled = 0;
    private int totalInvaders => this.rows * this.columns;
    void Awake()
    {
        speed = initialSpeed;


        for (int row = 0; row < this.rows; row++)
        {
            float width = columnSpacing * (columns - 1);
            float height = rowSpacing * (rows - 1);
            Vector3 center = new Vector3(-width / 2f, -height / 2f);
            Vector3 rowPosition = center + new Vector3(0, row * rowSpacing, 0f);

            for (int column = 0; column < this.columns; column++)
            {
                // This creates an invader at 0,0,0
                Invader invader = Instantiate(prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += column * columnSpacing;
                // This updated its local position relavite to 0,0,0
                invader.transform.localPosition = position;
                //
                invader.killed += onInvaderKilled;
            }
        }
    }

    private void Start()
    {
        // invoke after missileCooldown secs then invoke every missileCooldown secs
        InvokeRepeating(nameof(ShootMissile), missileCooldown, missileCooldown);
        InvokeRepeating(nameof(createMysteryShip), mysteryShipCooldown, mysteryShipCooldown);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        float width = columnSpacing * (columns - 1);

        this.transform.position += (direction * speed);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (direction == Vector3.right && invader.position.x >= (rightEdge.x - rowBoundryPadding)) {
                advanceForward();
            } else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + rowBoundryPadding)) {
                advanceForward();
            }
        }
    }

    private void ShootMissile()
    {

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / ((float)totalInvaders - (float)invadersKilled)))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void createMysteryShip()
    {
        Instantiate(mysteryShipPrefab, new Vector3(0,0,0), Quaternion.identity);
    }

    private void advanceForward()
    {
        direction *= -1;
        Vector3 currentPos = this.transform.position;
        currentPos.y -= advanceAmount;
        this.transform.position = currentPos;
    }

    private void onInvaderKilled()
    {
        invadersKilled++;
        this.speed = initialSpeed + ((maximumSpeed - initialSpeed) * ((float)invadersKilled / (float)totalInvaders));

    }

}
