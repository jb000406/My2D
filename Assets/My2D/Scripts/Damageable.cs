using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //ü��
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }

        private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set 
            {
                currentHealth = value; 

                //���� ó��
                if(currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }

        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            { 
                isDeath = value;
                //�ִϸ��̼�
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        //�������
        private bool isInvincible = false;
        [SerializeField] private float invincibleTimer = 3f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            //����
            animator = GetComponent<Animator>();

        }

        private void Start()
        {

            //�ʱ�ȭ
            CurrentHealth = MaxHealth;
            countdown = invincibleTimer;
        }

        private void Update()
        {
            //���������̸� ���� Ÿ�̸Ӹ� ������
            if(isInvincible)
            {
                if( countdown <= 0f )
                {
                    //������� ���
                    isInvincible = false;

                    countdown = invincibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage)
        {
            if (!isDeath && !isInvincible)
            {
                //������� �ʱ�ȭ
                isInvincible = true;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}�� ���� ü���� {CurrentHealth}");

                //�ִϸ��̼�  
                animator.SetTrigger(AnimationString.HitTrigger);
            }

        }



    }
}

