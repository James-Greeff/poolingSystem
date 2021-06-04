using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindNearestNeighbour : MonoBehaviour{

    private GameObjectPool poolInfo;
    private GameObject tempNeighbour;
    private float shortestDist = 1000;
    private float tempDist;
    private float searchDelay = 0.2f;
    private float searchCounter = 0;
    public GameObject[] activeNeighbours;
    public GameObject nearestNeighbour;

    void Start(){
        poolInfo = GameObject.FindGameObjectWithTag("Pool").GetComponent<GameObjectPool>();
        activeNeighbours = new GameObject[poolInfo.poolSize];
        poolInfo.activeObjectContainer.CopyTo(activeNeighbours, 0);
        shortestDist = Mathf.Infinity;
        nearestNeighbour = this.gameObject;
        searchCounter = 0;
        searchDelay = 0.2f;
    }

    void Update(){
      searchCounter += Time.deltaTime;
      if(searchCounter > searchDelay){ //the delay between a search for a new nearestNeighbour, the speed of the objects is slow enough to allow this optimization.
        compareNeighbours();
      }
      if(nearestNeighbour.activeSelf){
        Debug.DrawLine(this.gameObject.transform.position, nearestNeighbour.transform.position, Color.white, 0);
      }
    }

    public void RefreshNeighbours(){
      Array.Clear(activeNeighbours, 0, activeNeighbours.Length);
      if(activeNeighbours.Length != 0){
        poolInfo.activeObjectContainer.CopyTo(activeNeighbours, 0);
      }
    }

    void OnEnable(){
      nearestNeighbour = this.gameObject; //prevents despawned objects from drawing a line to their old nearestNeighbour when being re-activated.
    }
    public void compareNeighbours(){
      for(int x = 0; x < activeNeighbours.Length; x++){
        tempNeighbour = activeNeighbours[x];
        if(tempNeighbour != null && tempNeighbour != this.gameObject){
          tempDist = (this.gameObject.transform.position - tempNeighbour.transform.position).sqrMagnitude;
          if(tempDist < shortestDist){
            nearestNeighbour = tempNeighbour;
            shortestDist = tempDist;
          }
        }
      }
      shortestDist = Mathf.Infinity;
      searchCounter = 0;
    }
}
