using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GalagaPlayer : MonoBehaviour
{
    //���뺯�� �Ǽ����� Speed�� 0.05f �̴�.
    public float Speed = 0.05f;
    //���뺯�� ���ӿ�����Ʈ�Ӽ��� BulletPrefab�� ����
    public GameObject BulletPrefab;
    public float RightPosition = -11.11f;
    public float MiddlePosition = -11.94f;
    public float LeftPosition = -12.825f;
    //���뺯�� ��ġ,ȸ��,ũ������ �Ӽ��� ���� BulletPosition�� ����
    public Transform BulletPosition;
    public int lives = 3;
    public GameObject[] life = new GameObject[3];
    private MeshRenderer playerMeshRenderer;
    private bool isSpawning = false;
    public GameObject[] PaperGaze; // Ȱ��ȭ�� ������Ʈ �迭
    private int activationCount = 0; // Ȱ��ȭ Ƚ��
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
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���� ����Ű�� ������ ��
        {
            if (transform.position.x == RightPosition) // ���� ��ġ�� ���� �� ��ġ���
            {
                transform.position = new Vector3(MiddlePosition, transform.position.y, transform.position.z); // �߾� ��ġ�� �̵�
            }
            else if (transform.position.x == MiddlePosition) // ���� ��ġ�� ���� �� ��ġ���
            {
                transform.position = new Vector3(LeftPosition, transform.position.y, transform.position.z); // ���� ��ġ�� �̵�
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // ���� ����Ű�� ������ ��
        {
            if (transform.position.x == MiddlePosition) // ���� ��ġ�� �߾� ��ġ���
            {
                transform.position = new Vector3(RightPosition, transform.position.y, transform.position.z); // ���� �� ��ġ�� �̵�
            }
            else if (transform.position.x == LeftPosition) // ���� ��ġ�� ���� ��ġ���
            {
                transform.position = new Vector3(MiddlePosition, transform.position.y, transform.position.z); // ���� �� ��ġ�� �̵�
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



    //Ʈ�����۵��� ȣ�� ���
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
            Debug.Log("ȹ��");
            Destroy(other.gameObject);
            if (activationCount < PaperGaze.Length) // �迭 ���� Ȯ��
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

