public class SimpleLaser : BulletBase
{
    protected override bool DestroyAfterTouch => false;

    protected override bool CanDamagePlayer => false;
}
