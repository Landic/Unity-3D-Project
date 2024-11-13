using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 c;
    [SerializeField]
    private Transform fpvTransofrm;
    private Vector3 fpvPosition;


    private bool fpv = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c = this.transform.position - player.transform.position;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            fpv = !fpv;
            if (!fpv)
            {
                this.transform.position = fpvTransofrm.position;
                this.transform.rotation = fpvTransofrm.rotation;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (fpv)
        {
            this.transform.position = c + player.transform.position;
        } 
    }
}
