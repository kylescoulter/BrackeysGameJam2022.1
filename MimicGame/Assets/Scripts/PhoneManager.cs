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

        private GameObject chestObj;
        private Chest chest;

        private void Awake()
        {
            /*BaseGameManager.levelLoaded += SetPaymentAndReward;*/
        }

        private void Start()
        {
           
            openPhone = openPhoneLocation.transform.localPosition;
            closedPhone = closedPhoneLocation.transform.localPosition;
            SetPaymentAndReward();
        }

        private void Update()
        {
            if (chestObj == null)
            {
                chestObj = GameObject.FindGameObjectWithTag("Chest");
                chest = chestObj.GetComponent<Chest>();
                SetPaymentAndReward();
            }
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

        public void ClosePhone(bool animate)
        {
            var seq = DOTween.Sequence();
            if (animate)
            {
                seq.InsertCallback(.4f, delegate
                {
                    isPhoneOpen = false;
                });
                seq.Insert(3f, phoneTransform.DOLocalMove(closedPhone, 1f));
                seq.Insert(4f, hint.DOFade(0, 0f));
            }
            else
            {
                isPhoneOpen = false;
                seq.Insert(0f, phoneTransform.DOLocalMove(closedPhone, 0f));
                seq.Insert(0f, hint.DOFade(0, 0f));
            }
            
        }

        public bool IsPhoneOpen()
        {
            return isPhoneOpen;
        }

        public void SetPaymentAndReward()
        {
            Debug.Log("Payment being set");
            var chestObj = GameObject.FindGameObjectWithTag("Chest");
            Debug.Log("Chest Found");
            chest = chestObj.GetComponent<Chest>();
            payment.text = "Payment: " + chest.GetChestPayment() + "p";
            reward.text = "Chest Reward: " + chest.GetChestPrize() + "p";
            RewardPlayer();
        }

        public void RewardPlayer()
        {
            PlayerManager.AddMoney(chest.GetChestPayment() + chest.GetChestPrize());
        }
    }
}