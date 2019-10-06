using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace bq
{
    public class DiskFactory : MonoBehaviour
    {
        public GameObject DiskPrefab;

        private List<GameObject> used = new List<GameObject>();
        private List<GameObject> free = new List<GameObject>();

        private void Awake()
        {
            DiskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Disk"), Vector3.zero, Quaternion.identity);
            DiskPrefab.SetActive(false);
        }

        public GameObject GetDisk(int Round)
        {
            GameObject NewDisk = null;
            if (free.Count > 0)
            {
                NewDisk = free[0];
                free.Remove(free[0]);
            }
            else
            {
                NewDisk = GameObject.Instantiate<GameObject>(DiskPrefab, Vector3.zero, Quaternion.identity);
                NewDisk.name = NewDisk.GetInstanceID().ToString();
            }

            NewDisk.GetComponent<Disk>().SetLevel(Round);

            return NewDisk;
        }

        public void FreeDisk(GameObject UsedDisk)
        {
            if (UsedDisk != null)
            {
                UsedDisk.SetActive(false);
                free.Add(UsedDisk);
                used.Remove(UsedDisk);
            }
        }


    }
}
