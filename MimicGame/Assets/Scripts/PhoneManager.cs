using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class PhoneManager : MonoBehaviour
    {
        [SerializeField] private Transform phoneTransform;
        [SerializeField] private Transform openPhoneLocation;
        [SerializeField] private Transform closedPhoneLocation;
        [SerializeField] private TextMeshPro payment;
        [SerializeField] private TextMeshPro reward;
        [SerializeField] private TextMeshPro hint;

        private Boolean isPhoneOpen;
        private Vector3 openPhone;
        private Vector3 closedPhone;

        private void Start()
        {
            openPhone = openPhoneLocation.transform.localPosition;
            closedPhone = closedPhoneLocation.transform.localPosition;
            StartCoroutine(SetPaymentAndReward());
        }

        public void OpenPhone()
        {
            var seq = DOTween.Sequence();
            seq.Insert(0f, hint.DOFade(0, 0f));
            seq.Insert(0f, payment.DOFade(0, 0f));
            seq.Insert(0f, reward.DOFade(0, 0f));
            seq.Insert(0f, phoneTransform.DOLocalMove(openPhone, 1f));
            
            seq.InsertCallback(.2f, delegate
            {
                isPhoneOpen = true;
            });

            seq.Insert(1f, payment.DOFade(1, 2f));
            seq.Insert(2f, reward.DOFade(1, 2f));
            seq.Insert(5f, hint.DOFade(1, 4f));
        }

        public void ClosePhone()
        {
            var seq = DOTween.Sequence();

            seq.InsertCallback(.4f, delegate
            {
                isPhoneOpen = false;
            });
            seq.Insert(3f, phoneTransform.DOLocalMove(closedPhone, 1f));
            seq.Insert(4f, hint.DOFade(0, 0f));
        }

        public bool IsPhoneOpen()
        {
            return isPhoneOpen;
        }

        public IEnumerator SetPaymentAndReward()
        {
            yield return new WaitForSeconds(5);
            var chest = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>();
            payment.text += chest.GetChestPayment() + "p";
            reward.text += chest.GetChestPrize() + "p";
        }
    }
}