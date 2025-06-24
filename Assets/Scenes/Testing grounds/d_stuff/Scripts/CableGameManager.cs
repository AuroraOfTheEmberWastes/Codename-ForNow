using UnityEngine;

public class CableGameManager : MonoBehaviour
{

    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;
    public PathManager pathManager1;
    public PathManager pathManager2;
    public PathManager pathManager3;

    // Update is called once per frame
    void Update()
    {
        if (pathManager1.connectedPathsCount==3)
        {
            lv1.SetActive(false);
            lv2.SetActive(true);
        }

        

        if (pathManager2.connectedPathsCount == 3)
        {
            lv2.SetActive(false);
            lv3.SetActive(true);
        }
        
        if (pathManager3.connectedPathsCount == 3)
        {
            lv3.SetActive(false);
            //switch back to irl cam (game finished)
        }

        
    }
}
