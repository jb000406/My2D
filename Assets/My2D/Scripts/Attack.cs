using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        //���ݷ�
        [SerializeField] private float attackDamage = 10f;

        //�浹 üũ�ؼ� ���ݷ� ��ŭ ������ �ش�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }
        }
    }
}

