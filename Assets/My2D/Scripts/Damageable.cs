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

        //데미지 입을때 등록된 함수 호출
        public UnityAction<float, Vector2> hitAction;

        //체력
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

                //죽음 처리
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
                //애니메이션
                animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        //무적모드
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
            //참조
            animator = GetComponent<Animator>();

        }

        private void Start()
        {

            //초기화
            CurrentHealth = MaxHealth;
            countdown = invincibleTimer;
        }

        private void Update()
        {
            //무적상태이면 무적 타이머를 돌린다
            if(isInvincible)
            {
                if( countdown <= 0f )
                {
                    //무적모드 취소
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
                //무적모드 초기화
                isInvincible = true;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}가 현재 체력은 {CurrentHealth}");

                //애니메이션  
                LockVelocity = true;
                animator.SetTrigger(AnimationString.HitTrigger);

                //데미지 효과
                /*if(hitAction != null )
                {
                    hitAction.Invoke(damage, knocback);
                }*/
                hitAction?.Invoke(damage, knocback);
                CharacterEvent.characterDamaged?.Invoke(gameObject, damage);
            }

        }

        // 체력 회복
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

