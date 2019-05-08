using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.

    Vector3 offset;                     // The initial offset from the target.

    private SetTransparency setTrans;

    void Start()
    {
        StartCoroutine(DetectPlayerObstructions());
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    IEnumerator DetectPlayerObstructions()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Vector3 direction = (target.position - Camera.main.transform.position).normalized;
            RaycastHit rayCastHit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out rayCastHit, Mathf.Infinity))
            {
                SetTransparency objT = rayCastHit.collider.gameObject.GetComponent<SetTransparency>();
                
                if (objT)
                {
                    objT.SetTransparent();
                    setTrans = objT;
                }
                else
                {
                    if (setTrans)
                    {
                        setTrans.SetToNormal();
                        setTrans = null;
                    }
                }
            }

        }
    }

    public void StartRayCast()
    {
        StopCoroutine("DetectPlayerObstructions");
        StartCoroutine(DetectPlayerObstructions());
    }

    public void StopRayCast()
    {
        StopCoroutine("DetectPlayerObstructions");
    }
}
