using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zycrasController : MonoBehaviour {
	private Rigidbody2D zycrasRigidbody;
	public SpriteRenderer playerSprite;
	public Transform playerTransform;
	public int forceJump;
	public Animator anime;
	public bool jump;
	public bool run;
	public bool grounded;
	public bool attack;
	public bool idle;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float attackTimer;
	public float slideTemp;
	public Transform colisor;
	private Vector2 move = Vector2.zero;
	public float threshold;
	public float knockBackForca;
	public float velocidade = 100f;
	private Rigidbody2D fallground; 
	private float timeTemp;



	// Use this for initialization
	void Start () {
		zycrasRigidbody = gameObject.GetComponent<Rigidbody2D>();
	}




	// Update is called once per frame
	void Update () {
		move.x = Input.GetAxis ("Horizontal");
		grounded =	Physics2D.OverlapCircle(groundCheck.position,	0.2f,	whatIsGround);
		if (move.x > 0) {
			playerSprite.flipX = false;
			run = true;
			zycrasRigidbody.AddForce (new Vector2 (move.x * velocidade, 0));

		} else if (move.x < 0) {
			playerSprite.flipX = true;
			run = true;
			zycrasRigidbody.AddForce ((Vector2.right * velocidade) * move.x);

		} else {
			idle = true;
			run = false;
		}



		if (Input.GetButtonDown("Jump") && grounded == true) {
			zycrasRigidbody.AddForce (new Vector2 (0, forceJump));
			jump = true;
		}

		//respawn
		if (transform.position.y < threshold) {
			Application.LoadLevel (Application.loadedLevel);
			transform.position = new Vector3(20f, 3f, 0);
		}
			



		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);


		anime.SetBool ("jump", !grounded);
		anime.SetBool("idle",idle);
		anime.SetBool ("run", run);



	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("fall")) {
			Debug.Log ("entrei!!");
			fallground = other.gameObject.GetComponent<Rigidbody2D> ();
			StartCoroutine (test (fallground));
		}
	}
		

	public IEnumerator test(Rigidbody2D ground){

		yield return new WaitForSeconds (2);
		print ("3secs");
		ground.velocity = new Vector2 (0, -5);
	}
	/*void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject != null) {
			// se detectar colisão com o hero detrói a moeda
			if (col.gameObject.CompareTag ("spike")) {
				StartCoroutine (KnockBack (0.02f, knockBackForca));
				//Application.LoadLevel (Application.loadedLevel);
			}
		}

	}


	public IEnumerator KnockBack(float knockDur,float knockBackForca){

		float timer = 0;
		while (knockDur > timer){
			timer+=Time.deltaTime;
			zycrasRigidbody.velocity = new Vector2 (0, 0); 
			zycrasRigidbody.AddForce (new Vector3 (transform.position.x * -150, transform.position.y + knockBackForca, transform.position.z));
		}
		yield return 0;
	}*/

}
