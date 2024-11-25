using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCon : Card
{
    public int type;
    public Transform target;
    public int xIndex;

    public int id;
    public int oldX;
    public int oldY;
    public int level;

    public List<int> higherIds;
    public List<int> lowerIds;

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
            ChekManager.Instance.DeleteWhenMatch3(type);
        });
    }


    public void AddToListChekObj()
    {
        xIndex = ChekManager.Instance.FindPos(type);
        target = ChekManager.Instance.listChekPos[xIndex];
        ChekManager.Instance.listChekObj.Add(this.gameObject.transform);
    }

    public void Init(CardCon cc)
    {
        oldX = cc.oldX;
        oldY = cc.oldY;
        higherIds = cc.higherIds;
        lowerIds = cc.lowerIds;
    }

}
