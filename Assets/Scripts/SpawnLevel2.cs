using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel2 : MonoBehaviour
{
    [SerializeField]
    int[] dimentions;
    int[] superDimentions;// needs a rename
    Node[] tiles;
    triCounter counter;

    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject prefap;

    [SerializeField]
    int bombs;

    int dLength;//dimentions.Length
    int tLength;//tiles.Length

    // Use this for initialization
    void Start()
    {
        dLength = dimentions.Length;
        counter = new triCounter(dLength);
        superDimentions = new int[dLength];
        for (int i = 0; i < dLength; i++)
        {
            superDimentions[i] = 1;
            for (int j = 0; j < i; j++)
            {
                superDimentions[i] *= dimentions[j];
            }
            Debug.Log(superDimentions[i]);
        }
        int temp = 1;
        for (int i = 0; i < dLength; i++)
        {
            temp *= dimentions[i];
        }
        tiles = new Node[temp];
        tLength = tiles.Length;
        Spawn();
        for (int i = 0; i < tLength; i++)
        {
            FindNeighbours(i);
        }
        addBombs(bombs);
        for (int i = 0; i < tLength; i++)
        {
            tiles[i].SearchBombs();
        }
    }

    int MakeX(int self)
    {
        int x = 0;
        for (int i = 0; i < dLength; i++)
        {
            if (i % 2 == 0)
            {
                int dim1 = 1;
                for (int j = 0; j < i - 1; j++)
                {
                    if (j % 2 == 0)
                    {
                        dim1 *= dimentions[j];
                        dim1 += 1;
                    }
                }

                x += numberToArray(self)[i] * dim1;
            }
        }
        return x;
    }

    int MakeY(int self)
    {
        int y = 0;
        for (int i = 0; i < dLength; i++)
        {
            if (i % 2 == 1)
            {
                int dim1 = 1;
                for (int j = 0; j < i - 1; j++)
                {
                    if (j % 2 == 1)
                    {
                        dim1 *= dimentions[j];
                        dim1 += 1;
                    }
                }

                y += numberToArray(self)[i] * dim1;
            }
        }
        return y;
    }

    void Spawn()
    {
        for (int i = 0; i < tLength; i++)
        {
            tiles[i] = Instantiate(prefap).GetComponent<Node>();
            tiles[i].gameObject.transform.position = new Vector3(MakeX(i), MakeY(i), 0);
            tiles[i].name = i + "";
            tiles[i].gameMaster = gameObject;
        }
        Camera.main.transform.position = (tiles[tLength - 1].transform.position - tiles[0].transform.position) / 2 + tiles[0].transform.position + new Vector3(0, 0, -90);
        Camera.main.orthographicSize = Mathf.Max(Camera.main.transform.position.x / 1.6f, Camera.main.transform.position.y / 0.9f) + 5;
    }

    public int[] numberToArray(int self)
    {
        int[] coords = new int[dLength];
        for (int i = 0; i < dLength; i++)
        {
            int dim1 = dimentions[i];
            int dim2 = 1;
            for (int j = 0; j < i; j++)
            {
                dim1 *= dimentions[j];
                dim2 *= dimentions[j];
            }
            coords[i] = (int)Mathf.Floor(self % dim1 / dim2);

        }
        return coords;
    }

    public int arrayToNumber(int[] coords)
    {
        int result = coords[0];
        for (int i = 0; i < dLength; i++)
        {
            result += coords[i + 1] * dimentions[i];
        }
        return result;
    }

    private void FindNeighbours(int num)
    {
        for(int i = 0; i < Mathf.Pow(3, dLength); i++)
        {
            int result = num;
            int[] coords = numberToArray(num);
            bool addNeighbour = true;
            for (int j = 0; j < dLength; j++)
            {
                if(coords[j] + counter.counter[j] < 0 || coords[j] + counter.counter[j] >= dimentions[j])
                {
                    addNeighbour = false;
                    break;
                }
                result += counter.counter[j] * superDimentions[j];
            }
            if(addNeighbour && num != result && result >= 0 && result < tLength)
            {
                tiles[num].neighbours.Add(tiles[result]);
            }
            counter.Up();


        }
        counter.Reset();

        //int[] self = numberToArray(num);

        //for (int j = 0; j < tLength; j++)
        //{
        //    bool tmp = true;
        //    int[] arrayTmp = numberToArray(j);
        //    for (int k = 0; k < arrayTmp.Length; k++)
        //    {
        //        int temp = arrayTmp[k] - self[k];
        //        if (!(temp <= 1 && temp >= -1))
        //        {
        //            tmp = false;
        //            break;
        //        }
        //    }
        //    if (tmp && num != j)
        //    {
        //        tiles[num].neighbours.Add(tiles[j]);
        //    }
        //}
    }

    public void addBombs(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int location = Random.Range(0, tLength - 1);
            if (!tiles[location].isBomb)
            {
                tiles[location].isBomb = true;
            }
            else
            {
                i--;
            }
        }
    }
}

public class triCounter
{
    public int[] counter;
    int length;

    public triCounter(int size)
    {
        length = size;
        counter = new int[length];
        for (int i = 0; i < length; i++)
        {
            counter[i] = -1;
        }
    }

    public int[] Up()
    {
        counter[0]++;
        for (int i = 0; i < length; i++)
        {
            if(counter[i] > 1)
            {
                if(i < length-1)
                {
                    counter[i + 1]++;
                }
                counter[i] = -1;
            }
        }
        return counter;
    }

    public void Reset()
    {
        for (int i = 0; i < length; i++)
        {
            counter[i] = -1;
        }
    }

    //public void Down()
    //{
    //    counter[0]--;
    //    for (int i = 0; i < length; i++)
    //    {
    //        if (counter[i] < -1)
    //        {
    //            counter[i + 1]--;
    //            counter[i] = 1;
    //        }
    //    }
    //}
}