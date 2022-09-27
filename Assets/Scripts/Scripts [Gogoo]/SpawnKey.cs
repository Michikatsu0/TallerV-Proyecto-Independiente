using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    [SerializeField] GameObject keyObject;

    [SerializeField] List<Transform> listTransform = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        InstantiateKey();
    }


    void InstantiateKey()
    {
        Instantiate(keyObject, GetRandomPosition().position, GetRandomPosition().rotation);
    }

    Transform GetRandomPosition()
    {
        int random = Random.Range(0, listTransform.Count);
        return listTransform[random];
    }


}
