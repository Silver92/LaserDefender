using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 15.0f;
    private float padding = 0.5f;
    public GameObject projectile;
    public float projectileSpeed = 1f;
    public float firingRate = 0.2f;
    public float health = 100;

    public AudioClip fireSound;

    float xmin;
    float xmax;
    float ymin;
    float ymax;

	// Use this for initialization
	void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightTopMost = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
        xmin = leftBottomMost.x + padding;
        xmax = rightTopMost.x - padding;
        ymin = leftBottomMost.y + padding;
        ymax = rightTopMost.y - padding;
	}
	
    // Fire projectiles
    void Fire () {
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject beam = Instantiate(projectile, transform.position+offset, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
    
	// Update is called once per frame
	void Update () {
    
        // Fire settings
        if (Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }    
        if (Input.GetKeyUp(KeyCode.Space)){
            CancelInvoke("Fire");
        }   
        
        // movement settings 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        
        // restrict the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(newX, newY, transform.position.z);
	    
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {        
        Projectile missile = col.gameObject.GetComponent<Projectile>();
        if (missile) {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0) {
                Die();
            }
        }
    }
    
    void Die(){
        LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        man.LoadLevel("Win Screen");
        Destroy(gameObject);
    }
}
