using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCreater : MonoBehaviour
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
        //minionPosArr[0] = new Vector3(-1.71f, 2.11f);
        //minionPosArr[1] = new Vector3(-1.8f, 1.01f);
        //minionPosArr[2] = new Vector3(-2.59f, 1.34f);
        //minionPosArr[3] = new Vector3(-3.2f, 1.97f);
        //minionPosArr[4] = new Vector3(-2.71f, 0.3f);
        //minionPosArr[5] = new Vector3(-3.28f, 1.1f);
        //minionPosArr[6] = new Vector3(-2.58f, 2.38f);
        //minionPosArr[7] = new Vector3(-2.2f, 0.16f);
        //minionPosArr[8] = new Vector3(-3.79f, 1.55f);
        //minionPosArr[9] = new Vector3(-2.26f, 0.95f);
        //minionPosArr[10] = new Vector3(-2.28f, 1.94f);
        //minionPosArr[11] = new Vector3(-3.33f, 0.35f);
        //minionPosArr[12] = new Vector3(-3.78f, 2.45f);
        //minionPosArr[13] = new Vector3(-3.88f, 0.78f);
        //minionPosArr[14] = new Vector3(-3.17f, 2.81f);
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
}
