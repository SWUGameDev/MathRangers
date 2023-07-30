using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionCreator : MonoBehaviour
{
    public GameObject minionPrefab;
    private ObjectPool minionPool;
    public Transform[] minionTransformArr = new Transform[15];
    public GameObject boss;
    void Start()
    {
        minionPool = new ObjectPool(minionPrefab, 15, "MinionPool");
    }

    public void CreateMinion()
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_SWING);

        int randomIndex = Random.Range(5, 15);

        for (int i = 0; i < randomIndex; i++)
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