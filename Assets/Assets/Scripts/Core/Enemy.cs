using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDestroyable
{
	public int Id
	{
		get { return _id; }
		set { _id = value; }
	}
	private int _id;
	public int point;

	public GameObject playerExplosion;
	public GameObject explosion;

	protected GameController _gameController;

	abstract protected void Move();

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag(Tag.Boundary) || other.CompareTag(Tag.Enemy))
		{
			return;
		}

		//destroy this
		_gameController.EnemyManager.DestroyEnemy(_id);


		//anim
		Instantiate(explosion, transform.position, transform.rotation);


	   
		Destroy(other);

	}

	protected virtual void Destroy(Collider other)
    {

		PlayerBolt playerBolt = other.gameObject.GetComponent<PlayerBolt>();
		if (playerBolt != null)
		{
            _gameController.PlayerBoltManager.RemoveBolt(playerBolt.Id);
		}

		PlayerController player = other.gameObject.GetComponent<PlayerController>();
		if (player != null)
		{
			_gameController.ReducePower();
			_gameController.ReduceHealth();
			if (_gameController.IsGameOver())
			{
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                _gameController.player.DestroySelf();
				_gameController.GameOver();
			}
		}
		//add score
		_gameController.AddScore(point);
	}

	public void DestroySelf() {
		Destroy(gameObject);
	}
}
