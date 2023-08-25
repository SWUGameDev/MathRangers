using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleCameraController : MonoBehaviour
{
    [SerializeField] GameObject curtainCamera;
    [SerializeField] GameObject centerCamera;

    private void Awake()
    {
        CameraChange(true);
    }
    private void Start()
    {
        this.StartCoroutine(this.MovetoCenterCamera());
    }

    private IEnumerator MovetoCenterCamera()
    {
        yield return new WaitForSeconds(0.3f);
        CameraChange(false);
    }

    private void CameraChange(bool isActive)
    {
        curtainCamera.SetActive(isActive);
        centerCamera.SetActive(!isActive);
    }
}
