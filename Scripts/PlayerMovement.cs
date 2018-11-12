using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public bool letJump;

	public GameManager manager; 
	public float moveSpeed;
	public GameObject deathParticles;
	private float maxSpeed = 5f;
	private Vector3 input;

	private Vector3 spawn;
	public AudioClip deathSound;
	public AudioClip jumpSound;
	public AudioClip pickUpSound;
	public AudioClip endLevelSound;
	public AudioClip enemySound; 
	// Use this for initialization
	void Start () {
		spawn = transform.position;
		manager = manager.GetComponent<GameManager>();
	}
	

	
	
	void FixedUpdate () {
		input = new Vector3(Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
	/*	if(GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
		{
			GetComponent<Rigidbody>().AddRelativeForce(input * moveSpeed);
		}
		*/
		/*
		if (Input.GetKeyDown ("space"))
		{
			//	transform.Translate(Vector3.up * 100 * Time.deltaTime, Space.World);
			rigidbody.AddForce(Vector3.up * 600);
		} 
		*/
		//New way to jump
		if (Input.GetKeyDown("space"))
		{
			if (letJump){
				GetComponent<AudioSource>().PlayOneShot(jumpSound, 5);
				GetComponent<Rigidbody>().AddForce(Vector3.up * 600);
				letJump = false;
			}
		}
		if (transform.position.y < -2)
		{
			Die ();
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Enemy")
		{
			Die ();
		}
		if (other.transform.tag == "Spike")
		{
			Die ();
		}
		//Different tag required so that players cannot destory spikes by jumping on them
		if (other.transform.tag == "Floor") 
		{
			letJump = true;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Spike")
		{
			Die ();
		}
		//Different tag required so that players cannot destory spikes by jumping on them

		if (other.transform.tag == "Enemy")
		{
			GetComponent<AudioSource>().PlayOneShot(enemySound, 5);
			Destroy(other.gameObject);
		}

		if (other.transform.tag == "Token")
		{
			GetComponent<AudioSource>().PlayOneShot(pickUpSound, 5);
			manager.tokenCount += 1;
			Destroy(other.gameObject);
		}
		if (other.transform.tag == "Goal")
		{
			GetComponent<AudioSource>().PlayOneShot(endLevelSound, 2);
			manager.CompleteLevel();
		}
	}
	
	void Die()
	{
		GetComponent<AudioSource>().PlayOneShot(deathSound, 5);
		Instantiate(deathParticles, transform.position, Quaternion.Euler(270,0,0));
		transform.position = spawn;
	}
}
