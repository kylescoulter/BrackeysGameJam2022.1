using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace UnityTemplateProjects
{
    public class Chest : MonoBehaviour
    {
        [Header("Chest Properties")]
        public Material trimMaterial;
        public Material chestMaterial;
        public Material lockMaterial;
        public String owner;
        public Boolean hasRune;
        public Boolean isMimic;
        
        

        [Header("Chest Renderers")]
        [SerializeField] private GameObject chestObject;
        [SerializeField] private GameObject trimObject;
        
        [Header("Lock Renderers")]
        [SerializeField] private GameObject lockTrimObject;
        [SerializeField] private MeshRenderer lockBase;
        
        [SerializeField] private GameObject mark;
        [SerializeField] private GameObject markHint;

        private bool markable;
        private int prize;
        private int payment;
        private bool isMarked;

        public void ReskinChest(Material chestMat, Material trimMat, Material lockMat, Material lockTrimMat, String ownerStr, bool rune)
        {
            foreach (var obj in chestObject.GetComponentsInChildren<MeshRenderer>())
            {
                obj.material = chestMat;
            }
            
            foreach (var obj in trimObject.GetComponentsInChildren<MeshRenderer>())
            {
                obj.material = trimMat;
            }

            foreach (var obj in lockTrimObject.GetComponentsInChildren<MeshRenderer>())
            {
                obj.material = lockTrimMat;
            }

            chestMaterial = chestMat;
            trimMaterial = trimMat;
            lockMaterial = lockMat;
            lockBase.material = lockMat;
            owner = ownerStr;
            //hasRune = rune;

            prize = Random.Range(100, 1000);
        }

        public Boolean ActivateMark()
        {
            if (markable)
            {
                Debug.Log("Marked chest");
                mark.SetActive(true);
                isMarked = true;
            }

            return mark.activeSelf;
        }

        public Boolean DeactivateMark()
        {
            if (markable)
            {
                Debug.Log("Marked chest");
                mark.SetActive(false);
                isMarked = false;
            }
            
            return mark.activeSelf;
        }

        public void SetChestPayment(int val)
        {
            payment = val;
        } 
        
        public int GetChestPayment()
        {
            return payment;
        }
        
        public int GetChestPrize()
        {
            return prize;
        }

        public bool IsMarked => isMarked;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                markHint.SetActive(true);
                markable = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                markHint.SetActive(false);
                markable = false;
            }
        }
    }
}