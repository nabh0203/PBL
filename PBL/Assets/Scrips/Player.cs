using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public float Speed = 1f;
    public int lives = 3;
    public GameObject[] life = new GameObject[3];
    private MeshRenderer playerMeshRenderer;
    public string Scenename;
    void Start()
    {
        playerMeshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 newVelocity = rb.velocity;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newVelocity.x = -Speed;
            transform.rotation = Quaternion.Euler(new Vector3(90, 90, -90));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            newVelocity.x = Speed;
            transform.rotation = Quaternion.Euler(new Vector3(90, 180, 180));
        }
        else
        {
            newVelocity.x = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            newVelocity.y = Speed;
            transform.rotation = Quaternion.Euler(new Vector3(180, 90, -90));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            newVelocity.y = -Speed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, -90));
        }
        else
        {
            newVelocity.y = 0;
        }

        rb.velocity = newVelocity;
    }
    void OnTriggerEnter(Collider other)
    {
        // 유령과 부딪히면 다음 씬으로 이동
        if (other.gameObject.CompareTag("Cloud"))
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
                SceneManager.LoadScene(Scenename);
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