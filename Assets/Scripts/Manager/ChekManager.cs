using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekManager : MonoBehaviour
{
    [SerializeField] Transform Chek;
    [SerializeField] List<Transform> listChek;
    [SerializeField] List<Transform> listChekAvaiable;
    // Start is called before the first frame update
    void Start()
    {
        AutoAddListChek();
        listChekAvaiable = listChek;
    }

    

    void AutoAddListChek()
    {
        foreach (Transform child in Chek)
        {
            if (!listChek.Contains(child))
            {
                listChek.Add(child);
            }
        }
    }
}
