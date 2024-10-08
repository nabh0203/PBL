using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class excontrol : MonoBehaviour
{
    // Start is called before the first frame update
    //공용변수 실수형인 Speed 의 값은 500f 다.
    public float Speed = 500f;
    void Start()
    {
        //게임이 시작하면 Rigidbody를 컴포넌트하고 0, 500f, 0만큼 밀어내라
        GetComponent<Rigidbody>().AddForce(0, Speed, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            Destroy(other.gameObject);
        }
    }

}
