using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class excontrol : MonoBehaviour
{
    // Start is called before the first frame update
    //���뺯�� �Ǽ����� Speed �� ���� 500f ��.
    public float Speed = 500f;
    void Start()
    {
        //������ �����ϸ� Rigidbody�� ������Ʈ�ϰ� 0, 500f, 0��ŭ �о��
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
