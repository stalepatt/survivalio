public class Dash : Skill
{
    public override void Attack()
    {
        Fire();
    }

    protected override void Fire()
    {
        IMovable CharacterForDash = gameObject.GetComponent<IMovable>();
        CharacterForDash.MoveInSpeed(2);
    }
}