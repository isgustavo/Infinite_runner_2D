using ODT.Scriptable;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.IR
{
    public class LayerScrollBehaviour : MonoBehaviour
    {
        [SerializeField]
        private BoolVariable isPause;

        [Header ("Layer infos")]
        [SerializeField]
        private Vector2 startPointPosition;
        [SerializeField]
        private Vector2 endPointPosition;
        [SerializeField]
        private int layerScrollIndex;
        [SerializeField]
        private int layerScrollGapSize;
        [SerializeField]
        private int layerScrollSpeed;

        [Header ("Color")]
        [SerializeField]
        private bool hasColor;
        [SerializeField]
        private Color color;

        [Header ("Objects")]
        [SerializeField]
        private GameObject[] objectPrefabs;

        private List<ObjectScrollBehaviour> objectPool = new List<ObjectScrollBehaviour>();
        private ObjectScrollBehaviour lastObject;

        private void OnEnable()
        { 
            InitObjectPool();
            lastObject = GetRandomObject();
            lastObject.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (isPause.Value)
            {
                return;
            }

            if (lastObject.transform.position.x + lastObject.GetWidth() + layerScrollGapSize <= startPointPosition.x)
            {
                lastObject = GetRandomObject();
                lastObject.transform.position = startPointPosition;
                lastObject.gameObject.SetActive(true);
            }
        }

        private void InitObjectPool()
        {
            for (int i = 0; i < objectPrefabs.Length; i++)
            {
                GameObject newObj = Instantiate(objectPrefabs[i], transform);
                newObj.SetActive(false);
                ObjectScrollBehaviour obj = newObj.GetComponent<ObjectScrollBehaviour>();
                obj.Init(layerScrollIndex, color, startPointPosition, endPointPosition, layerScrollSpeed);
                objectPool.Add(obj);
            }
        }

        private ObjectScrollBehaviour GetRandomObject()
        {
            if (objectPool.Count <= 0)
            {
                return null;
            }

            int randomIndex = UnityEngine.Random.Range(0, objectPool.Count);

            if (objectPool[randomIndex].gameObject.activeInHierarchy)
            {
                return GetRandomObject();
            }
            else
            {
                return objectPool[randomIndex];
            }
        }
    }
}

/*
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
    private ScrollingObjectBehaviour firstObjBehaviour;
    private ScrollingObjectBehaviour currentObject;
    private ScrollingObjectBehaviour nextObject;

    private void Awake()
    {
        InitPool();
    }

    private void OnEnable()
    {
        //InitFirstObject();

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

    private void InitFirstObject()
    {
        if (firstObjectPrefab != null)
        {
            GameObject firstObj = Instantiate(firstObjectPrefab, transform);
            firstObjBehaviour = firstObj.GetComponent<ScrollingObjectBehaviour>();
            if (firstObjBehaviour != null)
            {
                firstObj.transform.position = new Vector2(firstObjectStartXPosition + (firstObjBehaviour.ObjectWidth * .5f), objectYPosition);
                firstObjBehaviour.Set(scrollingLayer, scrollingSpeed, hasColor, color);
                currentObject = firstObjBehaviour;
            }
        }
    }

    private void ResetFirstObject ()
    {
        if (firstObjBehaviour != null)
        {
            firstObjBehaviour.transform.position = new Vector2(firstObjectStartXPosition + (firstObjBehaviour.ObjectWidth * .5f), objectYPosition);
            firstObjBehaviour.Set(scrollingLayer, scrollingSpeed, hasColor, color);
            firstObjBehaviour.gameObject.SetActive(true);
            currentObject = firstObjBehaviour;
        }
    }

    private void SetNewObject ()
    {
        float currentEndXPosition = currentObject.ObjectEndXPoint;
        currentObject = nextObject;
        currentObject.transform.position = new Vector3(currentEndXPosition + (nextObject.ObjectWidth * .5f) + nextObject.distanceXToOtherObject, objectYPosition, 0);
        currentObject.gameObject.SetActive(true);
    }


    public void ReloadToMenu()
    {
        ResetFirstObject();
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
*/
