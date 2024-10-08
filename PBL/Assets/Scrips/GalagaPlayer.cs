using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalagaPlayer : MonoBehaviour
{
    //공용변수 실수형인 Speed는 0.05f 이다.
    public float Speed = 0.05f;
    //공용변수 게임오브젝트속성인 BulletPrefab를 지정
    public GameObject BulletPrefab;
    public float RightPosition = -11.11f;
    public float MiddlePosition = -11.94f;
    public float LeftPosition = -12.825f;
    //공용변수 위치,회전,크기조정 속성을 가진 BulletPosition를 지정
    public Transform BulletPosition;
    public int lives = 3;
    public GameObject[] life = new GameObject[3];
    private MeshRenderer playerMeshRenderer;
    private bool isSpawning = false;
    public GameObject[] PaperGaze; // 활성화할 오브젝트 배열
    private int activationCount = 0; // 활성화 횟수
    public AudioSource Audio;
    public AudioClip Music;
    public AudioSource Audio2;
    public AudioClip Music2;
    // Start is called before the first frame update

    void Start()
    {
        playerMeshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 좌측 방향키를 눌렀을 때
        {
            if (transform.position.x == RightPosition) // 현재 위치가 우측 끝 위치라면
            {
                transform.position = new Vector3(MiddlePosition, transform.position.y, transform.position.z); // 중앙 위치로 이동
            }
            else if (transform.position.x == MiddlePosition) // 현재 위치가 좌측 끝 위치라면
            {
                transform.position = new Vector3(LeftPosition, transform.position.y, transform.position.z); // 좌측 위치로 이동
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 우측 방향키를 눌렀을 때
        {
            if (transform.position.x == MiddlePosition) // 현재 위치가 중앙 위치라면
            {
                transform.position = new Vector3(RightPosition, transform.position.y, transform.position.z); // 우측 끝 위치로 이동
            }
            else if (transform.position.x == LeftPosition) // 현재 위치가 좌측 위치라면
            {
                transform.position = new Vector3(MiddlePosition, transform.position.y, transform.position.z); // 좌측 끝 위치로 이동
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isSpawning)
        {
            StartCoroutine(SpawnAndRotate());
            Audio.PlayOneShot(Music);
        }
    }

    IEnumerator SpawnAndRotate()
    {
        isSpawning = true;
        float randomZRotation = Random.Range(0, 360);

        Vector3 spawnPosition = BulletPosition.position;
        Quaternion fromRotation = Quaternion.Euler(0, 0, 0);
        Quaternion toRotation = Quaternion.Euler(0, 0, randomZRotation);

        float elapsedTime = 0;
        float duration = 0.5f;
        GameObject bulletCopy = Instantiate(BulletPrefab, spawnPosition, fromRotation);

        // Check if bulletCopy is not destroyed
        while (elapsedTime < duration && bulletCopy != null)
        {
            bulletCopy.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Check if bulletCopy is not destroyed
        if (bulletCopy != null)
        {
            bulletCopy.transform.rotation = toRotation;
        }

        isSpawning = false;
    }



    //트리거작동시 호출 명령
    public void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("O2"))
        {
            lives--;

            if (lives >= 0)
            {
                Destroy(life[lives]);
            }

            if (lives > 0)
            {
                StartCoroutine(FlashRedAndPause());
            }
            else
            {
                SceneManager.LoadScene("GalagaLose");
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Paper"))
        {
            Audio2.PlayOneShot(Music2);
            Debug.Log("획득");
            Destroy(other.gameObject);
            if (activationCount < PaperGaze.Length) // 배열 범위 확인
            {
                PaperGaze[activationCount].SetActive(true);
                activationCount++;

                if (activationCount >= 5)
                {
                    SceneManager.LoadScene("GalagaWin");
                }
            }
        }
    }



    IEnumerator FlashRedAndPause()
    {

        int numFlashes = 3;
        float flashDuration = 0.3f;

        Rigidbody playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody != null) playerRigidbody.velocity = Vector2.zero;

        for (int i = 0; i < numFlashes; i++)
        {
            playerMeshRenderer.enabled = false;
            yield return new WaitForSeconds(flashDuration);
            playerMeshRenderer.enabled = true;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}

