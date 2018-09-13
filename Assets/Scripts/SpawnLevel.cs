using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [SerializeField]
    int[] dimentions;
    GameObject[] tiles;

    void Start()
    {
        int temp = 0;
        for (int i = 0; i < dimentions.Length; i++)
        {
            temp += dimentions[i];
        }
        tiles = new GameObject[temp];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.AddComponent<Rigidbody>();
            for (int j = 0; j < dimentions.Length; j++)
            {
                if(j % j == 0)
                {

                }
                else
                {

                }
            }
            tiles[i].transform.position = new Vector3(i % dimentions[0], Mathf.Floor(i / dimentions[1]), 0);
        }
        for(int i = 0; i < tiles.Length; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                
            }
        }
    }
}
