using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float boundaryMargin = 0.5f;

    Rigidbody rb;
    private Vector3 moveInput = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = 0.0f;
        float z = 0.0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                x -= 1f;

            if (Keyboard.current.dKey.isPressed ||
                Keyboard.current.rightArrowKey.isPressed)
                x += 1f;

            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
                z += 1f;

            if (Keyboard.current.sKey.isPressed ||
                Keyboard.current.downArrowKey.isPressed)
                z -= 1f;
        }

        moveInput = new Vector3(x, 0.0f, z).normalized;
    }

    private void FixedUpdate()
    {
        if(playerData == null || rb == null || mainCamera == null)
            return;

        Vector3 nextPosition = rb.position + moveInput * playerData.MoveSpeed * Time.fixedDeltaTime;

        // 이동 범위 제한
        GetCameraBounds(out Vector2 xBounds, out Vector2 zBounds);

        nextPosition.x = Mathf.Clamp(
            nextPosition.x,
            xBounds.x,
            xBounds.y
        );
        nextPosition.z = Mathf.Clamp(
            nextPosition.z,
            zBounds.x,
            zBounds.y
        );

        rb.MovePosition(nextPosition);
    }

    /// <summary>
    /// 카메라 화면을 기준으로 플레이어의 이동 가능 범위를 계산합니다.
    /// 화면의 네 모서리에서 Ray를 발사하여 플레이어 높이의 평면과 교차하는 지점을 구합니다.
    /// </summary>
    /// <param name="xBounds">x축 이동 가능 범위 (최소값, 최대값)</param>
    /// <param name="zBounds">z축 이동 가능 범위 (최소값, 최대값)</param>
    private void GetCameraBounds(out Vector2 xBounds, out Vector2 zBounds)
    {
        Plane playerPlane = new Plane(
            Vector3.up,
            new Vector3(0.0f, rb.position.y, 0.0f)
        );

        Vector3[] corners = new Vector3[4];

        Vector2[] viewportPoints =
        {
            new Vector2( 0.0f, 0.0f ),
            new Vector2( 1.0f, 0.0f ),
            new Vector2( 0.0f, 1.0f ),
            new Vector2( 1.0f, 1.0f ),
        };

        for(int i = 0; i < viewportPoints.Length; i++)
        {
            Ray ray = mainCamera.ViewportPointToRay(
                new Vector3(viewportPoints[i].x, viewportPoints[i].y, 0.0f)
            );

            if(playerPlane.Raycast(ray, out float distance))
            {
                corners[i] = ray.GetPoint(distance);
            }
        }

        float minX = corners[0].x;
        float maxX = corners[0].x;
        float minZ = corners[0].z;
        float maxZ = corners[0].z;

        foreach(Vector3 corner in corners)
        {
            minX = Mathf.Min(minX, corner.x);
            maxX = Mathf.Max(maxX, corner.x);
            minZ = Mathf.Min(minZ, corner.z);
            maxZ = Mathf.Max(maxZ, corner.z);
        }

        xBounds = new Vector2(minX + boundaryMargin, maxX - boundaryMargin);
        zBounds = new Vector2(minZ + boundaryMargin, maxZ - boundaryMargin);
    }
}
