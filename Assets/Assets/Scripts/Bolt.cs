using UnityEngine;

public class Bolt : MonoBehaviour
{
    public float speed;

	private void Update()
	{
        Move();
	}

    public virtual void Move()
    {
        GetComponent<Rigidbody>().velocity = speed * transform.forward;
    }
}
