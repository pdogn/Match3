using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekManager : Singleton<ChekManager>
{
    [SerializeField] Transform Chek;
    public List<Transform> listChekPos;
    public List<Transform> listChekObj;

    public bool deleted;
    //public List<Transform> listChekAvaiable;
    // Start is called before the first frame update
    void Start()
    {
        deleted = false;
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

    //vị trí cuối của khay
    public int FindBottomOfList()
    {
        int countCardUsed = listChekObj.Count;

        return countCardUsed;
    }

    //Tìm vị trí để chèn card vào khay
    public int FindPos(int _type)
    {
        CardCon objSameType = new CardCon();
        for (int i = listChekObj.Count - 1; i >= 0; i--)
        {
            if (listChekObj[i].GetComponent<CardCon>().type == _type)
            {
                objSameType = listChekObj[i].GetComponent<CardCon>();
                break;
            }
        }

        if (!objSameType)
        {
            return FindBottomOfList();
        }
        else
        {
            int xIndx = objSameType.xIndex + 1;
            SortChekRay(xIndx -1);
            return xIndx;
        }
    }

    //sắp xếp lại khay khi chèn thêm card
    void SortChekRay(int _xIndx)
    {
        foreach(var child in listChekObj)
        {
            if(child.GetComponent<CardCon>().xIndex > _xIndx)
            {
                child.GetComponent<CardCon>().xIndex += 1;
                child.GetComponent<CardCon>().target = listChekPos[child.GetComponent<CardCon>().xIndex];
                child.GetComponent<CardCon>().MoveToTarget(.25f);
            }
        }
    }

    public void DeleteWhenMatch3(int _type)
    {
        int _count = 0;
        foreach(var child in listChekObj)
        {
            if(child.GetComponent<CardCon>().type == _type)
            {
                _count++;
            }
        }
        if(_count == 3)
        {
            deleted = true;
            int ind;
            //destroy 3 card giống nhau trong khay
            List<Transform> deleteCards = new List<Transform>();
            for(int i= listChekObj.Count -1; i>=0 ; i--)
            {
                if (listChekObj[i].GetComponent<CardCon>().type == _type)
                {
                    deleteCards.Add(listChekObj[i]);
                    listChekObj.Remove(listChekObj[i]);
                    //Destroy(listChekObj[i].gameObject);
                }
            }

            ind = deleteCards[0].GetComponent<CardCon>().xIndex;
            Debug.Log(ind);

            SortRayAfterDelete(ind);

            for (int j = deleteCards.Count - 1; j >= 0; j--)
            {
                Destroy(deleteCards[j].gameObject);
            }
            //SortRayAfterDelete(ind);
        }
    }

    //xếp lại khay sau khi xóa 3 card
    public void SortRayAfterDelete(int ind)
    {
        if (deleted == false) return;

        deleted = false;
        foreach(var child in listChekObj)
        {
            if(child.GetComponent<CardCon>().xIndex > ind)
            {
                child.GetComponent<CardCon>().xIndex -= 3;
                child.GetComponent<CardCon>().target = listChekPos[child.GetComponent<CardCon>().xIndex];
                child.GetComponent<CardCon>().MoveToTarget(.25f);
            }
        }
    }
}
