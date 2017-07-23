using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Licorne : MonoBehaviour 
{
	public bool isRunning = true;

	public float speed = 1;
	public bool canJump = true;
	public bool isJumping = false;
	public float airtime = 50;
	public int lane = 0;
	public bool isGrounded = true;
	public Animator anim;
	public GameObject vomi;
	public GlassManager glassMgr;
	AudioSource audioSrc;

	void Start()
	{
		audioSrc = GetComponent<AudioSource> ();
	}
	void Update ()
	{

			
		if (transform.position.y <= 0)
			transform.position = new Vector3(transform.position.x,0.001f,transform.position.z);
		
		if (transform.position.y >= 3)
			transform.position = new Vector3(transform.position.x,2.999f,transform.position.z);

		if (transform.position.y < 0.1f) 
		{
			anim.SetBool ("Jump", false);

			isGrounded = true;

			anim.SetBool ("Run", true);
		}
		
		if (transform.position.y >= 0.1f)
		{
			anim.SetBool ("Run", false);
			anim.SetBool ("Jump", true);
			isGrounded = false;
		}

		if (Input.GetButtonDown("Vomir") && isGrounded && glassMgr.glassIsFull)
			{
			audioSrc.Play();
			anim.SetBool ("Run", false);
			anim.SetBool ("Vomi", true);
			isRunning = false;
			Instantiate (vomi,transform.position,Quaternion.identity);
			}

		if (Input.GetButtonUp("Vomir"))
		{
			anim.SetBool ("Run", true);
			anim.SetBool ("Vomi", false);
			isRunning = true;
		}

		if (isRunning) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime);
		}

		if (Input.GetButtonDown ("Avance") && isGrounded)
			transform.GetComponent<Rigidbody>().AddForce(Vector3.forward*speed);
		
		if (Input.GetButtonUp ("Saute") && canJump && isGrounded)
		{
			isJumping = true;
			airtime = 0;
			if (transform.position.y < 3)
			transform.GetComponent<Rigidbody> ().AddForce (Vector3.up*1500);
		}

		if (Input.GetButtonUp ("Saute") || airtime <= 0)
		{
			isJumping = false;
		}

		if (airtime <= 0 && canJump) 
		{
			canJump = !canJump;
			isJumping = false;
			transform.GetComponent<Rigidbody>().AddForce(Vector3.down*500);
		}
		if (!isJumping && airtime < 50)
			airtime++;
		
		if (airtime > 49 || transform.position.y < 0.1f)
			canJump = true;

		if (Input.GetButtonDown ("Haut") && lane < 6) 
		{
			lane++;		
			transform.position += new Vector3 (-2.5f,0,0);
		}
		if (Input.GetButtonDown ("Bas") && lane > 0 ) 
		{
			lane--;		
			transform.position += new Vector3 (2.5f,0,0);
		}
	}
}
