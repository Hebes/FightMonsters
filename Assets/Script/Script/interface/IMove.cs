using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    /// <summary>
    /// �ƶ��ӿ�
    /// </summary>
    /// <param name="velocityVector"></param>
    public virtual void SetMove(Vector3 vector3) { }

    public virtual void SetMove(Vector2 vector2) { }
}
