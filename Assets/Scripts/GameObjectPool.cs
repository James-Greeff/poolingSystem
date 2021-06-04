using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameObjectPool : MonoBehaviour{

    public GameObject prefabToPool;
    public Queue<GameObject> activeObjectContainer = new Queue<GameObject>();
    public TextMeshProUGUI objectCounter;
    public int poolSize = 10;
    private Queue<GameObject> despawnContainer = new Queue<GameObject>();

    public GameObject GetFromPool(){
      if(activeObjectContainer.Count < poolSize && despawnContainer.Count == 0){
        GameObject newObject = GameObject.Instantiate(prefabToPool);
        return addToActiveQueue(newObject);
      }
      else if(despawnContainer.Count == 0){
        GameObject recycleObject = activeObjectContainer.Dequeue();
        return addToActiveQueue(recycleObject);
      }
      else{ //spawns an object from the inactive queue
        GameObject respawnObject = despawnContainer.Dequeue();
        return addToActiveQueue(respawnObject);
      }
    }

    public void Despawn(){
      GameObject despawnObject = activeObjectContainer.Dequeue();
      despawnObject.SetActive(false);
      despawnContainer.Enqueue(despawnObject);
      objectCounter.text = "" + activeObjectContainer.Count;
    }

    public GameObject addToActiveQueue(GameObject activatedObject){
      activatedObject.SetActive(false);
      activeObjectContainer.Enqueue(activatedObject);
      objectCounter.text = "" + activeObjectContainer.Count;
      return activatedObject;
    }
}
