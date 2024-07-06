using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public interface IDamageable
{
    public void Damage(Transform entity, int damage, Vector2 force);
}
