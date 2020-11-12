using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]    [Header("Скорость спавна")]  private float tempSpawnCube;
    [SerializeField]    private GameObject[] Cubes;
    [SerializeField]    private Transform[] _spawnPoints;
    [SerializeField]    private Transform _towerTransform;

    private void Start()
    {
        FindSpawnPoints();
        StartCoroutine(Spawn());
        
    }

    private void FindSpawnPoints() 
    {
        _spawnPoints = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            _spawnPoints[i++] = t;
        }
    }


    IEnumerator Spawn() 
    {
        while (true)
        {

            Instantiate(Cubes[Random.Range(0, 4)], _spawnPoints[Random.Range(0, 8)].position, Quaternion.Euler(_towerTransform.localRotation.eulerAngles.x,
                _towerTransform.localRotation.eulerAngles.y,
                _towerTransform.localRotation.eulerAngles.z), _towerTransform);

            yield return new WaitForSeconds(10f / tempSpawnCube);
        }      

    }

}
