using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bq;

public class FirstController : MonoBehaviour, ISceneController, UserAction {

    public bool Playing;
    private int Round = 1;
    private float time = 0;
    private int DiskFlyOneTime;
    UserGUI userGUI;
    private Queue<GameObject> Disks = new Queue<GameObject>();

    private static int max = 10;
    private static int min = 3;
    private Vector3[] position = new Vector3[max];

    public DiskFactory Factory;

    void Awake()
    {
        Director director = Director.GetInstance();
        director.CurrentScenceController = this;
        director.CurrentScenceController.LoadResources();
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;
        for (int i = 0; i < max; i++)
        {
            position[i] = new Vector3(10-2*i, 10-2*i, -7);
        }
    }

    public void LoadResources()
    {
        
    }

    public void restart()
    {
        this.Round = 1;
        this.Playing = false;
        this.time = 0;
    }

    void Start () {
        Factory = Singleton<DiskFactory>.Instance; 
    }
	
	void Update () {
        if (userGUI.status == 1)
        {
            if (time > 2f)
            {
                RoundOver();
            } else
            {
                time += Time.deltaTime;
                if (this.Playing == false)
                {
                    DiskFlyOneTime = Random.Range(min, max);

                    GameObject tmp;
                    for (int i = 0; i < DiskFlyOneTime; i++)
                    {
 
                        tmp = Factory.GetDisk(this.Round);
                        tmp.transform.position = position[i];
                        tmp.transform.GetComponent<Rigidbody>().velocity = tmp.GetComponent<Disk>().direction * tmp.GetComponent<Disk>().speed;
                        Disks.Enqueue(tmp);
                    }
                    this.Playing = true;
                }
            }
        }
	}

    private void RoundOver()
    {
        while(Disks.Count > 0)
        {
            Factory.FreeDisk(Disks.Dequeue());
        }
        this.Round++;
        this.time = 0;
        this.Playing = false;
        if (Round > 10)
        {
            userGUI.status = 0;
        }
    }

    public void hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                hit.collider.gameObject.SetActive(false);
                userGUI.Score += (0.1f * Round + 1) * (0.1f * Round + 1);
            }

        }
    }
}
