using UnityEngine;

public abstract class Attack_Pattern : Spider_Attack_Controller
{
    protected Spider_Attack_Controller controller;
    public abstract void Attack();

    public void SetController(Spider_Attack_Controller value)
    {
        controller = value;
    }
}
