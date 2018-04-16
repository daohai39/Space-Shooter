using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    public GameObject playerExplosion;
    public GameObject explosion;

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
            return;
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
	}
}
