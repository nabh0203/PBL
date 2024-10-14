# 전주대학교 실감미디어 미디어파사드 PBL 프로젝트
![image 104](https://github.com/user-attachments/assets/8db704a7-e2eb-4006-9758-d241610a1efd)

**사용 기술**

- Unity
- C#
- NDI 5 

## PBL 그리고 Void*

> **진로와 관련된 분야라면 “무엇이든(Void&ast;)” 가능한**
> 
> 여름 계절 학기 전주대학교 주최로 진행하였던 **실감 미디어 파사드 PBL 프로젝트**입니다.
>
> c, c++ 언어에서 **generic 포인터**로 알려진 **Void***는 굉장히 특별한 타입입니다.
>
> **”그 어떤 타입이든 객체든 가리킬 수 있는 포인터”** 의 의미를 가진 함수이며
>
> 다르게 해석하면 ***“무엇이든”*** 이라는 의미를 가진 함수라고 생각합니다.
> 
> 해당 프로젝트에 개발자로 참여함으로써 VR 콘텐츠만이 아닌 진로와 관련된 분야라면
> 
>***“무엇이든 가능하다”***  라는 자신감을 내비칠 수 있게 해준 콘텐츠입니다.

---
## 미디어 파사드를 활용한 콘텐츠
![스크린샷 2024-04-04 003301](https://github.com/user-attachments/assets/21cf60bd-f40f-4cb5-a247-9e46b52b3b64)

>**미디어(media)** 와 건물의 외벽을 뜻하는 **파사드(facade)** 가 합성된 용어로, 건물의 외벽에 다양한 콘텐츠 영상을 투사 하는 것을 뜻합니다.
>
>저희는 미디어 파사드를 이용하여 보는 사람들에게 실감형 콘텐츠를 제공하였습니다.

---
## **개발 과정**


### 3°C 그리고 3가지 게임 구성

**지구의 온도가 3°C 만 오르면 온 세상은 물에 잠기게 된다.**

> 이번 프로젝트의 중점 키워드인 **“기후위기”** 와 *“**지구의 온도가 현재 온도보다 3°C 만 올라가기만 해도 지구는 물에 잠긴다”***
>
> 라는 문장을 연결지어 총 3가지의 게임을 제작하였으며 그 중 2가지 게임은 "갤러그" , "팩맨" 의 로직을 참고하였습니다.
>
> 사용자는 게임에서 이기면 1도가 올라가게 되고 지게되면 온도가 올라가지 않는 기획을 구성 하였습니다.

---
### 1. GameMaster
![스크린샷(429)](https://github.com/user-attachments/assets/f1ac2fc3-19f8-4b58-a8eb-e0f255d9517b)

> 자동차 팩맨 게임을 관리해주는 Master 스크립트를 제작하여
>
> **- List 를 사용한 오브젝트 관리**
> 
> **- 코루틴을 활용한 타이머제작 및 Scene 이동**
> 
> **- 플레이어가 먹은 오브젝트의 태그를 확인하여 지정된 갯수에 도달하면 UI 이미지 변경**
>
> 로직을 구현 및 관리하였습니다.  

### 2. Player

> 팩맨 자동차 게임에서 Player 로직을 담당하며
> 
> **코루틴** 을 사용하여 **Player** 가 **Cloud** 라는 태그를 가진 오브젝트와 트리거되면 깜빡이는
>
> 로직을 제작하였습니다.
> 
### GalagaPlayer

```csharp
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

```

<aside>
<img src="/icons/snippet_gray.svg" alt="/icons/snippet_gray.svg" width="40px" /> **GalagaPlayer 스크립트 제작중 중점 사항**

> 나무 갤러그 게임에서 **Player** 부분에 사용되는 스크립트 입니다.
갤러그 시스템상에서 **Player** 가 **좌 우 로만 움직이는 로직을 구현**하기 위하여
**Update** 함수 안에 **Input.GetKeyDown** 함수를 사용하여 **좌 우 로만 움직일수 있게** 
조작을 구현하였으며 원하는 **Position** 값의 위치로 이동할수 있게 총 3가지의 
**Position** 변수를 설정하였습니다.
> 
> 
> 그리하여 **If** 문으로 감싸 왼쪽 방향키를 눌렀을 시 변수로 지정한 **Right** , **Middle** 
> 변수의 위치값이라면 좌측으로 이동가능하게 반대로 우측 방향키를 눌렀을 시 
> 변수로 지정한 **Left** , **Middle** 변수의 위치값이라면 우측으로 이동할수 있게 
> 제작하였습니다.
> 
> 마지막으로 총알이 발사되어 나무와 트리거 됐을시에 나오는 **O2** 라는 오브젝트와
> **Player** 와 트리거 됐을시 음악재생과 배열 변수로 잡아 놓은 **Papergaze** 안에 
> 오브젝트를 **SetActive** 함수를 통해 활성화를 시키도록 제작하였습니다.
> 
> 그리고 **Papergaze** 의 갯수가 5개에 도달하면 **SceneMove** 함수를 통해 이동하게 
> 제작하는것으로 마무리 하였습니다.
> 
> 또한 **Input.GetKeyDown** 함수를 사용해 **Space** 키를 눌렀을시 0.5초에 한번씩
>  **360 º** 로 돌아가며 발사되는 도끼 총알을 나가게 제작하였습니다. 
> 
> 또한 **Space** 키를 눌렀을시 **Audio.PlayOneShot** 함수를 사용하여 음악이 한번
> 재생되게 제작하였습니다.
> 
</aside>

### GalagaGM

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalagaGM : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip BGM;
    public Text timerText;
    public string Scenename;
    private float timeLeft = 50f;

    void Start()
    {
        StartCoroutine(WaitForSceneSwitch());
        Audio.PlayOneShot(BGM);
        
    }

    void Update()
    {
        UpdateTimer();
    }
    IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(50f);
        SceneManager.LoadScene(Scenename);
    }

    void UpdateTimer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
        }

        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

}

```

<aside>
<img src="/icons/snippet_gray.svg" alt="/icons/snippet_gray.svg" width="40px" /> **GalagaGM 스크립트 제작중 중점 사항**

> 나무 갤러그 게임에 사용되는 스크립트 입니다.
> 
> 
> **TImer** , **BGM** 을 담당해주는 스크립트이며 게임이 시작되면 **BGM**을 재생시키고 **StartCoroutine** 함수를 통해 **Timer** 가 재생되게 제작하였습니다. 
> 
> 자동차 팩맨과 마찬가지로 시간이 종료되면 지정한 **Scene** 으로 이동할수 있게 
> 제작하였습니다.
> 
</aside>

### SpawnTree

```csharp
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
        // 3초마다 Generate() 메소드 호출
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

```

<aside>
<img src="/icons/snippet_gray.svg" alt="/icons/snippet_gray.svg" width="40px" /> **SpawnTree 스크립트 제작중 중점 사항**

> 나무 갤러그 게임에서 **Tree** 라는 오브젝트를 관리해주는 스크립트 입니다.
먼저 생성위치를 배열을 통해 3가지 위치에 지정한 뒤 게임 시작시
**InvokeRepeating** 함수를 사용하여 3초마다 랜덤한 위치에 **Tree** 오브젝트를 
생성합니다.
> 
> 
> ---
> 
> **Generate** 로직
> 
> > **1. int index = Random.Range(0, 3);** 에서  **Random.Range**를 통해 3가지를
>    랜덤으로 **index** 라는 변수에 집어 넣습니다.
> > 
> > 
> > **2.** 이러한 **index** 라는 변수는 **spawnPoints** 라는 배열안에 들어가게 됩니다.
> > 
> > **3.** **Trees** 오브젝트는 **Instantiate** 함수 ****를 통해 미리 지정한 **spawnPoint**
> >    위치값에서 생성됩니다.
> > 
> > **4.** 생성이 된 뒤에 **Vector3 moveDirection = Vector3.down;** 를 통해 아래로 
> >    이동하게 됩니다. 
> > 
> >    이 부분에서 **Trees** 오브젝트는 **Rigidbody** 속성을 받아
> >    **moveDirection * moveSpeed** 값만큼 아래로 이동하게 됩니다.
> > 
> 
> ---
> 
> 그다음 생성된 **Tree** 오브젝트들은 **Generate** 함수에 포함되어있는 **ShootBubble**
> 코루틴 함수를 실행하게 됩니다. 
> 
> 이러한 로직은 생성된 **Tree** 오브젝트들이 **O2** 오브젝트를 발사하기 위한 로직이며 
> 이렇게 생성된 **O2** 오브젝트는 앞서 설명 드렸던 **GalagaPlayer** 가 적용된 오브젝트와 상호 작용하여 트리거 됐을시 **GalagaPlayer** 에서 생성하였던 **Life** 시스템과 
> 상호 작용하여 라이프 시스템을구축하게 제작하였습니다.
> 
</aside>

