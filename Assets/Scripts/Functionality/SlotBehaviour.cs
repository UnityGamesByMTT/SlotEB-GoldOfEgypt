using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class SlotBehaviour : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainContainer_RT;

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] myImages;

    [Header("Slot Images")]
    [SerializeField]
    private List<SlotImage> images;
    [SerializeField]
    private List<SlotImage> Tempimages;

    [Header("Slots Objects")]
    [SerializeField]
    private GameObject[] Slot_Objects;
    [Header("Slots Elements")]
    [SerializeField]
    private LayoutElement[] Slot_Elements;

    [Header("Slots Transforms")]
    [SerializeField]
    private Transform[] Slot_Transform;

    private Dictionary<int, string> x_string = new Dictionary<int, string>();
    private Dictionary<int, string> y_string = new Dictionary<int, string>();

    [Header("Buttons")]
    [SerializeField]
    private Button SlotStart_Button;

    [Header("Animated Sprites")]
    [SerializeField]
    private Sprite[] A_Sprite;
    [SerializeField]
    private Sprite[] Wild2_Sprite;
    [SerializeField]
    private Sprite[] Ankh_Sprite;
    [SerializeField]
    private Sprite[] Eye_Sprite;
    [SerializeField]
    private Sprite[] FreeSpin_Sprite;
    [SerializeField]
    private Sprite[] J_Sprite;
    [SerializeField]
    private Sprite[] JackPot_Sprite;
    [SerializeField]
    private Sprite[] K_Sprite;
    [SerializeField]
    private Sprite[] Q_Sprite;
    [SerializeField]
    private Sprite[] Scatter_Sprite;
    [SerializeField]
    private Sprite[] Lotus_Sprite;
    [SerializeField]
    private Sprite[] Shen_Sprite;
    [SerializeField]
    private Sprite[] Wick_Sprite;
    [SerializeField]
    private Sprite[] Ten_Sprite;
    [SerializeField]
    private Sprite[] Wild1_Sprite;

    [Header("Miscellaneous UI")]
    [SerializeField]
    private TMP_Text Balance_text;
    [SerializeField]
    private TMP_Text TotalBet_text;
    [SerializeField]
    private TMP_Text Lines_text;
    [SerializeField]
    private TMP_Text TotalWin_text;
    [SerializeField]
    private Button AutoSpin_Button;
    [SerializeField]
    private Button MaxBet_Button;
    [SerializeField]
    private Button BetPlus_Button;
    [SerializeField]
    private Button BetMinus_Button;
    [SerializeField]
    private Button LinePlus_Button;
    [SerializeField]
    private Button LineMinus_Button;

    int tweenHeight = 0;

    [SerializeField]
    private GameObject Image_Prefab;

    [SerializeField]
    private PayoutCalculation PayCalculator;

    private Tweener tweener1;
    private Tweener tweener2;
    private Tweener tweener3;
    private Tweener tweener4;
    private Tweener tweener5;

    [SerializeField]
    private List<ImageAnimation> TempList;

    [SerializeField]
    private int IconSizeFactor = 100;

    private int numberOfSlots = 5;

    [SerializeField]
    int verticalVisibility = 3;

    [SerializeField]
    private SocketIOManager SocketManager;
    [SerializeField]
    private FireAnim[] fireAnimCtrl;

    Coroutine FireRout = null;
    Coroutine LineRout = null;
    //Coroutine AutoSpinRoutine = null;

    [SerializeField] private AudioController audioController;

    private List<Tweener> alltweens = new List<Tweener>();

    private void Start()
    {

        if (SlotStart_Button) SlotStart_Button.onClick.RemoveAllListeners();
        if (SlotStart_Button) SlotStart_Button.onClick.AddListener(StartSlots);

        if (BetPlus_Button) BetPlus_Button.onClick.RemoveAllListeners();
        if (BetPlus_Button) BetPlus_Button.onClick.AddListener(delegate { ChangeBet(true); });
        if (BetMinus_Button) BetMinus_Button.onClick.RemoveAllListeners();
        if (BetMinus_Button) BetMinus_Button.onClick.AddListener(delegate { ChangeBet(false); });

        if (LinePlus_Button) LinePlus_Button.onClick.RemoveAllListeners();
        if (LinePlus_Button) LinePlus_Button.onClick.AddListener(delegate { ChangeLine(true); });
        if (LineMinus_Button) LineMinus_Button.onClick.RemoveAllListeners();
        if (LineMinus_Button) LineMinus_Button.onClick.AddListener(delegate { ChangeLine(false); });

        if (MaxBet_Button) MaxBet_Button.onClick.RemoveAllListeners();
        if (MaxBet_Button) MaxBet_Button.onClick.AddListener(MaxBet);

        //if (AutoSpin_Button) AutoSpin_Button.onClick.RemoveAllListeners();
        //if (AutoSpin_Button) AutoSpin_Button.onClick.AddListener(AutoSpin);
        //numberOfSlots = 5;
        //PopulateInitalSlots(numberOfSlots);
        //FetchLines();
    }

    internal void FetchLines(string x_value, string y_value, int LineID, int count)
    {
        x_string.Add(LineID, x_value);
        y_string.Add(LineID, y_value);
    }

    internal void GenerateStaticLine(TMP_Text LineID_Text)
    {
        DestroyStaticLine();
        int LineID = 1;
        try
        {
            LineID = int.Parse(LineID_Text.text);
        }
        catch (Exception e)
        {
            Debug.Log("Exception while parsing " + e.Message);
        }
        List<int> x_points = null;
        List<int> y_points = null;
        x_points = x_string[LineID]?.Split(',')?.Select(Int32.Parse)?.ToList();
        y_points = y_string[LineID]?.Split(',')?.Select(Int32.Parse)?.ToList();
        PayCalculator.GeneratePayoutLinesBackend(x_points, y_points, x_points.Count, true);
    }

    internal void DestroyStaticLine()
    {
        PayCalculator.ResetStaticLine();
    }

    bool IsAutoSpin = false;

    bool SlotRunning = false;

    private void MaxBet()
    {
        if (audioController) audioController.PlayButtonAudio();
        if (TotalBet_text) TotalBet_text.text = "99999";
    }

    private void ChangeLine(bool IncDec)
    {
        if (audioController) audioController.PlayButtonAudio();

        double currentline = 1;
        try
        {
            currentline = double.Parse(Lines_text.text);
        }
        catch (Exception e)
        {
            Debug.Log("parse error " + e);
        }
        if (IncDec)
        {
            if (currentline < 20)
            {
                currentline += 1;
            }
            else
            {
                currentline = 20;
            }

            if (currentline > 20)
            {
                currentline = 20;
            }
        }
        else
        {
            if (currentline > 1)
            {
                currentline -= 1;
            }
            else
            {
                currentline = 1;
            }

            if (currentline < 1)
            {
                currentline = 1;
            }
        }

        if (Lines_text) Lines_text.text = currentline.ToString();

    }

    private void ChangeBet(bool IncDec)
    {
        if (audioController) audioController.PlayButtonAudio();

        double currentbet = 0;
        try
        {
            currentbet = double.Parse(TotalBet_text.text);
        }
        catch (Exception e)
        {
            Debug.Log("parse error " + e);
        }
        if (IncDec)
        {
            if (currentbet < 99999)
            {
                currentbet += 100;
            }
            else
            {
                currentbet = 99999;
            }

            if (currentbet > 99999)
            {
                currentbet = 99999;
            }
        }
        else
        {
            if (currentbet > 0)
            {
                currentbet -= 100;
            }
            else
            {
                currentbet = 0;
            }

            if (currentbet < 0)
            {
                currentbet = 0;
            }
        }

        if (TotalBet_text) TotalBet_text.text = currentbet.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && SlotStart_Button.interactable)
        {
            StartSlots();
        }
    }

    internal void PopulateInitalSlots(int number, List<int> myvalues)
    {
        PopulateSlot(myvalues, number);
    }

    internal void LayoutReset(int number)
    {
        if (Slot_Elements[number]) Slot_Elements[number].ignoreLayout = true;
        if (SlotStart_Button) SlotStart_Button.interactable = true;
    }

    private void PopulateSlot(List<int> values , int number)
    {
        if (Slot_Objects[number]) Slot_Objects[number].SetActive(true);
        for(int i = 0; i<values.Count; i++)
        {
            GameObject myImg = Instantiate(Image_Prefab, Slot_Transform[number]);
            images[number].slotImages.Add(myImg.GetComponent<Image>());
            images[number].slotImages[i].sprite = myImages[values[i]];
            PopulateAnimationSprites(images[number].slotImages[i].gameObject.GetComponent<ImageAnimation>(), values[i]);
        }
        for (int k = 0; k < 2; k++)
        {
            GameObject mylastImg = Instantiate(Image_Prefab, Slot_Transform[number]);
            images[number].slotImages.Add(mylastImg.GetComponent<Image>());
            images[number].slotImages[images[number].slotImages.Count - 1].sprite = myImages[values[k]];
            PopulateAnimationSprites(images[number].slotImages[images[number].slotImages.Count - 1].gameObject.GetComponent<ImageAnimation>(), values[k]);
        }
        if (mainContainer_RT) LayoutRebuilder.ForceRebuildLayoutImmediate(mainContainer_RT);
        tweenHeight = (values.Count * IconSizeFactor) - 280;
    }

    private void PopulateAnimationSprites(ImageAnimation animScript, int val)
    {
        switch(val)
        {
            case 0:
                for (int i = 0; i < Ten_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Ten_Sprite[i]);
                }
                break;
            case 1:
                for (int i = 0; i < A_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(A_Sprite[i]);
                }
                break;
            case 2:
                for (int i = 0; i < Ankh_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Ankh_Sprite[i]);
                }
                break;
            case 3:
                for (int i = 0; i < Eye_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Eye_Sprite[i]);
                }
                break;
            case 4:
                for (int i = 0; i < FreeSpin_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(FreeSpin_Sprite[i]);
                }
                break;
            case 5:
                for (int i = 0; i < J_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(J_Sprite[i]);
                }
                break;
            case 6:
                for (int i = 0; i < JackPot_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(JackPot_Sprite[i]);
                }
                break;
            case 7:
                for (int i = 0; i < K_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(K_Sprite[i]);
                }
                break;
            case 8:
                for (int i = 0; i < Lotus_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Lotus_Sprite[i]);
                }
                break;
            case 9:
                for (int i = 0; i < Q_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Q_Sprite[i]);
                }
                break;
            case 10:
                for (int i = 0; i < Scatter_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Scatter_Sprite[i]);
                }
                break;
            case 11:
                for (int i = 0; i < Shen_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Shen_Sprite[i]);
                }
                break;
            case 12:
                for (int i = 0; i < Wick_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wick_Sprite[i]);
                }
                break;
            case 13:
                for (int i = 0; i < Wild1_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wild1_Sprite[i]);
                }
                break;
            case 14:
                for (int i = 0; i < Wild2_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wild2_Sprite[i]);
                }
                break;
        }
    }

    private void StartSlots()
    {
        if (audioController) audioController.PlayWLAudio("spin");

        if (SlotStart_Button) SlotStart_Button.interactable = false;
        if (TempList.Count > 0) 
        {
            if(FireRout !=null)
            {
                StopCoroutine(FireRout);
                FireRout = null;
            }
            fireAnimCtrl[0].StopFire();
            fireAnimCtrl[1].StopFire();
            if (LineRout != null)
            {
                StopCoroutine(LineRout);
                LineRout = null;
            }
            StopCoroutine(ManageAnimCtrl());
            StopGameAnimation();
        }
        for (int i = 0; i < Tempimages.Count; i++)
        {
            Tempimages[i].slotImages.Clear();
            Tempimages[i].slotImages.TrimExcess();
        }
        PayCalculator.ResetLines();
        StartCoroutine(TweenRoutine());
    }

    [SerializeField]
    private List<int> TempLineIds;
    [SerializeField]
    private List<string> x_animationString;
    [SerializeField]
    private List<string> y_animationString;

    private IEnumerator TweenRoutine()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            InitializeTweening(Slot_Transform[i]);
            yield return new WaitForSeconds(0.1f);
        }

        SocketManager.AccumulateResult();
        yield return new WaitForSeconds(0.5f);
        List<int> resultnum = SocketManager.tempresult.StopList?.Split(',')?.Select(Int32.Parse)?.ToList();

        for (int i = 0; i < numberOfSlots; i++)
        {
            yield return StopTweening(resultnum[i] , Slot_Transform[i], i);
        }

        yield return new WaitForSeconds(0.3f);
        GenerateMatrix(SocketManager.tempresult.StopList);
        CheckPayoutLineBackend(SocketManager.tempresult.resultLine, SocketManager.tempresult.x_animResult, SocketManager.tempresult.y_animResult);
        KillAllTweens();
        if (SlotStart_Button) SlotStart_Button.interactable = true;
    }

    private void StartGameAnimation(GameObject animObjects = null) 
    {
        if (animObjects != null)
        {
            ImageAnimation temp = animObjects.GetComponent<ImageAnimation>();
            temp.StartAnimation();
            TempList.Add(temp);
        }
        else
        {
            for (int i = 0; i < TempList.Count; i++)
            {
                TempList[i].StartAnimation();
            }
        }
    }

    private IEnumerator ManageAnimCtrl()
    {
        yield return new WaitForSeconds(6);
        StopGameAnimation(true);
        yield return new WaitForSeconds(4);
        StartGameAnimation();
        yield return new WaitForSeconds(6);
        StopGameAnimation();
    }

    private void StopGameAnimation(bool firstime = false)
    {
        for (int i = 0; i < TempList.Count; i++)
        {
            TempList[i].StopAnimation();
        }
        if (!firstime)
        {
            Debug.Log("clear my list");
            TempList.Clear();
            TempList.TrimExcess();
        }
        firstime = false;
    }

    private void CheckPayoutLineBackend(List<int> LineId, List<string> x_AnimString, List<string> y_AnimString)
    {
        List<int> x_points = null;
        List<int> y_points = null;
        List<int> x_anim = null;
        List<int> y_anim = null;

        if (LineId.Count > 0)
        {
            if (audioController) audioController.PlayWLAudio("win");

            for (int i = 0; i < LineId.Count; i++)
            {
                x_points = x_string[LineId[i]]?.Split(',')?.Select(Int32.Parse)?.ToList();
                y_points = y_string[LineId[i]]?.Split(',')?.Select(Int32.Parse)?.ToList();
                PayCalculator.GeneratePayoutLinesBackend(x_points, y_points, x_points.Count);
            }

            for (int i = 0; i < x_AnimString.Count; i++)
            {
                x_anim = x_AnimString[i]?.Split(',')?.Select(Int32.Parse)?.ToList();
                y_anim = y_AnimString[i]?.Split(',')?.Select(Int32.Parse)?.ToList();

                for (int k = 0; k < x_anim.Count; k++)
                {
                    StartGameAnimation(Tempimages[x_anim[k]].slotImages[y_anim[k]].gameObject);
                }
            }

            if (x_AnimString.Count > 0)
            {
                if (FireRout != null)
                {
                    StopCoroutine(FireRout);
                    FireRout = null;
                }
                FireRout = StartCoroutine(FireRoutine());
                if (LineRout != null)
                {
                    StopCoroutine(LineRout);
                    LineRout = null;
                }
                LineRout = StartCoroutine(ManageAnimCtrl());
            }

        }
        else {
            if (audioController) audioController.PlayWLAudio("lose");

        }

    }

    private IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(2);
        fireAnimCtrl[0].StartFire();
        fireAnimCtrl[1].StartFire();
        yield return new WaitForSeconds(2);
        fireAnimCtrl[0].StopFire();
        fireAnimCtrl[1].StopFire();
    }

    private void GenerateMatrix(string stopList)
    {
        List<int> numbers = stopList?.Split(',')?.Select(Int32.Parse)?.ToList();

        for (int i = 0; i < numbers.Count; i++)
        {
            for (int s = 0; s < verticalVisibility; s++)
            {
                Tempimages[i].slotImages.Add(images[i].slotImages[(images[i].slotImages.Count - numbers[i]) + s]);
            }
        }
    }

    #region TweeningCode
    private void InitializeTweening(Transform slotTransform)
    {
        slotTransform.localPosition = new Vector2(slotTransform.localPosition.x, 0);
        Tweener tweener = slotTransform.DOLocalMoveY(-tweenHeight, 0.2f).SetLoops(-1, LoopType.Restart).SetDelay(0);
        tweener.Play();
        alltweens.Add(tweener);
    }



    private IEnumerator StopTweening(int reqpos, Transform slotTransform, int index)
    {
        alltweens[index].Pause();
        int tweenpos = (reqpos * IconSizeFactor) - IconSizeFactor;
        alltweens[index] = slotTransform.DOLocalMoveY(-tweenpos + 100, 0.5f).SetEase(Ease.OutElastic);
        yield return new WaitForSeconds(0.2f);
    }


    private void KillAllTweens()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            alltweens[i].Kill();
        }
        alltweens.Clear();

    }
    #endregion

}

[Serializable]
public class SlotImage
{
    public List<Image> slotImages = new List<Image>(10);
}

