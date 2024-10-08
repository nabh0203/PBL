using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionAfter10Seconds : MonoBehaviour
{
    public float moveSpeed = 1.0f;    // �̵� �ӵ�
    public float waitTime = 10.0f;    // ��� �ð�
    public GameObject Block;
    public List<Vector3> targetPositions;   // ������ ��ġ�� ����� ����Ʈ
    private bool canMove = false;    // �̵� ���� ����
    private Vector3 direction;      // �̵� ����
    private int currentPositionIndex = 0; // ���� ��ǥ �ε���

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

    // ��� �ð� ���� ����ϰ� �̵� ���� �÷��׸� Ȱ��ȭ�մϴ�.
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        Block.SetActive(false);
    }

    // ��ǥ ��ġ�� �̵���Ű�� �޼���
    void MoveObject()
    {
        Vector3 targetPosition = targetPositions[currentPositionIndex];    // �̵��� ��ǥ

        // ��ǥ ��ġ�� ������ ����մϴ�.
        Vector3 targetDirection = (targetPosition - transform.position).normalized;

        // ��ǥ ��ġ�� �̵��մϴ�.
        transform.position += targetDirection * moveSpeed * Time.deltaTime;

        // ���� ���� ��ġ���� ��ǥ ��ġ���� �Ÿ��� �̵� �ӵ����� ª���� ���� ��ǥ�� �̵��մϴ�.
        if (Vector3.Distance(transform.position, targetPosition) < moveSpeed * Time.deltaTime)
        {
            currentPositionIndex++;

            // ���� ��ǥ �ε����� ����Ʈ ������ ����� ó�� ��ǥ�� ���ư��ϴ�.
            if (currentPositionIndex >= targetPositions.Count)
            {
                currentPositionIndex = 0;
            }
        }
    }
}
