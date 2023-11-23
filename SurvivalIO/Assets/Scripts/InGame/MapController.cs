using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private const float MAP_SIZE = 32;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SpriteRenderer spriteRenderer = Utils.GetOrAddComponent<SpriteRenderer>(gameObject);
        //spriteRenderer.sprite = Managers.ResourceManager.Load<Sprite>("map sprite path");

        BoxCollider2D boxCollider = Utils.GetOrAddComponent<BoxCollider2D>(gameObject);
        boxCollider.isTrigger = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Sensor"))
        {
            Debug.Log($"{collision.name}: This object is set on wrong layer");
            return;
        }
        Vector3 playerPosition = collision.transform.position;
        Vector3 targetPosition = GetTargetPosition(in playerPosition);

        this.transform.Translate(targetPosition);
    }

    private Vector3 GetTargetPosition(in Vector3 playerPosition)
    {
        float distanceX = playerPosition.x - this.transform.position.x;
        float distanceY = playerPosition.y - this.transform.position.y;

        Vector3 targetDirection =
            Mathf.Abs(distanceX) > Mathf.Abs(distanceY) ? // 두 오브젝트 간 거리가 X축이 더 멀면 수평 이동 필요, Y축이 더 멀면 수직 이동 필요
            Vector3.right : Vector3.up;

        float playerMovingDirection =
            (targetDirection == Vector3.right) ? // 수평 이동할 경우 플레이어의 X축 이동 방향, 수직 이동할 경우 플레이어의 Y축 이동 방향
            (distanceX / Mathf.Abs(distanceX)) : (distanceY / Mathf.Abs(distanceY));

        return targetDirection * playerMovingDirection * MAP_SIZE;
    }


}
