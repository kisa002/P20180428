using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureController : MonoBehaviour
{
    //4:3
	public int type = -1;
    public Vector3 PicturePos;
    public Quaternion PictureRot;
    public Vector3 PictureScale;

    private Vector3 SelectedPos;
    private Quaternion SelectedRot;
    private Vector3 SelectedScale;

    public SpriteRenderer FrontSprite;
    public SpriteRenderer BackSprite;

    Quaternion Q;

    private void Awake()
    {
        SelectedPos = new Vector3(35f, 20f,3.3f);
        SelectedRot = new Quaternion (90f, 0f, 0f, 1f);
        SelectedRot.eulerAngles = new Vector3(90f, 0f, 0f);
        SelectedScale = new Vector3(3.8f, 3.8f, 1f);

        Q.eulerAngles = new Vector3(90f, 0f, 0f);
    }

    /// <summary>
    /// true=옛날사진 false=최근사진
    /// </summary>
    //public void SetPictureSize(bool bOld)
    //{
    //    if(bOld)
    //    {
    //        SelectedScale = new Vector3(4.3f, 4.3f, 1f);
    //    }
    //    else
    //    {
    //        SelectedScale = new Vector3(4.3f, 4.3f, 1f);
    //    }
    //}
    private void OnMouseDown()
    {
        if (PlayerController.instance.bPictureSelected == true)
            return;
        PicturePos = transform.localPosition;
        PictureRot = transform.localRotation;
        PictureScale = transform.localScale;
        PlayerController.instance.SelectController=this;
        bZoomming = true;
        StartCoroutine(ZoomIn());
        PlayerController.instance.bPictureSelected = true;
        //PlayerController.instance.sPictureName = FrontSprite.sprite.name;
      
    }

    private void OnMouseDrag()
    {
        if (bSelected&&!bZoomming)
        {
            
            float x = Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime;
            transform.Rotate(y, 0, -x, Space.World);
        }
    }




    bool bSelected;
    bool bZoomming;
    float RotateSpeed = 100f;


    float fTIme;
    IEnumerator ZoomIn()
    {
        fTIme = 0f;
        while (bZoomming)
        {
            fTIme += Time.deltaTime / 1f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, SelectedPos, fTIme);
            transform.rotation = Quaternion.Lerp(transform.rotation, SelectedRot, fTIme);
            transform.localScale = Vector3.Lerp(transform.localScale, SelectedScale, fTIme);
            if (Vector3.Distance(transform.localPosition, SelectedPos) < 1f)
            {
                bZoomming = false;
            }

            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = SelectedPos;
        transform.rotation = SelectedRot;
        transform.localScale = SelectedScale;
        bSelected = true;
        PlayerController.instance.SetBtnEnable(true);
        Debug.Log("선택끝");
        yield return null;
    }



    Vector3 V = new Vector3(1f, 1f, 1f);
    IEnumerator ZoomOut()
    {
        fTIme = 0f;
        while (bZoomming)
        {
            fTIme += Time.deltaTime / 1f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, PicturePos, fTIme);
            transform.rotation = Quaternion.Lerp(transform.rotation, PictureRot, fTIme);
            transform.localScale = Vector3.Lerp(transform.localScale, PictureScale, fTIme);
            if (Vector3.Distance(transform.localPosition, PicturePos) < 1f)
            {
                bZoomming = false;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = PicturePos;
        transform.rotation = PictureRot;
        transform.localScale = PictureScale;
        PlayerController.instance.bPictureSelected = false;
        bSelected = true;
        Debug.Log("취소 끝");
        yield return null;
    }


    Vector3 Vtemp = new Vector3(35f, 20f, 40f);

    IEnumerator SelectPicture()
    {
        fTIme = 0f;
        Debug.Log("정답");
        while (bZoomming)
        {
            fTIme += Time.deltaTime / 5f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vtemp, fTIme);
            transform.rotation = Quaternion.Lerp(transform.rotation, Q, Time.deltaTime * 10f);
            //transform.localScale = Vector3.Lerp(transform.localScale, V, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.localPosition, Vtemp) < 1f)
            {
                bZoomming = false;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = Vtemp;
        transform.rotation = Q;
        //transform.localScale = V;
        PlayerController.instance.bPictureSelected = false;
        bSelected = true;
        Debug.Log("취소 끝");
        yield return null;
    }

    public void CancelBtn()
    {
        PlayerController.instance.SetBtnEnable(false);
        bSelected = false;
        bZoomming = true;
        StartCoroutine(ZoomOut());
    }
    public void SelectBtn()
    {
        PlayerController.instance.SetBtnEnable(false);
        if (PlayerController.instance.sSelectName == FrontSprite.sprite.name)
        {
            bSelected = false;
            bZoomming = true;
            UIManager.Instance.SetSuccessTalk();        
            StartCoroutine(SelectPicture());
        }
        else
        {
            bSelected = false;
            bZoomming = true;
            UIManager.Instance.SetFailedTalk();
            StartCoroutine(ZoomOut());
        }
    }

    //private void Update()
    //{

    //    //취소 버튼 
    //    if(Input.GetKeyDown(KeyCode.Space)&&bSelected)
    //    {
    //        bSelected = false;
    //        bZoomming = true;
    //        StartCoroutine(ZoomOut());
    //    }
    //    //선택 버튼
    //    if(Input.GetKeyDown(KeyCode.Z) && bSelected)
    //    {
    //        if(PlayerController.instance.sSelectName== FrontSprite.sprite.name)
    //        {
    //            bSelected = false;
    //            bZoomming = true;
    //            StartCoroutine(SelectPicture());
    //        }
    //        else
    //        {
    //            bSelected = false;
    //            bZoomming = true;
    //            StartCoroutine(ZoomOut());
    //        }

    //    }
    //}







}