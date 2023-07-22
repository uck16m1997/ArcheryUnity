using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BowBaseState
{
    abstract public void HandleInput(BowController bowController);
    abstract public void OnEnter(BowController bowController);
}
