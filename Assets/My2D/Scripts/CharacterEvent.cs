using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //ĳ���Ϳ� ���õ� �̺�Ʈ �Լ����� �����ϴ� Ŭ����
    public class CharacterEvent
    {
        //ĳ���Ͱ� �������� ������ ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> characterDamaged;

        //ĳ���Ͱ� ü���� ȸ���Ҷ� ��ϵ� �Լ� ȣ��
        public static UnityAction<GameObject, float> characterHealed;

        //....



    }
}