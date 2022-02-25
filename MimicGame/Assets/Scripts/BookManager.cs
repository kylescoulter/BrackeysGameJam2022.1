using System;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class BookManager : MonoBehaviour
    {
        
        [SerializeField] private Animator bookAnimator;

        //private Boolean isAnimating;
        
        private void Start()
        {
           
        }

        public void OpenBook()
        {
            if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
            {
                bookAnimator.SetTrigger("Open");
            }
        }

        public void CloseBook()
        {
            if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
            {
                bookAnimator.SetTrigger("Close");
            }
        }
    }
}