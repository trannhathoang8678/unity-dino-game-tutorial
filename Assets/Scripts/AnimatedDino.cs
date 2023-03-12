using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedDino : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {

            if (frame >= sprites.Length)
            {
                frame = 2;
            }

            if (frame >= sprites.Length - 2 && frame < sprites.Length)
            {
                spriteRenderer.sprite = sprites[frame];
            }
        }
        else
        {
            if (frame >= sprites.Length - 2)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < sprites.Length - 2)
            {
                spriteRenderer.sprite = sprites[frame];
            }
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
