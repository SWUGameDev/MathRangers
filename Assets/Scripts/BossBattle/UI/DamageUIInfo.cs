using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUIInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text damageText;

    public void InitializeDamageUIInfo(Vector3 position, int damage)
    {
        this.SetPosition(position);
        this.SetDamage(damage);
    }

    private void SetDamage(int damage)
    {
        this.damageText.text = damage.ToString();
    }

    private void SetPosition(Vector3 position)
    {
        this.transform.localPosition = position;
    }
}
