using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
    public bool isBomb;
    bool isVisible;
    bool isFlagged;
    int neighbourbombs;
    Material mat;
    [SerializeField]
    TextMesh text;
    public List<Node> neighbours;
    
    public GameObject gameMaster;
    
    public void LeftClick()
    {
        if(isBomb && !isFlagged)
        {
            isVisible = true;
            text.text = "B";
            mat.color = Color.red;
            //SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        else if (!isVisible && !isFlagged)
        {
            isVisible = true;
            text.text = neighbourbombs + "";
            mat.color = Color.white;
            if (neighbourbombs == 0)
            {
                foreach (Node guy in neighbours)
                {
                    //Debug.Log("click");
                    guy.LeftClick();
                    //guy.RightClick();
                }
            }
        }
    }

    public void RightClick()
    {
        if (isFlagged && !isVisible)
        {
            text.text = "";
            isFlagged = false;
            mat.color = Color.grey;
        }
        else if(!isVisible)
        {
            text.text = "F";
            isFlagged = true;
            mat.color = Color.yellow;
        }

    }
    
    public Node()
    {
        isBomb = false;
        isVisible = false;
        isFlagged = false;
        neighbourbombs = 0;
        neighbours = new List<Node>();
    }

    void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        mat.color = Color.grey;
    }

    public void SearchBombs()
    {
        neighbourbombs = 0;
        foreach (Node guy in neighbours)
        {
            if(guy.isBomb)
            {
                neighbourbombs++;
            }
        }
    }
}
