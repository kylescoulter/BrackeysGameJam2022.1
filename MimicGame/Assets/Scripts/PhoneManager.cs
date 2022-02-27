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
        [SerializeField] private TextMeshPro total;
        [SerializeField] private TextMeshPro hint;

        private Boolean isPhoneOpen;
        private Vector3 openPhone;
        private Vector3 closedPhone;

        private PlayerManager player;
        private GameObject chestObj;
        private Chest chest;
        private bool passed;
        

        private void Start()
        {
            openPhone = openPhoneLocation.transform.localPosition;
            closedPhone = closedPhoneLocation.transform.localPosition;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        }

        private void Update()
        {
            if (chestObj == null)
            {
                chestObj = GameObject.FindGameObjectWithTag("Chest");
                if (chestObj != null)
                {
                    chest = chestObj.GetComponent<Chest>();
                }
            }
        }

        public void OpenPhone()
        {
            CheckPlayer();
            SetPaymentAndReward();
            var seq = DOTween.Sequence();
            seq.Insert(0f, payment.DOColor(Color.white, 0f));
            seq.Insert(0f, reward.DOColor(Color.white, 0f));
            seq.Insert(0f, total.DOColor(Color.white, 0f));
            seq.Insert(0f, hint.DOFade(0, 0f));
            seq.Insert(0f, payment.DOFade(0, 0f));
            seq.Insert(0f, reward.DOFade(0, 0f));
            seq.Insert(0f, total.DOFade(0, 0f));
            
            seq.Insert(0f, phoneTransform.DOLocalMove(openPhone, 1f));
            
            seq.InsertCallback(.2f, delegate
            {
                isPhoneOpen = true;
            });

            seq.Insert(1f, payment.DOFade(1, 2f));
            seq.InsertCallback(2f, delegate
            {
                if (!chest.isMimic)
                {
                    seq.Insert(2f, reward.DOFade(1, 2f));
                }
            });
            seq.InsertCallback(4f, delegate
            {
                if (!passed)
                {
                    seq.Insert(4f, payment.DOColor(Color.red, .5f));
                    seq.Insert(4f, payment.DOFade(.1f, 2f));
                    if (!chest.isMimic)
                    {
                        seq.Insert(5f, reward.DOColor(Color.red, .5f));
                        seq.Insert(5f, reward.DOFade(.1f, 2f));
                    }
                }
            });
            seq.Insert(5f, total.DOFade(1, 2f));
            seq.Insert(7f, hint.DOFade(1, 4f));
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
            var totalAmount = 0;
            if (!chest.isMimic && passed)
            {
                totalAmount = chest.GetChestPayment() + chest.GetChestPrize();
            }
            else if (chest.isMimic && passed)
            {
                totalAmount = chest.GetChestPayment();
            }
            payment.text = "Payment: " + chest.GetChestPayment() + "p";
            reward.text = "Chest Reward: " + chest.GetChestPrize() + "p";
            total.text = "Total: " + totalAmount + "p";
            player.AddMoney(totalAmount);
        }

        private void CheckPlayer()
        {
            if (chest.isMimic && chest.IsMarked)
            {
                passed = true;
            }
            else if (chest.isMimic && !chest.IsMarked)
            {
                passed = false;
            }
            else if (!chest.isMimic && chest.IsMarked)
            {
                passed = false;
            }
            else if (!chest.isMimic && !chest.IsMarked)
            {
                passed = true;
            }
        }
    }
}