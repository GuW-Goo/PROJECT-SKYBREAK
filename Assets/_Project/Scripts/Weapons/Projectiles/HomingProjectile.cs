using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class HomingProjectile :BaseProjectile
{
    [Header("Homing Setting")]
    [SerializeField] private float detectionAngle = 90f;   // 감지 각도
    [SerializeField] private float farTurnRate = 360f;     // 멀 때 선회율 (deg/s)
    [SerializeField] private float nearTurnRate = 540f;    // 가까울 때 선회율 (deg/s)
    [SerializeField] private float nearDist = 15f;         // 이 거리 이하 = 최대 선회
    [SerializeField] private float farDist = 75f;          // 이 거리 이상 = 최소 선회

    private float detectionRange;
    private Transform target;

    

    public void Initialize(
        int damage,
        float moveSpeed,
        float lifeTime,
        GameObject explosionEffect,
        float detectionRange)
    {
        base.Initialize(damage, moveSpeed, lifeTime, explosionEffect);

        this.detectionRange = detectionRange;

    }

    private void OnEnable()
    {
        target = FindTarget();
    }

    protected override void Update()
    {
        if(target != null)
        {
            Vector3 currentDir = SteerToTarget(
                transform.position,
                transform.forward,
                target,
                moveSpeed
            );

            transform.rotation = Quaternion.LookRotation(currentDir);
        }

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 유도 탄환의 추적 대상을 탐색합니다.
    /// Enemy 태그와 IDamageable 인터페이스를 가진 적을 대상으로 하며,
    /// 전방 90도 부채꼴 범위 내의 가장 가까운 적을 우선 선택합니다.
    /// 해당 범위에 적이 없으면 탐색 반경 내에서 가장 가까운 적을 선택합니다.
    /// </summary>
    /// <returns>탐색된 적의 Transform. 적을 찾지 못하면 null을 반환합니다.</returns>
    private Transform FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(
            transform.position,
            detectionRange
        );

        Transform closestInCone = null;
        Transform closestInRange = null;

        float coneDistance = float.MaxValue;
        float rangeDistance = float.MaxValue;

        Vector3 forward = transform.forward;
        forward.y = 0f;

        foreach (Collider collider in colliders)
        {
            if (!collider.TryGetComponent<IDamageable>(out _))
                continue;

            Vector3 direction = collider.transform.position - transform.position;
            direction.y = 0f;

            float distance = direction.sqrMagnitude;

            // 원형 탐색: 가장 가까운 적 기록
            if (distance < rangeDistance)
            {
                rangeDistance = distance;
                closestInRange = collider.transform;
            }

            // 부채꼴 탐색: 정면 90도 안에 있는지 확인
            float angle = Vector3.Angle(forward, direction);

            if (angle <= detectionAngle / 2 && distance < coneDistance)
            {
                coneDistance = distance;
                closestInCone = collider.transform;
            }
        }

        // 부채꼴 탐색 성공 시 우선 반환, 실패하면 원형 탐색 결과 반환
        return closestInCone != null ? closestInCone : closestInRange;
    }

    /// <summary>
    /// 매 프레임 호출. 현재 진행 방향(currentDir)을 타겟 쪽으로 조금 돌려서 반환.
    /// 멀면 완만하게, 가까우면 급격하게 꺾임.
    /// </summary>
    /// <param name="currentPos">탄의 현재 위치</param>
    /// <param name="currentDir">탄의 현재 진행 방향 (XZ, 정규화)</param>
    /// <param name="target">추적 대상</param>
    /// <param name="speed">현재 탄속 (런타임 변경 가능)</param>
    /// <param name="dt">Time.deltaTime</param>
    /// <returns>이번 프레임에 적용할 새 진행 방향 (정규화)</returns>
    private Vector3 SteerToTarget(Vector3 currentPos, Vector3 currentDir, Transform target, float speed)        
    {
        currentDir.y = 0f;
        currentDir = currentDir.sqrMagnitude > 0.0001f ? currentDir.normalized : Vector3.forward;

        if (target == null) return currentDir;

        Vector3 toTarget = target.position - currentPos;
        toTarget.y = 0f;                       // 카메라 높이와 무관하게 XZ만 사용
        float dist = toTarget.magnitude;
        if (dist < 0.0001f) return currentDir;

        Vector3 targetDir = toTarget / dist;

        // 거리 → 선회율 (가까울수록 커짐)
        float t = Mathf.InverseLerp(nearDist, farDist, dist);   // 0=가까움, 1=멈
        float turnRate = Mathf.Lerp(nearTurnRate, farTurnRate, t * t);

        // 탄속이 빠를수록 같은 각속도로는 곡선 반경이 커지므로 속도에 비례해 보정
        // (기준 속도 100 기준으로 정규화)
        turnRate *= Mathf.Max(1f, speed / 100f);

        float maxDegrees = turnRate * Time.deltaTime;

        // XZ 평면 회전이므로 Y축 기준 SignedAngle 사용, 오버슛 방지
        float signedAngle = Vector3.SignedAngle(currentDir, targetDir, Vector3.up);
        float clamped = Mathf.Clamp(signedAngle, -maxDegrees, maxDegrees);

        return Quaternion.AngleAxis(clamped, Vector3.up) * currentDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            Instantiate(explosionEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            target.TakeDamage(damage);
            poolManager.ReturnProjectile(this);
            Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
        }
    }
}
