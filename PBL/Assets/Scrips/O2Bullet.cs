using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Bullet : MonoBehaviour
{
   
    // Start is called before the first frame update
    //���������� �Ǽ��� ������ Speed�� 500f �̴�.
    public float Speed = 50f;
    void Start()
    {
        //������ ���۵Ǹ� Rigidbody�� ������Ʈ�Ͽ� 0, -Speed, 0 ��ŭ �����־� �о��
        GetComponent<Rigidbody>().AddForce(0, -Speed, 0);
    }

    //Ʈ���� �۵���
    private void OnTriggerEnter(Collider other)
    {
        //�ٸ� ���ӿ�����Ʈ�� �ı��϶�
        Destroy(other.gameObject);
        //�ڽ��� ���� ������Ʈ�� �ı��ض�
        Destroy(gameObject);
    }
}
