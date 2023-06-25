using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float maxDistance = 3f;
    [SerializeField] private float bulletSpeed = 5.0f;
    private Vector3 startPosition;
    Rigidbody2D rigid;
    public GameObject monster;


    private void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        monster = GameObject.Find("Boss");

        startPosition = transform.position;
        Vector3 monsterDir = monster.transform.position - transform.position;
        rigid.velocity = monsterDir.normalized * bulletSpeed;
    }



    //public void Fire(Vector2 direction)
    //{
    //    rigid.velocity = direction.normalized * bulletSpeed;
    //}

    private void Update()
    {
        float currentDistance = Vector3.Distance(startPosition, transform.position);

        if (currentDistance >= maxDistance)
        {
            // 총알을 오브젝트 풀로 반환?
            gameObject.SetActive(false);
        }
    }
}
