using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateUpdater : MonoBehaviour
{
    public void StateActive()
    {
        Invoke("StateActiveSet", 2f);
    }
    public void StateIdle()
    {
        CombatStateManager.current.SetState(CombatStateManager.SceneState.Friendly);
    }

    private void StateActiveSet()
    {
        CombatStateManager.current.SetState(CombatStateManager.SceneState.Hostile);
    }
}
