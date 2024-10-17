using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //������ ������ ��ϵ� �Լ� ȣ��
        public UnityAction<float, Vector2> hitAction;

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

        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool(AnimationString.LockVelocity, value);
            }
        }
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

        public void TakeDamage(float damage, Vector2 knocback)
        {
            if (!isDeath && !isInvincible)
            {
                //������� �ʱ�ȭ
                isInvincible = true;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}�� ���� ü���� {CurrentHealth}");

                //�ִϸ��̼�  
                LockVelocity = true;
                animator.SetTrigger(AnimationString.HitTrigger);

                //������ ȿ��
                /*if(hitAction != null )
                {
                    hitAction.Invoke(damage, knocback);
                }*/
                hitAction?.Invoke(damage, knocback);
                CharacterEvent.characterDamaged?.Invoke(gameObject, damage);
            }

        }

        // ü�� ȸ��
        public bool Heal(float amount)
        {
            if(CurrentHealth >= MaxHealth)
            {
                return false;
            }

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            CharacterEvent.characterHealed?.Invoke(gameObject, amount);

            return true;
        }



    }
}

