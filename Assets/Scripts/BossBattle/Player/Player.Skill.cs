using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    private Color changeColor;
    [SerializeField] SpriteRenderer[] playerSpriteRenderer;
    private bool isTransparent = false;
    private bool isUnbeat = false;

    private float transparentAlpha;
    private float transparent = 0.5f;
    private float normal = 1.0f;

    private int transparentIdx;

    public void Buff101(int buttonIdx)
    {
        Debug.Log("스킬 101");
        transparentIdx = (int)playerProperty.Buff101UnbeatTime * 2;
        this.StartCoroutine(TransparentCycle());
        bossSceneUIManager.SkillButton[buttonIdx].interactable = false;
    }

    public void Buff102(int buttonIdx) 
    {
        Debug.Log("스킬 102");
        playerProperty.Hp += playerProperty.EnergyHp;
        playerUIController.ChangePlayerHpValue();
        bossSceneUIManager.SkillButton[buttonIdx].interactable = false;
    }

    public void Buff103(int buttonIdx) 
    {
        Debug.Log("스킬 103");
        bossSceneUIManager.SkillButton[buttonIdx].interactable = false;
        playerProperty.MinAttackPower += playerProperty.Buff103MinAttackPower;
        playerProperty.MaxAttackPower += playerProperty.Buff103MaxAttackPower;
        playerProperty.AttackSpeed += playerProperty.Buff103AttackSpeed;

        this.StartCoroutine(Waitfor103(playerProperty.Buff103Time));
 
        playerProperty.MinAttackPower -= playerProperty.Buff103MinAttackPower;
        playerProperty.MaxAttackPower -= playerProperty.Buff103MaxAttackPower;
        playerProperty.AttackSpeed -= playerProperty.Buff103AttackSpeed;
    }

    private IEnumerator Waitfor103(int time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator TransparentCycle()
    {
        isUnbeat = true;
        for (int i = 0; i < transparentIdx; i++)
        {
            isTransparent = !isTransparent;
            ChangeTransparent();
            yield return new WaitForSeconds(0.5f);
        }
        isUnbeat = false;
    }

    private void ChangeTransparent()
    {
        transparentAlpha = isTransparent ? transparent : normal;
        for (int i = 0; i < playerSpriteRenderer.Length; i++)
        {
            changeColor = playerSpriteRenderer[i].color;
            changeColor.a = transparentAlpha;
            playerSpriteRenderer[i].color = changeColor;
        }
    }
}
