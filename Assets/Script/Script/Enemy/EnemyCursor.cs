using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCursor : CursorObject
{
    private void Awake()
    {
        SetCursorType = ECursorType.Attack;
    }
}
