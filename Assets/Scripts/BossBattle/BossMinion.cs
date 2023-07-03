using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinion : MonoBehaviour
{
    public GameObject minionPrefab;
    private ObjectPool minionPool;
    private Vector3[] minionPosArr = new Vector3[10];
    public GameObject boss;
    private int randomIndex;
    void Start()
    {
        minionPool = new ObjectPool(minionPrefab, 10, "MinionPool");
        minionPosArr[0] = new Vector3(1f, 1f);
        minionPosArr[1] = new Vector3(3f, 1f);
        minionPosArr[2] = new Vector3(1f, 3f);
        minionPosArr[3] = new Vector3(5f, 5f);
        minionPosArr[4] = new Vector3(6f, 6f);
        minionPosArr[5] = new Vector3(7f, 6f);
        minionPosArr[6] = new Vector3(6f, 7f);
        minionPosArr[7] = new Vector3(8f, 8f);
        minionPosArr[8] = new Vector3(9f, 9f);
        minionPosArr[9] = new Vector3(10f, 10f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateMinion();
        }
    }

    private void CreateMinion()
    {
        randomIndex = Random.Range(5, 10);
        for(int i = 0; i < randomIndex; i++)
        {
            GameObject minionObj = minionPool.GetObject();

            minionObj.transform.position = boss.transform.position + minionPosArr[i];
        }
    }
}
