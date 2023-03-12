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
    public int lives = 3;

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
        //txtLives = this.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }

            if (Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right * runForce;
            }

            if (Input.GetKey(KeyCode.A))
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
        if (other.CompareTag("Left") || other.CompareTag("Right") || other.CompareTag("Meteor"))
        {
            InvokeRepeating("RedDamageEffect", 0f, 0.5f);
            spriteRenderer.color = Color.white;
            if (lives > 1)
            {
                ite = 4;
                lives -= 1;
                txtLives.text = lives.ToString();
                return;
            }
            else
            {
                lives = 3;
                spriteRenderer.color = Color.white;

                txtLives.text = lives.ToString();
                FindObjectOfType<GameManager>().GameOver();
            }
        }

    }

}
