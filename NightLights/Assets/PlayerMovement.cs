using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canJump;
    public AudioSource source;
    public AudioClip sfxBD, sfxJFX;
    public Vector3 jumpForce;
    public float playerFacing;
    public GameObject UINOTES;
    float speed = 2;
    public Rigidbody2D rb;
    public bool facingRight = true;
    public Animator animator;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerFacing = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(playerFacing));

        if (playerFacing != 0)
        {
            rb.AddForce(new Vector3(playerFacing * speed, 0f));
        }
        if (playerFacing > 0 && !facingRight)
        {
            Flip();
        }

        if (playerFacing < 0 && facingRight)
        {
            Flip();
        }

        if (GetComponent<Transform>().position.y <= -5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        if (collision.gameObject.tag == "Note") {
            Debug.Log("WORKS");
            UINOTES.SetActive(true);
        }

        if (collision.gameObject.tag == "BodySound") {
            source.clip = sfxBD;
            source.Play();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
