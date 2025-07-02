using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CableGameManager : MonoBehaviour
{

    public GameObject lv1;
    public GameObject lv2;
    public GameObject lv3;
    public PathManager pathManager1;
    public PathManager pathManager2;
    public PathManager pathManager3;
    public List<GameObject> enableObj;
    public List<GameObject> disableObj;
    public InteractionObject interactionObject;

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
            StartCoroutine(endingGame());
        }
    }

    public IEnumerator endingGame()
    {
        yield return new WaitForSeconds(1f);
        interactionObject.OnEventTrigger();
        foreach (GameObject obj in enableObj)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in disableObj)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }


}
