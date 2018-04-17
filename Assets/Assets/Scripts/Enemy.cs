using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDestroyOnContact
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

	abstract public void Move();

	private void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag(Tag.Boundary) || other.CompareTag(Tag.Enemy))
		{
			return;
		}

		//destroy this
		_gameController.EnemyManager.DestroyEnemy(id);


		//anim
		Instantiate(explosion, transform.position, transform.rotation);


	   
		Destroy(other);

	}

	public virtual void Destroy(Collider other)
	{

		//destroy bullet
		//if (other.CompareTag(Tag.PlayerBolt)) {
		//    Destroy(other.gameObject);
		//}

		PlayerBolt playerBolt = other.gameObject.GetComponent<PlayerBolt>();
		if (playerBolt != null)
		{
		    _gameController.BoltManager.RemovePlayerBolt(playerBolt.Id);
//			Destroy(other.gameObject);
		}



		//destroy player
		//if (other.CompareTag(Tag.Player)) {
		//    _gameController.ReducePower();
		//    _gameController.ReduceHealth();
		//    if (_gameController.IsGameOver()) {
		//        Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		//        Destroy(other.gameObject);
		//        _gameController.GameOver();
		//    }
		//}

		PlayerController player = other.gameObject.GetComponent<PlayerController>();
		if (player != null)
		{
			_gameController.ReducePower();
			_gameController.ReduceHealth();
			if (_gameController.IsGameOver())
			{
				Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
				player.DestrorySelf();
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
