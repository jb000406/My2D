using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region Variables
        //���ݷ�
        [SerializeField] private float attackDamage = 10f;

        public Vector2 knockback = Vector2.zero;

        #endregion
        //�浹 üũ�ؼ� ���ݷ� ��ŭ ������ �ش�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                //knockback�� ���� ����
                Vector2 deliveredknockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y); 

                damageable.TakeDamage(attackDamage, knockback);
            }
        }
    }
}

