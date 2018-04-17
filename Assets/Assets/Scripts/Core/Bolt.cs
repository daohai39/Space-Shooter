using UnityEngine;

public class Bolt : MonoBehaviour, IDestroyable
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

	protected virtual void Move()
	{
		GetComponent<Rigidbody>().velocity = speed * transform.forward;
	}

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
