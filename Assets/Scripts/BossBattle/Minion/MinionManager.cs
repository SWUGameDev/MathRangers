using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    public GameObject minionPrefab;
    private ObjectPool minionPool;
    //private Vector3[] minionPosArr = new Vector3[15];
    public Transform[] minionTransformArr = new Transform[15];
    public GameObject boss;
    private int randomIndex;
    void Start()
    {
        minionPool = new ObjectPool(minionPrefab, 15, "MinionPool");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateMinion();
        }
    }

    public void CreateMinion()
    {
        randomIndex = Random.Range(5, 15);
        for(int i = 0; i < randomIndex; i++)
        {
            GameObject minionObj = minionPool.GetObject();

            minionObj.transform.position = boss.transform.position + minionTransformArr[i].position;
        }
    }

    public ObjectPool GetMinionPool() 
    {
        return this.minionPool;
    }
}
