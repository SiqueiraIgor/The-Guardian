using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour {

    public float velocidade;

    public Transform player;
    private Animator animator;

    public bool isGrounded;
    public float forca;
    public float JumpTime = 0.6f;
    public float jumpDelay = 0.6f;
    public bool jumped;
    public Transform ground;

	// Use this for initialization
	void Start () {

        animator = player.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
        Movimentar();
		
	}

    void Movimentar()
    {
        isGrounded = Physics2D.Linecast(this.transform.position, ground.position, 1 << LayerMask.NameToLayer("Plataforma"));
        animator.SetFloat("walk", Mathf.Abs(Input.GetAxis("Horizontal")));
        if(Input.GetAxisRaw("Horizontal")> 0)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * forca);
            JumpTime = jumpDelay;
            animator.SetTrigger("jump");
            jumped = true;
        }

        JumpTime -= Time.deltaTime;

  
        if(JumpTime<=0 && isGrounded && jumped)
        {
            animator.SetTrigger("ground");
            jumped = false;
        }
    }
}
