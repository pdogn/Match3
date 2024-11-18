using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCon : Card
{
    public Transform target;
    public override void DoTapped()
    {
        Debug.Log("Tapped to : " + this.gameObject.name);
    }

    public override void MoveToTarget()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
