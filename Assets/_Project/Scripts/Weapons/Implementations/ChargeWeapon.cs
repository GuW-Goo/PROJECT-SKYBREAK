using UnityEngine;

public class ChargeWeapon : BaseWeapon
{
    private ChargeVisualController chargeVisual;

    private float chargeStartTime;
    private float chargeTime;

    private bool isCharging;
    private bool hasReachedMaxCharge;

    private int finalDamage;
    private float finalProjectileScale;
    private float currentMaxChargeTime;

    private float MaxChargeTime => Mathf.Max(0.05f, runtimeStats.ChargeSpeed);

    private void Awake()
    {
        chargeVisual = GetComponent<ChargeVisualController>();
    }

    public override void OnFirePressed()
    {
        if (!IsFireReady || isCharging)
            return;

        isCharging = true;
        hasReachedMaxCharge = false;

        currentMaxChargeTime = MaxChargeTime;

        chargeStartTime = Time.time;
        chargeTime = 0.0f;

        chargeVisual.ShowCharge();
    }

    public override void OnFireHeld()
    {
        if (!isCharging)
            return;


        chargeTime = Mathf.Min(
            Time.time - chargeStartTime,
            currentMaxChargeTime
        );

        // 시각 효과 크기 갱신
        float visualRatio = Mathf.InverseLerp(
            0.0f,
            currentMaxChargeTime,
            chargeTime
        );

        chargeVisual.UpdateChargeSize(visualRatio);

        // 최대 충전 도달
        if(chargeTime >= currentMaxChargeTime && !hasReachedMaxCharge)
        {
            hasReachedMaxCharge = true;
            chargeVisual.SetFullyCharged(true);
        }
    }

    public override void OnFireReleased()
    {
        if (!isCharging) 
            return;

        isCharging = false;

        // 최대 충전 여부 확인
        chargeTime = Time.time - chargeStartTime;
        hasReachedMaxCharge = chargeTime >= currentMaxChargeTime;

        if (hasReachedMaxCharge)
        {
            finalDamage = weaponData.MaxChargeAttackPower + runtimeStats.AttackPower;
            finalProjectileScale = weaponData.MaxProjectileScale;
        }
        else
        {
            finalDamage = weaponData.MinChargeAttackPower;
            finalProjectileScale = weaponData.MinProjectileScale;
        }

        TryFire();
        chargeVisual.HideCharge();
    }

    public override void OnUnequip()
    {
        CancelCharge();
    }

    protected override void Fire()
    {
        SpawnChargeProjectile(
            firePoint.position,
            firePoint.forward,
            finalDamage,
            finalProjectileScale,
            runtimeStats.ExplodeRange,
            hasReachedMaxCharge
        );
    }

    private void CancelCharge()
    {
        isCharging = false;
        hasReachedMaxCharge = false;

        chargeTime = 0f;
        chargeStartTime = 0f;

        chargeVisual.SetFullyCharged( false );
        chargeVisual.HideCharge();
    }
}
