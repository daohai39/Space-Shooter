using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public int xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public float waitTime;
	public Boundary boundary;
	public Transform spawnBolt;
	public GameObject bolt;

	private AudioSource _sound;
	private Rigidbody _rb;
	private int _health;
	private int _power;
	private float _nextTime;

	public int Health { get { return _health; }}
	public int Power { get { return _power; }}

	// Use this for initialization
	private void Start () 
	{
		_rb = GetComponent<Rigidbody>();
		_health = 3;
		_power = 1;
		_sound = GetComponent<AudioSource>();
	}
//
//	private void Update()
//	{
//        if (Input.GetButton("Fire1") && Time.time >= _nextTime)
//        {
//            _nextTime = Time.time + waitTime;
//            PlayerBolt bolt = (PlayerBolt)Instantiate(bolt, spawnBolt.position, spawnBolt.rotation);
//            
//            _sound.Play();
//        }
//	}

	public PlayerBolt Fire()
	{
		if (Time.time >= _nextTime)
		{
			_nextTime = Time.time + waitTime;
			PlayerBolt bolt = (PlayerBolt)Instantiate(bolt, spawnBolt.position, spawnBolt.rotation);
			_sound.Play();
			return bolt;
		}

		return null;
	}

	private void FixedUpdate()
	{
		var moveHorizontal = Input.GetAxis("Horizontal");
		var moveVertical = Input.GetAxis("Vertical");
		var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		_rb.velocity = movement * speed;
		_rb.position = new Vector3(
			Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
		);

		_rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * -tilt);    
	}

	public void AddHealth()
	{
		_health++;
	}

	public void ReduceHealth()
	{
		if (_health > 0) _health--;
	}

	public void AddPower()
	{
		_power++;
	}

	public void ReducePower()
	{
		if(_power > 1) _power--;
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
