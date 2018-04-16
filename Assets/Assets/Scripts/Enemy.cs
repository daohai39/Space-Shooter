using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDestroyOnContact
{
    public int id;
    public int point;

    public GameObject playerExplosion;
    public GameObject explosion;

    protected GameController _gameController;

    abstract public void Move();

    public void SetID(int id) {
        this.id = id;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Tag.Boundary) || other.CompareTag(Tag.Enemy))
        {
            return;
        }

        //destroy this
        _gameController._enemyManager.DestroyEnemy(id);


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

        PlayerBolt playerbolt = other.gameObject.GetComponent<PlayerBolt>();
        if (playerbolt != null)
        {
            Destroy(other.gameObject);
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
                Destroy(other.gameObject);


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
