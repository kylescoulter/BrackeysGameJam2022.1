using System;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class BookManager : MonoBehaviour
    {
        
        [SerializeField] private Animator bookAnimator;
        [SerializeField] private Transform bookTransform;
        [SerializeField] private Transform openBookLocation;
        [SerializeField] private Transform closedBookLocation;
        [SerializeField] private List<Material> bookPages;
        [SerializeField] private GameObject leftPage;
        [SerializeField] private GameObject rightPage;
        [SerializeField] private GameObject leftHint;
        [SerializeField] private GameObject rightHint;
        [SerializeField] private GameObject bookHint;
        

        private Boolean isBookOpen;
        private Vector3 openBook;
        private Vector3 closedBook;
        private bool readyToOpen;
        
        
        private void Start()
        {
            openBook = openBookLocation.transform.localPosition;
            closedBook = closedBookLocation.transform.localPosition;
            var seq = DOTween.Sequence();

            seq.InsertCallback(0f, delegate { bookHint.SetActive(true); });
            seq.InsertCallback(3f, delegate
            {
                bookHint.SetActive(false);
                readyToOpen = true;
            });
            
        }

        public void OpenBook()
        { 
            var seq = DOTween.Sequence();
            seq.Insert(0f, bookTransform.DOLocalMove(openBook, 1f));
            seq.InsertCallback(0f, delegate
            {
                leftPage.SetActive(false);
                rightPage.SetActive(false); 
                leftHint.SetActive(false);
                rightHint.SetActive(false);
                bookHint.SetActive(false);
            });

            seq.InsertCallback(.2f, delegate
            {
                if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
                {
                    isBookOpen = true;
                    bookAnimator.ResetTrigger("Close");
                    bookAnimator.SetTrigger("Open");
                }
            });
            seq.InsertCallback(2f, delegate
            {
                leftPage.SetActive(true); 
                rightPage.SetActive(true);
                leftHint.SetActive(true);
                rightHint.SetActive(true);
                leftPage.GetComponent<MeshRenderer>().material = bookPages[0];
                rightPage.GetComponent<MeshRenderer>().material = bookPages[1];
            });
            seq.Insert(2.5f, leftPage.GetComponent<MeshRenderer>().material.DOFade(1, 2f));
            seq.Insert(2.5f, rightPage.GetComponent<MeshRenderer>().material.DOFade(1, 2f));
        }

        public void NextPage()
        {
            var index = 0;
            
            var seq = DOTween.Sequence();

            seq.InsertCallback(0f, delegate
            {
                if (rightPage.GetComponent<MeshRenderer>().material != bookPages[5])
                {
                    var count = 2;
                    foreach (var mat in bookPages)
                    {
                        if (leftPage.GetComponent<MeshRenderer>().material.name.Contains(mat.name))
                        {
                            index = bookPages.IndexOf(mat);
                        }
                    }
                    

                    leftPage.GetComponent<MeshRenderer>().material = bookPages[index + count];
                    count++;
                    rightPage.GetComponent<MeshRenderer>().material = bookPages[index + count];
                    count++;
                }
                else
                {
                    leftPage.GetComponent<MeshRenderer>().material = bookPages[6];
                    rightPage.GetComponent<MeshRenderer>().material = bookPages[7];
                }
            });
        }
        
        public void PrevPage()
        {
            var index = 0;
            
            var seq = DOTween.Sequence();

            seq.InsertCallback(0f, delegate
            {
                if (rightPage.GetComponent<MeshRenderer>().material != bookPages[1])
                {
                    var count = 2;
                    foreach (var mat in bookPages)
                    {
                        if (leftPage.GetComponent<MeshRenderer>().material.name.Contains(mat.name))
                        {
                            index = bookPages.IndexOf(mat);
                        }
                    }
                    

                    leftPage.GetComponent<MeshRenderer>().material = bookPages[index - count];
                    count--;
                    rightPage.GetComponent<MeshRenderer>().material = bookPages[index - count];
                    count--;
                }
                else
                {
                    leftPage.GetComponent<MeshRenderer>().material = bookPages[0];
                    rightPage.GetComponent<MeshRenderer>().material = bookPages[1];
                }
            });
        }

        public void CloseBook()
        {
            var seq = DOTween.Sequence();
            seq.InsertCallback(.4f, delegate
            {
                leftPage.SetActive(false);
                rightPage.SetActive(false);
                leftHint.SetActive(false);
                rightHint.SetActive(false);
            });

            seq.InsertCallback(.4f, delegate
            {
                if (bookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !bookAnimator.IsInTransition(0))
                {
                    bookAnimator.ResetTrigger("Open");
                    bookAnimator.SetTrigger("Close");
                    isBookOpen = false;
                }
            });
            seq.Insert(2f, bookTransform.DOLocalMove(closedBook, 1f));
        }

        public bool IsBookOpen()
        {
            return isBookOpen;
        }

        public bool ReadyToOpen => readyToOpen;
    }
}