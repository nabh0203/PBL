using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Bullet : MonoBehaviour
{
   
    // Start is called before the first frame update
    //공영변수에 실수형 변수인 Speed는 500f 이다.
    public float Speed = 50f;
    void Start()
    {
        //게임이 시작되면 Rigidbody에 컴포넌트하여 0, -Speed, 0 만큼 힘을주어 밀어내라
        GetComponent<Rigidbody>().AddForce(0, -Speed, 0);
    }

    //트리거 작동시
    private void OnTriggerEnter(Collider other)
    {
        //다른 게임오브젝트를 파괴하라
        Destroy(other.gameObject);
        //자신의 게임 오브젝트로 파괴해라
        Destroy(gameObject);
    }
}
