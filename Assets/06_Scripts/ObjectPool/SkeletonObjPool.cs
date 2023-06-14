using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonObjPool : MonoBehaviour
{
    public static SkeletonObjPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Skeleton> poolingObjectQueue = new Queue<Skeleton>();

    private static int _objNo = 1;

    private void Awake()
    {
        Instance = this;

        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Skeleton CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Skeleton>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Skeleton GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.name = "Skeleton_" + _objNo;
            _objNo += 1;
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObject(Skeleton obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

