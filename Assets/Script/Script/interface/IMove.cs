using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    /// <summary>
    /// ÒÆ¶¯½Ó¿Ú
    /// </summary>
    /// <param name="velocityVector"></param>
    public abstract void SetMove(Vector3 velocityVector);
}
