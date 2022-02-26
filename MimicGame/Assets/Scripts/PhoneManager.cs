using System;
using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class PhoneManager : MonoBehaviour
    {
        
        [SerializeField] private Animator bookAnimator;
        [SerializeField] private Transform phoneTransform;
        [SerializeField] private Transform openPhoneLocation;
        [SerializeField] private Transform closedPhoneLocation;

        private Boolean isPhoneOpen;
        private Vector3 openPhone;
        private Vector3 closedPhone;
        
        
        private void Start()
        {
            openPhone = openPhoneLocation.transform.localPosition;
            closedPhone = closedPhoneLocation.transform.localPosition;
        }

        public void OpenPhone()
        {
            var seq = DOTween.Sequence();
            seq.Insert(0f, phoneTransform.DOLocalMove(openPhone, 1f));
            
            seq.InsertCallback(.2f, delegate
            {
                isPhoneOpen = true;
            });
        }

        public void ClosePhone()
        {
            var seq = DOTween.Sequence();

            seq.InsertCallback(.4f, delegate
            {
               
                isPhoneOpen = false;
                
            });
            seq.Insert(3f, phoneTransform.DOLocalMove(closedPhone, 1f));
        }

        public bool IsPhoneOpen()
        {
            return isPhoneOpen;
        }
    }
}