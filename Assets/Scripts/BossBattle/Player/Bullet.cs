using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float time = 0f;
    public float timeInterval = 1f;
    [SerializeField] private float bulletSpeed = 13.0f;
    private Vector3 startPosition;
    Rigidbody2D rigid;

    private Player player;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Shot()
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_PLAYER_ATTACK);

        startPosition = transform.position;

        Vector3 monsterDir = player.monster.transform.position - transform.position;

        monsterDir = monsterDir == Vector3.zero ? Vector3.up : monsterDir;
        rigid.velocity = monsterDir.normalized * bulletSpeed;
    }

    public void Initialized(Player player)
    {
        this.player = player;
    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(startPosition, transform.position);

        time += Time.deltaTime;
        if (time >= timeInterval)
        {
            this.player.GetBulletPool().ReturnObject(this.gameObject);
            time = 0f;
        }
    }
}
