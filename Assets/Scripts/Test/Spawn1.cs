using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn1 : MonoBehaviour
{
    public float cardWidth;
    public float cardHeight;
    public int levelNum;//tong so lop
    public int levelCardNum;//so card trong 1 lop
    public int clearCardNum;//so card xoa
    public int cardTypeNum;//so type trong 1 man

    public int totalCard;

    private void Start()
    {
        
    }

    public void NN()
    {
        totalCard = levelNum * levelCardNum;
        int unit = clearCardNum * cardTypeNum;

    }
}
