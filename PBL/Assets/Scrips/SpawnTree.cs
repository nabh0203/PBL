using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    public GameObject Tree;
    public Transform[] spawnPoints; // 생성 위치 배열
    public float moveSpeed = 2f;    // 이동 속도
    public int BulletMinTime = 60;
    //공변수 BulletMaxTime의 값은 180
    public int BulletMaxTime = 180;
    public GameObject Bubble; // 공기 방울 프리팹
    public AudioSource Audio;
    public AudioClip Music;

    void Start()
    {
        // 5초마다 Generate() 메소드 호출
        InvokeRepeating("Generate", 0f, 3f);
    }

    void Generate()
    {
        // 랜덤한 위치에서 오브젝트 생성
        int index = Random.Range(0, 3);
        Transform spawnPoint = spawnPoints[index];
        GameObject Trees = Instantiate(Tree, spawnPoint.position, Quaternion.identity);
        StartCoroutine(ShootBubble(Trees.transform));
        // 생성된 오브젝트 이동
        Vector3 moveDirection = Vector3.down;
        Trees.GetComponent<Rigidbody>().velocity = moveDirection * moveSpeed;
        
    }

    IEnumerator ShootBubble(Transform treeTransform)
    {
        
        // 발사 지연 시간 설정
        float delay = Random.Range(BulletMinTime, BulletMaxTime) / 100f;
        yield return new WaitForSeconds(delay);
        if (treeTransform == null)
        {
            yield break;
        }
        // 생성된 오브젝트 앞부분 위치
        float forwardOffset = 0.5f;
        Vector3 bulletPos = treeTransform.position + new Vector3(0, -forwardOffset, 0);

        // 공기 방울 생성
        GameObject bubbleInstance = Instantiate(Bubble, bulletPos, Quaternion.identity);
        Audio.PlayOneShot(Music);

        // 공기 방울 생성 후 1초 후 삭제
        Destroy(bubbleInstance, 1f);
    }

}
