using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectSpawner : MonoBehaviour
{
    public GameObjectPool poolRef;
    public SpawnVolume rectangleSize;
    public TMP_InputField inputField;
    public int initialSpawnValue;
    private GameObject spawnHolder;
    private float randx, randy, randz, xLimit, yLimit, zLimit;
    private int spawnInput = 1;

    void Start(){
      this.ShowGui("" + initialSpawnValue, "0123456789", 6); //sets the input field to only accept numbers and restricts the max input, if not restricted it can crash the program haha.
      spawnInput = int.Parse(inputField.text);
      xLimit = rectangleSize.x;
      yLimit = rectangleSize.y;
      zLimit = rectangleSize.z;
      for(int x = 0; x < spawnInput; x++){
        spawnObject();
      }
      spawnInput = 1;
    }
    public void SpawnObjects(){
      if(inputField.text != ""){
        spawnInput = int.Parse(inputField.text);
      }else{
        spawnInput = 1;
      }
      for(int x = 0; x < spawnInput; x++){
        spawnObject();
      }
      GameObject[] refreshArray = poolRef.activeObjectContainer.ToArray();
      for(int m = 0; m < refreshArray.Length; m++){
        refreshArray[m].GetComponent<FindNearestNeighbour>().RefreshNeighbours(); //updates the list of objects to find a neighbour with
      }
    }
    public void DespawnObjects(){
      if(inputField.text != ""){
        spawnInput = int.Parse(inputField.text);
      }
      for(int y = 0; y < spawnInput; y++){
        if(poolRef.activeObjectContainer.Count != 0){ //prevents despawning if no objects are active
          poolRef.Despawn();
        }
      }
      GameObject[] refreshArray = poolRef.activeObjectContainer.ToArray();
      for(int m = 0; m < refreshArray.Length; m++){
        refreshArray[m].GetComponent<FindNearestNeighbour>().RefreshNeighbours();
      }
    }
    private void spawnObject(){
      randx = Random.Range(0, xLimit);
      randy = Random.Range(0, yLimit);
      randz = Random.Range(0, zLimit);
      spawnHolder = poolRef.GetFromPool();
      spawnHolder.transform.position = new Vector3(randx,randy,randz);
      spawnHolder.SetActive(true);
    }
    private void ShowGui(string inputStart, string validInput, int characterLim){
      inputField.text = inputStart;
      inputField.characterLimit = characterLim;
      inputField.onValidateInput = (string text, int charIndex, char addedChar) =>{
        return ValidateChar(validInput, addedChar);
      };
    }
    private char ValidateChar(string validCharacters, char addedChar){ //checks if each character entered is valid according to validInput string
      if(validCharacters.IndexOf(addedChar) != -1){
        return addedChar;
      }else{
        return '\0';
      }
    }
}
