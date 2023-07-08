using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Boss : MonoBehaviour
{
    private BossStateMachine bossStateMachine;

    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] public Transform spawnPoint;

    [SerializeField] private MinionCreator minionCreator;
    private Vector2 direction = Vector2.left;

    public static UnityEvent OnPlayerAttacked;
    private void Awake() {

        Boss.OnPlayerAttacked = new UnityEvent();

        this.bossStateMachine = this.gameObject.AddComponent<BossStateMachine>();

        //TODO : 이후에 위치 변경하기
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_BOSS_BATTLE);
    }
    void Start()
    {
        this.bossStateMachine.Initialize("Idle",this);
    }

    void Update()
    {
        Debug.Log(this.bossStateMachine.currentState);

        this.TurnToTarget();
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Boss.OnPlayerAttacked?.Invoke();
    }
}
