using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawn : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Start() 
    {
        StartCoroutine(MoveUp());
    }
    IEnumerator MoveUp()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + _speed * Time.deltaTime, transform.position.z);

            yield return null;
        }
    }


}
