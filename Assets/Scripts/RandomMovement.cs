using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour{

    private float xWidth;
    private float yHeight;
    private float zLength;
    private float randx;
    private float randy;
    private float randz;
    private Vector3 destination;
    private SpawnVolume dimensions;
    public float speed = 0.01f;

    void Start(){
      dimensions = GameObject.FindGameObjectWithTag("ObjectContainer").GetComponent<SpawnVolume>();
      xWidth = dimensions.x;
      yHeight = dimensions.y;
      zLength = dimensions.z;
      randomDestination();
      destination = new Vector3(randx,randy,randz);
    }
    void Update(){
      if(Vector3.Distance(this.gameObject.transform.position, destination) < 0.1f){
        randomDestination();
        destination = new Vector3(randx,randy,randz);
      }
      //if performance is the only factor then i would remove the lerp e.g. Vector3.MoveTowards(transform.position, destination, speed);
      transform.position = Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, destination, speed), speed);
    }
    private void randomDestination(){
      randx = Random.Range(0, xWidth);
      randy = Random.Range(0, yHeight);
      randz = Random.Range(0, zLength);
    }
}
