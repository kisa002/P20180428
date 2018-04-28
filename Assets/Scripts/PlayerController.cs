using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject PictureObj;
    //public Canvas Canvas;
    public GameObject UiBtnObj;
    public PictureController SelectController;
    public Transform[] StartPos;

    public bool bPictureSelected;
    //public string sPictureName;
    private void Awake()
    {
        instance = this;
        SelectController = new PictureController();
        StartCoroutine(Init());


        Obj = new PictureController[10];
        for (int i = 0; i < 10; i++)
        {
            Obj[i] = Instantiate(PictureObj, transform).GetComponent< PictureController>();
        }
        ////스케일 로테이션 세팅 추가
        //GameObject obj = Instantiate(PictureObj);
        //obj.transform.position = new Vector3(-30f, 0f,0f);
        //obj.transform.localScale = new Vector3(1f, 1f, 1f);
        ////obj.transform.GetComponent<PictureController>().SetPictureSize(false);
        //obj.transform.GetComponent<PictureController>().FrontSprite.sprite = GetSprite("testf001");
        //obj.transform.GetComponent<PictureController>().BackSprite.sprite = GetSprite("testb001");

        //GameObject obj2 = Instantiate(PictureObj);
        //obj2.transform.position = new Vector3(30f, 0f,0f);
        //obj2.transform.localScale = new Vector3(1f, 1f, 1f);
        ////obj2.transform.GetComponent<PictureController>().SetPictureSize(false);
        //obj2.transform.GetComponent<PictureController>().FrontSprite.sprite = GetSprite("testf101");
        //obj2.transform.GetComponent<PictureController>().BackSprite.sprite = GetSprite("testb101");
        StartChapter(1);

    }
    PictureController[]Obj;

    public string sSelectName;
    public void StartChapter(int nChapter)
    {
        switch (nChapter)
        {
            case 1:
                sSelectName = "testf101";
                for (int i = 0; i < 8; i++)
                {

                    Obj[i].transform.position = StartPos[i].position;
                    Obj[i].transform.localScale = new Vector3(1f, 1f, 1f);
                    Obj[i].transform.rotation = StartPos[i].rotation;
                    //Obj[i].transform.GetComponent<PictureController>().SetPictureSize(false);
                    Obj[i].transform.GetComponent<PictureController>().FrontSprite.sprite = GetSprite("testf001");
                    Obj[i].transform.GetComponent<PictureController>().BackSprite.sprite = GetSprite("testb001");

                }

                Obj[7].transform.position = StartPos[7].position;
                Obj[7].transform.localScale = new Vector3(1f, 1f, 1f);
                Obj[7].transform.rotation = StartPos[7].rotation;
                //Obj[i].transform.GetComponent<PictureController>().SetPictureSize(false);
                Obj[7].transform.GetComponent<PictureController>().FrontSprite.sprite = GetSprite("testf101");
                Obj[7].transform.GetComponent<PictureController>().BackSprite.sprite = GetSprite("testb101");
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            default:
                break;
        }

    }


    //private Sprite[] arSprite;

    Dictionary<string, Sprite> DcSprite;
    int nSpriteCount;

    IEnumerator Init()
    {
        Sprite[] arObj = Resources.LoadAll<Sprite>("Sprites");
        nSpriteCount = arObj.Length;

        DcSprite = new Dictionary<string, Sprite>();
        //arSprite = new Sprite[nSpriteCount];

        for (int i = 0; i < nSpriteCount; i++)
        {
            Sprite t1 = (Sprite)arObj[i];
            DcSprite.Add(t1.name, t1);
        }

  
        yield return null;
    }
    Sprite GetSprite(string SpriteName)
    {
        //return DcSprite[SpriteName];
        if (DcSprite.ContainsKey(SpriteName))
        {
            return DcSprite[SpriteName];
        }
        else
        {
            Debug.Log("이미지가 없습니다");
            return DcSprite["b000"];
        }

        //return null;

    }


    public void SetBtnEnable(bool bActive)
    {
        if (bActive)
            UiBtnObj.SetActive(true);
        else
            UiBtnObj.SetActive(false);
    }

    public void SelectBtn()
    {
        SelectController.SelectBtn();
    }
    public void CancleBtn()
    {
        SelectController.CancelBtn();
    }


}
