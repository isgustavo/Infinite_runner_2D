using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagerBehaviour : MonoBehaviour 
{
    [SerializeField]
    private BoolVariable isPause;
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    private Transform objectsStartPosition;
    [SerializeField]
    private float timeToFirstObject;
    [SerializeField]
    private float timeBetweenObject;
    [SerializeField]
    private int scrollingLayer;
    [SerializeField]
    private int scrollingSpeed;

    private GameObjectPool<ScrollingObjectBehaviour> objectPool = new GameObjectPool<ScrollingObjectBehaviour>();

    private void Awake()
    {
        InitPool();
    }

    public void Start()
    {
        InvokeRepeating("SpawnCoinWithDelay",timeToFirstObject, timeBetweenObject);
    }

    private void SpawnCoinWithDelay ()
    {
        if(!isPause.Value)
        {
            ScrollingObjectBehaviour scrollingObj = objectPool.GetRandomObj();
            if (scrollingObj != null)
            {
                scrollingObj.transform.position = objectsStartPosition.position;
                scrollingObj.gameObject.SetActive(true);
            }
        }
    }

    private void InitPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform);
            obj.SetActive(false);
            ScrollingObjectBehaviour scrollingObj = obj.GetComponent<ScrollingObjectBehaviour>();
            if (scrollingObj != null)
            {
                objectPool.AddObj(scrollingObj);
                scrollingObj.Set(scrollingLayer, scrollingSpeed);
                scrollingObj.OnDisableObjectAction += objectPool.Release;
            }
        }
    }

    public void OnPauseValueChangedEvent ()
    {
        if(isPause.Value)
        {
            StopAllCoroutines();
        }
    }

	
}
