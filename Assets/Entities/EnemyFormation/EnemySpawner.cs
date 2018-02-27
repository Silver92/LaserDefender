using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10.0f;
    public float height = 5.0f;
    private bool movingRight = true;
    public float speed = 5.0f;
    private float xmax;
    private float xmin;
        
	// Use this for initialization
	void Start () {
        // decide the edge of the enemy formation
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
        Vector3 rightTopEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,1, distanceToCamera));
        xmax = rightTopEdge.x;
        xmin = leftBottomEdge.x;
        // create enemy at each assigned position
        foreach (Transform child in transform){
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child; 
        }
    }
    
    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
 
    // Update is called once per frame
    void Update () {
		if(movingRight){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }else{
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if(leftEdgeOfFormation <= xmin ){
            movingRight = true;
        }else if (rightEdgeOfFormation >= xmax){
            movingRight = false;
        }
        
	}
}
