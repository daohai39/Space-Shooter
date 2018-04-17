using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDestroyable {

	public float speed;
	public float tilt;
	public float waitTime;
	public Boundary boundary;
	public Transform spawnBolt;
	public PlayerBolt bolt;

	private AudioSource _sound;
	private Rigidbody _rb;
	private int _health;
	private int _power;
	private float _nextTime;

	public int Health { get { return _health; }}
	public int Power { get { return _power; }}

    private void Awake()
    {
        SetDefault();
    }

	private void Start () 
	{
	}

	private void FixedUpdate()
	{
        Move();
	}

    private void Move()
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

    public PlayerBolt Fire()
    {
        if (Time.time > _nextTime)
        {
            _nextTime = Time.time + waitTime;
            PlayerBolt playerBolt = (PlayerBolt)Instantiate(bolt, spawnBolt.position, spawnBolt.rotation);
            _sound.Play();
            return playerBolt;
        }

        return null;
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
        gameObject.SetActive(false);
	}

    public void SetDefault()
    {
        _rb = GetComponent<Rigidbody>();
        _health = 3;
        _power = 1;
        _sound = GetComponent<AudioSource>();
        transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(true);
    }
}
