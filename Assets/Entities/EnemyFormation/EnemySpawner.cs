﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10.0f;
    public float height = 5.0f;
    private bool movingRight = true;
    public float speed = 5.0f;
    public float spawnDelay = 0.5f;
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
        SpawnEnemies();
    }
    
    void SpawnEnemies(){
        // create enemy at each assigned position
        foreach (Transform child in transform){
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child; 
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition()) 
        { 
            Invoke("SpawnUntilFull", spawnDelay);
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
        // check if the formation is going outside the playspace
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if(leftEdgeOfFormation <= xmin ){
            movingRight = true;
        }else if (rightEdgeOfFormation >= xmax){
            movingRight = false;
        }
        
        if(AllMembersDead()) {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
	}
    
    Transform NextFreePosition(){
        foreach(Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount == 0){
                return childPositionGameObject;
            }
        }
        return null;
    }
    
    bool AllMembersDead () {
        foreach(Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount > 0){
                return false;
            }
        }
        return true;
    }
}
