using System;
using DG.Tweening;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class BookManager : MonoBehaviour
    {
        
        [SerializeField] private Animator bookAnimator;
        [SerializeField] private Transform bookTransform;
        [SerializeField] private Transform openBookLocation;
        [SerializeField] private Transform closedBookLocation;

        private Boolean isBookOpen;
        private Vector3 openBook;
        private Vector3 closedBook;
        
        
        private void Start()
        {
            openBook = openBookLocation.transform.localPosition;
            closedBook = closedBookLocation.transform.localPosition;
        }

        public void OpenBook()
        {
            var seq = DOTween.Sequence();
            seq.Insert(0f, bookTransform.DOLocalMove(openBook, 1f));
            
            seq.InsertCallback(.2f, delegate
            {
                if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
                {
                    isBookOpen = true;
                    bookAnimator.ResetTrigger("Close");
                    bookAnimator.SetTrigger("Open");
                }
            });
        }

        public void CloseBook()
        {
            var seq = DOTween.Sequence();

            seq.InsertCallback(.4f, delegate
            {
                if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
                {
                    bookAnimator.ResetTrigger("Open");
                    bookAnimator.SetTrigger("Close");
                    isBookOpen = false;
                }
            });
            seq.Insert(3f, bookTransform.DOLocalMove(closedBook, 1f));
        }

        public bool IsBookOpen()
        {
            return isBookOpen;
        }
    }
}