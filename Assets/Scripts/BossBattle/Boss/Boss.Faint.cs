using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : MonoBehaviour
{
    [SerializeField] SpriteRenderer bossSpriteRenderer;
    private Color targetColor = Color.green;
    private Color originalColor;

    public float faintTime;
    public void BossFaint()
    {
        bossSpriteRenderer.color = targetColor;
    }

    public void BossFaintEnd()
    {
        bossSpriteRenderer.color = originalColor;
    }
}
