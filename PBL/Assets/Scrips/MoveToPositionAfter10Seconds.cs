using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionAfter10Seconds : MonoBehaviour
{
    public float moveSpeed = 1.0f;    // 이동 속도
    public float waitTime = 10.0f;    // 대기 시간
    public GameObject Block;
    public List<Vector3> targetPositions;   // 선택한 위치가 저장될 리스트
    private bool canMove = false;    // 이동 가능 여부
    private Vector3 direction;      // 이동 방향
    private int currentPositionIndex = 0; // 현재 좌표 인덱스

    // Start is called before the first frame update
    void Start()
    {
        Block.SetActive(true);
        StartCoroutine(WaitAndMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveObject();
        }
    }

    // 대기 시간 동안 대기하고 이동 가능 플래그를 활성화합니다.
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        Block.SetActive(false);
    }

    // 목표 위치로 이동시키는 메서드
    void MoveObject()
    {
        Vector3 targetPosition = targetPositions[currentPositionIndex];    // 이동할 좌표

        // 목표 위치로 방향을 계산합니다.
        Vector3 targetDirection = (targetPosition - transform.position).normalized;

        // 목표 위치로 이동합니다.
        transform.position += targetDirection * moveSpeed * Time.deltaTime;

        // 만약 현재 위치에서 목표 위치까지 거리가 이동 속도보다 짧으면 다음 좌표로 이동합니다.
        if (Vector3.Distance(transform.position, targetPosition) < moveSpeed * Time.deltaTime)
        {
            currentPositionIndex++;

            // 현재 좌표 인덱스가 리스트 범위를 벗어나면 처음 좌표로 돌아갑니다.
            if (currentPositionIndex >= targetPositions.Count)
            {
                currentPositionIndex = 0;
            }
        }
    }
}
