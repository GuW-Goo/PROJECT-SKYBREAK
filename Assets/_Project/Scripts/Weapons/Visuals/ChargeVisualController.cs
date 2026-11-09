using UnityEngine;

public class ChargeVisualController : MonoBehaviour
{
    [Header("Charge Visual")]
    [SerializeField] private GameObject chargeVisualPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Scale")]
    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 1.5f;

    [Header("Blink")]
    [SerializeField] private float blinkSpeed = 8.0f;

    private GameObject chargeVisual;
    private Renderer[] visualRenderers;

    private bool isFullyCharged;

    // 차지 모델 생성
    public void ShowCharge()
    {
        if (chargeVisual != null)
            return;

        chargeVisual = Instantiate(chargeVisualPrefab, firePoint);

        visualRenderers = chargeVisual.GetComponentsInChildren<Renderer>();

        chargeVisual.transform.localPosition = Vector3.zero;
        chargeVisual.transform.localRotation = Quaternion.identity;
        chargeVisual.transform.localScale = Vector3.one * minScale;

        isFullyCharged = false;
    }

    // 충전량에 따른 크기 변경
    public void UpdateChargeSize(float chargeRatio)
    {
        if (chargeVisual == null)
            return;

        float scale = Mathf.Lerp(minScale, maxScale, chargeRatio);

        chargeVisual.transform.localScale = Vector3.one * scale;
    }

    // 최대 충전 상태 설정
    public void SetFullyCharged(bool value)
    {
        isFullyCharged = value;

        if(!value)
        {
            SetVisualVisible(true);
        }
    }

    private void Update()
    {
        if (!isFullyCharged || chargeVisual == null)
            return;

        bool visible = Mathf.Sin(Time.time * blinkSpeed) > 0.0f;

        SetVisualVisible(visible);
    }

    private void SetVisualVisible(bool visible)
    {
        if(visualRenderers == null)
            return;

        foreach(Renderer visualRenderer in visualRenderers)
        {
            visualRenderer.enabled = visible;
        }
    }

    public void HideCharge()
    {
        if (chargeVisual != null)
        {
            Destroy(chargeVisual);
            chargeVisual = null;
            visualRenderers = null;
        }
    }
}
