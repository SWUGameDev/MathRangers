using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject obj;

    private Transform parent;

    private Queue<GameObject> objectQueue;

/**
ObjectPool 생성자 함수 입니다.
- obj : 오브젝트 풀로 관리할 prefab
- count : 초기 오브젝트 풀 내 게임오브젝트 개수
- ObjectPoolName : 오브젝트 풀을 담을 부모 게임오브젝트의 이름입니다.
*/
    public ObjectPool(GameObject obj,int count,string ObjectPoolName = "Default")
    {
        GameObject parent = new GameObject($"{ObjectPoolName}_ObjectPool");
        this.InitializeObjectPool(obj,count,parent.transform);
    }

    private void InitializeObjectPool(GameObject obj,int count,Transform parent)
    {
        this.objectQueue = new Queue<GameObject>();
        this.obj = obj;
        this.parent = parent;
        for(int index = 0;index<count;++index)
            this.objectQueue.Enqueue(this.CreateObject());
    }

    private GameObject CreateObject()
    {
        var newObj = GameObject.Instantiate(this.obj);
        newObj.transform.SetParent(this.parent);
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public GameObject GetObject()
    {
        if(this.objectQueue.Count<=0)
        {
            var newObj = GameObject.Instantiate(this.obj);
            return newObj;
        }else{
            var obj = this.objectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.parent);
        this.objectQueue.Enqueue(obj);
    }

}
