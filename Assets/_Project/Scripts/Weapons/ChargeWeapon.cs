using UnityEngine;
using UnityEngine.Rendering;

public class ChargeWeapon : BaseWeapon
{
    [Header("Charge Visual")]
    [SerializeField] private ChargeVisualController chargeVisual;

    private float chargeStartTime;
    private float chargeTime;

    private bool isCharging;
    private bool hasReachedMaxCharge;

    int finalDamage;
    float finalProjectileScale;

    public override void OnFirePressed()
    {
        if (!IsFireReady || isCharging)
            return;

        isCharging = true;
        hasReachedMaxCharge = false;

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
            weaponData.MaxChargeTime
        );

        // 시각 효과 크기 갱신
        float visualRatio = Mathf.InverseLerp(
            0.0f,
            weaponData.MaxChargeTime,
            chargeTime
        );

        chargeVisual.UpdateChargeSize(visualRatio);

        // 최대 충전 도달
        if(chargeTime >= weaponData.MaxChargeTime && !hasReachedMaxCharge)
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
        hasReachedMaxCharge = chargeTime >= weaponData.MaxChargeTime;

        if (hasReachedMaxCharge)
        {
            finalDamage = weaponData.MaxChargeAttackPower;
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

    protected override void Fire()
    {
        SpawnChargeProjectile(
            firePoint.position,
            firePoint.forward,
            finalDamage,
            finalProjectileScale,
            weaponData.GetSplashRadius(level),
            hasReachedMaxCharge
        );
    }
}
