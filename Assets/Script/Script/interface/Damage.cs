using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamage
{
    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="obj"></param>
    public virtual void TakeDamage() { }
    public virtual void TakeDamage(float obj) { }
    public virtual void TakeDamage(object obj) { }
    /// <summary>
    /// 死亡
    /// </summary>
    public virtual void Kill() { }
}
