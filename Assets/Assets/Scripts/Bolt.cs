using UnityEngine;

public class Bolt : MonoBehaviour
{
	public float speed;

	public int Id
	{
		get { return _id; }
		set { _id = value; }
	}

	private int _id;

	private void Update()
	{
		Move();
	}

	public virtual void Move()
	{
		GetComponent<Rigidbody>().velocity = speed * transform.forward;
	}

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
