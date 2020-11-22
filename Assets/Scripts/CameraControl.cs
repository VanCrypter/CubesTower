using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [SerializeField]
    private float speed;  

    Coroutine coroutine;

    private void Update()
    {        
            MoveUp();
    }

    private void MoveUp()
    {

        transform.position = new Vector3(transform.position.x,	
					Mathf.Lerp(transform.position.y, Tower.highestColumn+2f, Time.deltaTime) ,
					transform.position.z);
    }

    public void RotateCamera(float deg) 
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Rotate(deg, 8f));
        }    
    }

  
    IEnumerator Rotate(float angle, float intensity)
    {
        var cam = transform;
        var targetRotation = cam.rotation * Quaternion.Euler(0f,angle / 10f, 0.0f);

        while (true)
        {
            cam.rotation = Quaternion.Lerp(cam.rotation, targetRotation, intensity * Time.deltaTime);

            if (Quaternion.Angle(cam.rotation, targetRotation) < 1f)
            {
                coroutine = null;
                cam.rotation = targetRotation;
                yield break;
            }

            yield return null;
        }
    }

}
