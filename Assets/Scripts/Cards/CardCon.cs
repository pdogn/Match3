using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCon : Card
{
    public int type;
    public Transform target;
    public int xIndex;

    //public ChekManager chekManager;
    public override void DoTapped()
    {
        Debug.Log("Tapped to : " + this.gameObject.name);

        if (ChekManager.Instance.listChekObj.Count < ChekManager.Instance.listChekPos.Count)
        {
            AddToListChekObj();
            MoveToTarget(.25f);
        }
    }

    public override void MoveToTarget(float arriveTime)
    {
        DOTween.Kill(transform);
        transform.DOKill();
        transform.DOMove(target.position, arriveTime).SetEase(Ease.Linear).OnComplete(() =>
        {

        });
    }


    public void AddToListChekObj()
    {
        xIndex = ChekManager.Instance.FindBottomOfList();
        target = ChekManager.Instance.listChekPos[xIndex];
        ChekManager.Instance.listChekObj.Add(this.gameObject.transform);
    }
}
