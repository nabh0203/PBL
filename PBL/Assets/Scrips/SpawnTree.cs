using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject Tree;
    public Transform[] spawnPoints; // ���� ��ġ �迭
    public float moveSpeed = 2f;    // �̵� �ӵ�
    public int BulletMinTime = 60;
    //������ BulletMaxTime�� ���� 180
    public int BulletMaxTime = 180;
    public GameObject Bubble; // ���� ��� ������
    public AudioSource Audio;
    public AudioClip Music;

    void Start()
    {
        // 5�ʸ��� Generate() �޼ҵ� ȣ��
        InvokeRepeating("Generate", 0f, 3f);
    }

    void Generate()
    {
        // ������ ��ġ���� ������Ʈ ����
        int index = Random.Range(0, 3);
        Transform spawnPoint = spawnPoints[index];
        GameObject Trees = Instantiate(Tree, spawnPoint.position, Quaternion.identity);
        StartCoroutine(ShootBubble(Trees.transform));
        // ������ ������Ʈ �̵�
        Vector3 moveDirection = Vector3.down;
        Trees.GetComponent<Rigidbody>().velocity = moveDirection * moveSpeed;
        
    }

    IEnumerator ShootBubble(Transform treeTransform)
    {
        
        // �߻� ���� �ð� ����
        float delay = Random.Range(BulletMinTime, BulletMaxTime) / 100f;
        yield return new WaitForSeconds(delay);
        if (treeTransform == null)
        {
            yield break;
        }
        // ������ ������Ʈ �պκ� ��ġ
        float forwardOffset = 0.5f;
        Vector3 bulletPos = treeTransform.position + new Vector3(0, -forwardOffset, 0);

        // ���� ��� ����
        GameObject bubbleInstance = Instantiate(Bubble, bulletPos, Quaternion.identity);
        Audio.PlayOneShot(Music);

        // ���� ��� ���� �� 1�� �� ����
        Destroy(bubbleInstance, 1f);
    }

}
