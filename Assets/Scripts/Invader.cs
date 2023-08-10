using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationTime = 1.0f;

    private SpriteRenderer spriteRenderer;
    private int spriteFrame = 0;

    public System.Action killed;

    private void Awake() // Called before the start function
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating(nameof(AnimationSprite), 0, animationTime);
    }


    void Update()
    {
        
    }

    private void AnimationSprite()
    {
        spriteFrame++;
        if (spriteFrame >= sprites.Length)
        {
            spriteFrame = 0;
        }
        spriteRenderer.sprite = sprites[spriteFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.gameObject.SetActive(false);
            this.killed.Invoke();
        }
    }
}
