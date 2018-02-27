using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public GameObject projectile;
    public float projectileSpeed = 10;
    public float firingRate = 0.8f;
    public float health = 190;
    public float shotsPerSeconds = 0.5f;
    
    
    void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability){
            Fire();
        }
    }
    
    void Fire () {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0); 
        GameObject missile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {        
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
