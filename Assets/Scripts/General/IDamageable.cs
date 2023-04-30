using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Interface for anything that can be hurt. */
public interface IDamageable
{
    public void TakeDamage(float damage);
}
