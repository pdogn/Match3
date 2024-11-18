using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public abstract void DoTapped();

    public abstract void MoveToTarget();
}
