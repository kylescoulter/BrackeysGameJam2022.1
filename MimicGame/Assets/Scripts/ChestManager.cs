using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityTemplateProjects
{
    public class ChestManager : MonoBehaviour
    {
        [SerializeField] private GameObject chestObj;
        [Header("Chest Materials")]
        [SerializeField] private Material brownChest;
        [SerializeField] private Material redChest;
        [SerializeField] private Material greenChest;
        
        [Header("Trim Materials")]
        [SerializeField] private Material ironTrim;
        [SerializeField] private Material silverTrim;
        [SerializeField] private Material goldTrim;
        
        [Header("Lock Materials")]
        [SerializeField] private Material ironLock;
        [SerializeField] private Material silverLock;
        [SerializeField] private Material magicLock;
        [SerializeField] private Material lockTrim;
        
        [SerializeField] private Material mimicChest;

        private List<Material> chestMats = new List<Material>();
        private List<Material> trimMats = new List<Material>();
        private List<Material> lockMats = new List<Material>();
        private List<String> owners = new List<string>();
        private Material chestMaterial;
        private Material trimMaterial;
        private Material lockBaseMaterial;
        private String owner;
        private Boolean rune;
        private Chest chest;
        

        private void Start()
        {
            FillMaterialLists();
            RandomizeChest();
            chest = chestObj.GetComponent<Chest>();
            CheckMimic();
            if (chest.isMimic)
            {
                chestMaterial = mimicChest;
            }
        }

        public GameObject GenerateChest()
        {
            chestObj.GetComponent<Chest>().ReskinChest(chestMaterial, trimMaterial, lockBaseMaterial, lockTrim, owner, rune);
            return chestObj;
        }

        private void FillMaterialLists()
        {
            chestMats.Add(brownChest);
            chestMats.Add(redChest);
            chestMats.Add(greenChest);
            
            trimMats.Add(ironTrim);
            trimMats.Add(silverTrim);
            trimMats.Add(goldTrim);
            
            lockMats.Add(ironLock);
            lockMats.Add(silverLock);
            lockMats.Add(magicLock);
            
            owners.Add("goblin");
            owners.Add("ogre");
            owners.Add("elf");
            
        }

        private void RandomizeChest()
        {
            chestMaterial = chestMats[Random.Range(0, chestMats.Count)];
            trimMaterial = trimMats[Random.Range(0, trimMats.Count)];
            lockBaseMaterial = lockMats[Random.Range(0, lockMats.Count)];
            owner = owners[Random.Range(0, owners.Count)];
            
            rune = Random.Range(0, 2) == 1;
        }

        private void CheckMimic()
        {
            if (chest.chestMaterial == brownChest)
            {
                if (chest.trimMaterial == ironTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
                else if (chest.trimMaterial == silverTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
                else if (chest.trimMaterial == goldTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
            }
            else if (chest.chestMaterial == redChest)
            {
                if (chest.trimMaterial == ironTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
                else if (chest.trimMaterial == silverTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                }
                else if (chest.trimMaterial == goldTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                }
            }
            else if (chest.chestMaterial == greenChest)
            {
                if (chest.trimMaterial == ironTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
                else if (chest.trimMaterial == silverTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                }
                else if (chest.trimMaterial == goldTrim)
                {
                    if (chest.lockMaterial == ironLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = true;
                        }
                    }
                    else if (chest.lockMaterial == silverLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                    else if (chest.lockMaterial == magicLock)
                    {
                        if (chest.owner.Equals("goblin"))
                        {
                            chest.isMimic = false;
                        }
                        else if (chest.owner.Equals("ogre"))
                        {
                            chest.isMimic = true;
                        }
                        else if (chest.owner.Equals("elf"))
                        {
                            chest.isMimic = false;
                        }
                    }
                }
            }
        }
    }
}