using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectManagerBehaviour : MonoBehaviour
{

    [SerializeField]
    private BoolVariable isPause;
    [SerializeField]
    private GameObject firstObjectPrefab;
    [SerializeField]
    private float firstObjectStartXPosition;
    [SerializeField]
    private float objectYPosition;
    [SerializeField]
    private Transform objectsStartPosition;
    [SerializeField]
    private int scrollingLayer;
    [SerializeField]
    private int scrollingSpeed;
    [SerializeField]
    private bool hasColor;
    [SerializeField]
    private Color color;
    [SerializeField]
    private ScrollingObject[] objects;

    private GameObjectPool<ScrollingObjectBehaviour> objectPool = new GameObjectPool<ScrollingObjectBehaviour>();
    private ScrollingObjectBehaviour currentObject;
    private ScrollingObjectBehaviour nextObject;

    private void OnEnable()
    {
        InitPool();
        InitFirstObject();

        nextObject = objectPool.GetRandomObj();
    }

    private void Update()
    {
        if(!isPause.Value)
        {
            if (currentObject.ObjectEndXPoint <= objectsStartPosition.position.x)
            {
                SetNewObject();
                nextObject = objectPool.GetRandomObj();
            }
        }
    }

    private void InitPool ()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject obj = Instantiate(objects[i].Prefab, transform);
            obj.SetActive(false);
            ScrollingObjectBehaviour scrollingObj = obj.GetComponent<ScrollingObjectBehaviour>();
            if (scrollingObj != null)
            {
                objectPool.AddObj(scrollingObj);
                scrollingObj.Set(scrollingLayer, scrollingSpeed, hasColor, color, objects[i].DistanceToNextObject);
                scrollingObj.OnDisableObjectAction += objectPool.Release;
            }
        }
    }

    private void InitFirstObject ()
    {
        if (firstObjectPrefab != null)
        {
            GameObject firstObj = Instantiate(firstObjectPrefab, transform);
            ScrollingObjectBehaviour firstObjBehaviour = firstObj.GetComponent<ScrollingObjectBehaviour>();
            if (firstObjBehaviour != null)
            {
                firstObj.transform.position = new Vector2(firstObjectStartXPosition + (firstObjBehaviour.ObjectWidth * .5f), objectYPosition);
                firstObjBehaviour.Set(scrollingLayer, scrollingSpeed, hasColor, color);
                currentObject = firstObjBehaviour;
            }
        }
    }

    private void SetNewObject ()
    {
        float currentEndXPosition = currentObject.ObjectEndXPoint;
        currentObject = nextObject;
        currentObject.transform.position = new Vector3(currentEndXPosition + (nextObject.ObjectWidth * .5f) + nextObject.distanceXToOtherObject, objectYPosition, 0);
        currentObject.gameObject.SetActive(true);
    }
}

[Serializable]
public struct ScrollingObject
{
    [SerializeField]
    private GameObject prefab;
    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
    [SerializeField]
    private int distanceToNextObject;
    public int DistanceToNextObject
    {
        get
        {
            return distanceToNextObject;
        }
    }
}


public class GameObjectPool<T>
{
    private List<T> availableObjs = new List<T>();
    private List<T> inUseObjs = new List<T>();

    public void AddObj(T obj)
    {
        availableObjs.Add(obj);
    }

    public T GetRandomObj()
    {
        if (availableObjs.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableObjs.Count);
            T obj = availableObjs[randomIndex];
            availableObjs.RemoveAt(randomIndex);
            inUseObjs.Add(obj);
            return obj;
        }
        else
        {
            Debug.LogError("ERROR: Empty pool");
            return default(T);
        }
    }

    public void Release(T obj)
    {
        inUseObjs.Remove(obj);
        availableObjs.Add(obj);
    }
}

