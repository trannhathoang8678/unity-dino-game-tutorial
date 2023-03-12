using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float runForce = 5f;
    public float gravity = 9.81f * 2f;
    public int hp = 100;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    private TextMeshProUGUI txtLives;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction = Vector3.up * jumpForce;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction = Vector3.right * runForce;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Vector3.left * runForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private int ite = 4;

    private void RedDamageEffect()
    {
        spriteRenderer.color = Color.red;
        InvokeRepeating("ResetColor", 0.25f, 0.5f);
    }

    private void ResetColor()
    {
        spriteRenderer.color = Color.white;
        ite--;
        if (ite == 0)
        {
            CancelInvoke();
            ite = 4;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        InvokeRepeating("RedDamageEffect", 0f, 0.5f);
        spriteRenderer.color = Color.white;
        if (hp > 25)
        {
            ite = 4;
            if (other.CompareTag("Meteor"))
            {
                hp -= 50;
            } 
            else
            {
                hp -= 25;
            }
            txtLives.text = hp.ToString();
        }
        else
        {
            hp = 100;
            spriteRenderer.color = Color.white;
            txtLives.text = hp.ToString();
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
