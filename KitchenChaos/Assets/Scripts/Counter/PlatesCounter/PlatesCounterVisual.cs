using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]private PlatesCounter platesCounter;
    [SerializeField]private GameObject platePrefab;
    [SerializeField]private Transform platesParent;
    private List<GameObject> plateVisualGameObjectList;
    void Awake()
    {
        plateVisualGameObjectList=new List<GameObject>();
    }
    void Start(){
        platesCounter.OnPlateSpawned +=PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved+=PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
      GameObject plateGameObject= plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
      plateVisualGameObjectList.Remove(plateGameObject);
      Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
       Transform plateVisualTransform= Instantiate(platePrefab,platesParent).transform;
        float plateoffsetY=.1f;
        plateVisualTransform.localPosition=new Vector3(0,plateoffsetY*plateVisualGameObjectList.Count,0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
