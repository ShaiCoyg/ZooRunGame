using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float speed; 
  private Rigidbody2D body;
  private Animator anim;
  private bool grounded;
  private bool flag = false;
  private void Awake() {
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  private void Update() { 
    if(flag && Input.GetKey(KeyCode.Space)) {
        flag = false;
        SceneManager.LoadScene("Level 1");
    }
    float horizontalInput = Input.GetAxis("Horizontal");
    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    if (horizontalInput > 0.01f) {
        transform.localScale = Vector3.one;
    }
    if (horizontalInput < -0.01f) {
        transform.localScale = new Vector3(-1, 1, 1);
    }

    if(Input.GetKey(KeyCode.Space) && grounded) {
        Jump();
    }

    anim.SetBool("walk", horizontalInput != 0);
    anim.SetBool("grounded", grounded);

    
  }

  private void Jump() {
    body.velocity = new Vector2(body.velocity.x, speed);
    anim.SetTrigger("jump");
    grounded = false;
  }

  private void OnCollisionEnter2D(Collision2D collision) {
      if(collision.gameObject.tag == "Ground") {
          grounded = true;
      }
    
  }

  void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Ground") {
      }
      if (other.gameObject.tag == "dog") {
          flag = true;
          SceneManager.LoadScene("finish");
      }
      else {
          SceneManager.LoadScene("Level 1");
      }
  }
}
