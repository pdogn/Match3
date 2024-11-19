using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekManager : Singleton<ChekManager>
{
    [SerializeField] Transform Chek;
    public List<Transform> listChekPos;
    public List<Transform> listChekObj;
    //public List<Transform> listChekAvaiable;
    // Start is called before the first frame update
    void Start()
    {
        AutoAddListChek();
        //listChekAvaiable = listChekPos;
    }
    
    void AutoAddListChek()
    {
        foreach (Transform child in Chek)
        {
            if (!listChekPos.Contains(child))
            {
                listChekPos.Add(child);
            }
        }
    }

    public int FindBottomOfList()
    {
        int countCardUsed = listChekObj.Count;

        return countCardUsed;
    }

    //public int FindPos(int _type)
    //{
    //    if(listChekObj.Count < 2)
    //    {
    //        return FindBottomOfList();
    //    }
    //    if(listChekObj.Count > 1)
    //    {
    //        CardCon objSameType;
    //        for(int i = listChekObj.Count - 1; i >= 0; i--)
    //        {
    //            if (listChekObj[i].GetComponent<CardCon>().type == _type)
    //            {
    //                objSameType = listChekObj[i].GetComponent<CardCon>();
    //                break;
    //            }
    //        }

    //        objSameType.tar
    //    }
    //}
}
