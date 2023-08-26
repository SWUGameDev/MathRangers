using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RunPlayer : MonoBehaviour
{
    bool isFall = false;

    void CheckFallDown()
    {
        if (this.gameObject.transform.position.y <= runSceneUIManager.MinY && isFall == false)
        {
            this.StartCoroutine(this.LiftUpPlayer());
        }
    }

    IEnumerator LiftUpPlayer()
    {
        isFall = true;
        runSceneUIManager.SetAllScroll(false);
        runSceneUIManager.SetAllReverse(true);

        TakeDamageplayer(fallDownDamage);
        yield return new WaitForSeconds(1.0f);

        Vector3 liftUpPosition = playerTransform.position + Vector3.up * 12.0f;
        playerTransform.position = liftUpPosition;

        runSceneUIManager.SetAllReverse(false);
        yield return new WaitForSeconds(1.0f);
        runSceneUIManager.SetAllScroll(true);

        isFall = false;
    }
}
