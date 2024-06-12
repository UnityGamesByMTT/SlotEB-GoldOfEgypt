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
    [SerializeField] private Button AutoSpinStop_Button;

    [Header("Animated Sprites")]
    [SerializeField]
    private Sprite[] Wick_Sprite;
    [SerializeField]
    private Sprite[] Shen_Sprite;
    [SerializeField]
    private Sprite[] Eye_Sprite;
    [SerializeField]
    private Sprite[] Ankh_Sprite;
    [SerializeField]
    private Sprite[] Lotus_Sprite;
    [SerializeField]
    private Sprite[] A_Sprite;
    [SerializeField]
    private Sprite[] K_Sprite;
    [SerializeField]
    private Sprite[] J_Sprite;
    [SerializeField]
    private Sprite[] Q_Sprite;
    [SerializeField]
    private Sprite[] Ten_Sprite;
    [SerializeField]
    private Sprite[] Wild2_Sprite;
    [SerializeField]
    private Sprite[] Scatter_Sprite;
    [SerializeField]
    private Sprite[] Jackpot_Sprite;
    [SerializeField]
    private Sprite[] FreeSpin_Sprite;
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


    int tweenHeight = 0;

    [SerializeField]
    private GameObject Image_Prefab;

    [SerializeField]
    private PayoutCalculation PayCalculator;


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

    private Coroutine AutoSpinRoutine = null;
    private Coroutine tweenroutine = null;
    private Coroutine FreeSpinRoutine = null;
    public bool IsSpinning = false;
    private bool IsFreeSpin = false;
    public bool IsAutoSpin = false;
    bool SlotRunning = false;
    internal bool CheckPopups = false;
    private int BetCounter = 0;
    private int LineCounter = 0;
    [SerializeField]
    private UIManager uiManager;

    [Header("Audio Management")]
    [SerializeField]
    private AudioController audioController;

    private List<Tweener> alltweens = new List<Tweener>();

    private void Start()
    {

        if (SlotStart_Button) SlotStart_Button.onClick.RemoveAllListeners();
        if (SlotStart_Button) SlotStart_Button.onClick.AddListener(delegate { StartSlots(); });

        if (BetPlus_Button) BetPlus_Button.onClick.RemoveAllListeners();
        if (BetPlus_Button) BetPlus_Button.onClick.AddListener(delegate { OnBetOne(true); });
        if (BetMinus_Button) BetMinus_Button.onClick.RemoveAllListeners();
        if (BetMinus_Button) BetMinus_Button.onClick.AddListener(delegate { OnBetOne(false); });

        if (LinePlus_Button) LinePlus_Button.onClick.RemoveAllListeners();
        if (LinePlus_Button) LinePlus_Button.onClick.AddListener(delegate { ChangeLine(true); });
        if (LineMinus_Button) LineMinus_Button.onClick.RemoveAllListeners();
        if (LineMinus_Button) LineMinus_Button.onClick.AddListener(delegate { ChangeLine(false); });

        if (MaxBet_Button) MaxBet_Button.onClick.RemoveAllListeners();
        if (MaxBet_Button) MaxBet_Button.onClick.AddListener(MaxBet);

        if (AutoSpin_Button) AutoSpin_Button.onClick.RemoveAllListeners();
        if (AutoSpin_Button) AutoSpin_Button.onClick.AddListener(AutoSpin);

        if (AutoSpinStop_Button) AutoSpinStop_Button.onClick.RemoveAllListeners();
        if (AutoSpinStop_Button) AutoSpinStop_Button.onClick.AddListener(StopAutoSpin);
        numberOfSlots = 5;
    }

    private void AutoSpin()
    {
        if (!IsAutoSpin)
        {

            IsAutoSpin = true;
            if (AutoSpinStop_Button) AutoSpinStop_Button.gameObject.SetActive(true);
            if (AutoSpin_Button) AutoSpin_Button.gameObject.SetActive(false);

            if (AutoSpinRoutine != null)
            {
                StopCoroutine(AutoSpinRoutine);
                AutoSpinRoutine = null;
            }
            AutoSpinRoutine = StartCoroutine(AutoSpinCoroutine());

        }



    }

    internal void FreeSpin(int spins)
    {
        if (!IsFreeSpin)
        {

            IsFreeSpin = true;
            ToggleButtonGrp(false);

            if (FreeSpinRoutine != null)
            {
                StopCoroutine(FreeSpinRoutine);
                FreeSpinRoutine = null;
            }
            FreeSpinRoutine = StartCoroutine(FreeSpinCoroutine(spins));

        }
    }

    private void StopAutoSpin()
    {
        if (IsAutoSpin)
        {
            IsAutoSpin = false;
            if (AutoSpinStop_Button) AutoSpinStop_Button.gameObject.SetActive(false);
            if (AutoSpin_Button) AutoSpin_Button.gameObject.SetActive(true);
            StartCoroutine(StopAutoSpinCoroutine());
        }

    }

    private IEnumerator AutoSpinCoroutine()
    {

        while (IsAutoSpin)
        {
            StartSlots(IsAutoSpin);
            yield return tweenroutine;


        }
    }

    private IEnumerator FreeSpinCoroutine(int spinchances)
    {
        int i = 0;
        while (i < spinchances)
        {
            StartSlots(IsAutoSpin);
            yield return tweenroutine;
            i++;
        }
        ToggleButtonGrp(true);
        IsFreeSpin = false;
    }

    private IEnumerator StopAutoSpinCoroutine()
    {
        yield return new WaitUntil(() => !IsSpinning);
        ToggleButtonGrp(true);
        if (AutoSpinRoutine != null || tweenroutine != null)
        {
            StopCoroutine(AutoSpinRoutine);
            StopCoroutine(tweenroutine);
            tweenroutine = null;
            AutoSpinRoutine = null;
            StopCoroutine(StopAutoSpinCoroutine());
        }
    }

    internal void FetchLines(string LineVal, int count)
    {
        y_string.Add(count + 1, LineVal);
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
        PayCalculator.GeneratePayoutLinesBackend(y_points, y_points.Count, true);
    }

    internal void DestroyStaticLine()
    {
        PayCalculator.ResetStaticLine();
    }


    private void MaxBet()
    {
        if (audioController) audioController.PlayButtonAudio();
        BetCounter = SocketManager.initialData.Bets.Count - 1;
        if (TotalBet_text) TotalBet_text.text = SocketManager.initialData.Bets[BetCounter].ToString();
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
    void OnBetOne(bool IncDec)
    {
        if (audioController) audioController.PlayButtonAudio();

        if (BetCounter < SocketManager.initialData.Bets.Count - 1)
        {
            BetCounter++;
        }
        else
        {
            BetCounter = 0;
        }
        Debug.Log("Index:" + BetCounter);

        if (TotalBet_text) TotalBet_text.text = SocketManager.initialData.Bets[BetCounter].ToString();
        //if (BetPerLine_text) BetPerLine_text.text = SocketManager.initialData.Bets[BetCounter].ToString();
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

    internal void SetInitialUI()
    {
        BetCounter = SocketManager.initialData.Bets.Count - 1;
        LineCounter = SocketManager.initialData.LinesCount.Count - 1;
        if (TotalBet_text) TotalBet_text.text = SocketManager.initialData.Bets[BetCounter].ToString();
        if (Lines_text) Lines_text.text = SocketManager.initialData.LinesCount[LineCounter].ToString();
        if (TotalWin_text) TotalWin_text.text = SocketManager.playerdata.haveWon.ToString();
        if (Balance_text) Balance_text.text = SocketManager.playerdata.Balance.ToString();
        uiManager.InitialiseUIData(SocketManager.initUIData.AbtLogo.link, SocketManager.initUIData.AbtLogo.logoSprite, SocketManager.initUIData.ToULink, SocketManager.initUIData.PopLink, SocketManager.initUIData.paylines, SocketManager.initUIData.spclSymbolTxt);
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
        GenerateMatrix(number);
    }

    private void PopulateAnimationSprites(ImageAnimation animScript, int val)
    {
        animScript.textureArray.Clear();
        animScript.textureArray.TrimExcess();
        switch (val)
        {
            case 0:
                for (int i = 0; i < Wick_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wick_Sprite[i]);
                }
                break;
            case 1:
                for (int i = 0; i < Shen_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Shen_Sprite[i]);
                }
                break;
            case 2:
                for (int i = 0; i < Eye_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Eye_Sprite[i]);
                }
                break;
            case 3:
                for (int i = 0; i < Ankh_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Ankh_Sprite[i]);
                }
                break;
            case 4:
                for (int i = 0; i < Lotus_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Lotus_Sprite[i]);
                }
                break;
            case 5:
                for (int i = 0; i < A_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(A_Sprite[i]);
                }
                break;
            case 6:
                for (int i = 0; i < K_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(K_Sprite[i]);
                }
                break;
            case 7:
                for (int i = 0; i < J_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(J_Sprite[i]);
                }
                break;
            case 8:
                for (int i = 0; i < Q_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Q_Sprite[i]);
                }
                break;
            case 9:
                for (int i = 0; i < Ten_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Ten_Sprite[i]);
                }
                break;
            case 10:
                for (int i = 0; i < Wild2_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wild2_Sprite[i]);
                }
                break;
            case 11:
                for (int i = 0; i < Scatter_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Scatter_Sprite[i]);
                }
                break;
            case 12:
                for (int i = 0; i < Jackpot_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Jackpot_Sprite[i]);
                }
                break;
            case 13:
                for (int i = 0; i < FreeSpin_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(FreeSpin_Sprite[i]);
                }
                break;
            case 14:
                for (int i = 0; i < Wild1_Sprite.Length; i++)
                {
                    animScript.textureArray.Add(Wild1_Sprite[i]);
                }
                break;
        }
    }

    private void StartSlots(bool autoSpin=false)
    {
        if (!autoSpin)
        {
            if (AutoSpinRoutine != null)
            {
                StopCoroutine(AutoSpinRoutine);
                StopCoroutine(tweenroutine);
                tweenroutine = null;
                AutoSpinRoutine = null;
            }

        }

        if (audioController) audioController.PlayWLAudio("spin");

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
        PayCalculator.ResetLines();
        tweenroutine=StartCoroutine(TweenRoutine());
    }

    private IEnumerator TweenRoutine()
    {
        IsSpinning = true;
        ToggleButtonGrp(false);
        for (int i = 0; i < numberOfSlots; i++)
        {
            InitializeTweening(Slot_Transform[i]);
            yield return new WaitForSeconds(0.1f);
        }

        double bet = 0;
        double balance = 0;
        try
        {
            bet = double.Parse(TotalBet_text.text);
        }
        catch (Exception e)
        {
            Debug.Log("Error while conversion " + e.Message);
        }

        try
        {
            balance = double.Parse(Balance_text.text);
        }
        catch (Exception e)
        {
            Debug.Log("Error while conversion " + e.Message);
        }

        balance = balance - bet;

        if (Balance_text) Balance_text.text = balance.ToString();

        SocketManager.AccumulateResult(bet);

        yield return new WaitUntil(() => SocketManager.isResultdone);


        for (int j = 0; j < SocketManager.resultData.ResultReel.Count; j++)
        {
            List<int> resultnum = SocketManager.resultData.FinalResultReel[j]?.Split(',')?.Select(Int32.Parse)?.ToList();
            for (int i = 0; i < 5; i++)
            {
                if (images[i].slotImages[images[i].slotImages.Count - 5 + j]) images[i].slotImages[images[i].slotImages.Count - 5 + j].sprite = myImages[resultnum[i]];
                PopulateAnimationSprites(images[i].slotImages[images[i].slotImages.Count - 5 + j].gameObject.GetComponent<ImageAnimation>(), resultnum[i]);
            }
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < numberOfSlots; i++)
        {
            yield return StopTweening(5, Slot_Transform[i], i);
        }

        yield return new WaitForSeconds(0.3f);
        CheckPayoutLineBackend(SocketManager.resultData.linesToEmit, SocketManager.resultData.FinalsymbolsToEmit, SocketManager.resultData.jackpot);
        KillAllTweens();


        //CheckPopups = true;

        if (TotalWin_text) TotalWin_text.text = SocketManager.playerdata.haveWon.ToString();

        if (Balance_text) Balance_text.text = SocketManager.playerdata.Balance.ToString();

        if (SocketManager.resultData.jackpot > 0)
        {
            uiManager.PopulateWin(4, SocketManager.resultData.jackpot);
        }
        else if (SocketManager.resultData.WinAmout >= bet * 5 && SocketManager.resultData.WinAmout < bet * 10)
        {
            uiManager.PopulateWin(1, SocketManager.resultData.WinAmout);
        }
        else if (SocketManager.resultData.WinAmout >= bet * 10 && SocketManager.resultData.WinAmout < bet * 15)
        {
            uiManager.PopulateWin(2, SocketManager.resultData.WinAmout);
        }
        else if (SocketManager.resultData.WinAmout >= bet * 15)
        {
            uiManager.PopulateWin(3, SocketManager.resultData.WinAmout);
        }

        yield return new WaitUntil(() => !CheckPopups);
        if (!IsAutoSpin)
        {
            ToggleButtonGrp(true);
            IsSpinning = false;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            IsSpinning = false;
        }
        if (SocketManager.resultData.freeSpins > 0)
        {
            uiManager.FreeSpinProcess((int)SocketManager.resultData.freeSpins);
        }
    }

    internal void CallCloseSocket()
    {
        SocketManager.CloseSocket();
    }

    void ToggleButtonGrp(bool toggle)
    {

        if (SlotStart_Button) SlotStart_Button.interactable = toggle;
        if (AutoSpin_Button) AutoSpin_Button.interactable = toggle;
        if (LinePlus_Button) LinePlus_Button.interactable = toggle;
        if (LineMinus_Button) LineMinus_Button.interactable = toggle;
        if (BetMinus_Button) BetMinus_Button.interactable = toggle;
        if (BetPlus_Button) BetPlus_Button.interactable = toggle;
        if (MaxBet_Button) MaxBet_Button.interactable = toggle;

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
    }

    
    private void CheckPayoutLineBackend(List<int> LineId, List<string> points_AnimString, double jackpot = 0)
    {
        List<int> y_points = null;
        List<int> points_anim = null;
        if (LineId.Count > 0)
        {
            if (audioController) audioController.PlayWLAudio("win");

            for (int i = 0; i < LineId.Count; i++)
            {
                y_points = y_string[LineId[i] + 1]?.Split(',')?.Select(Int32.Parse)?.ToList();
                PayCalculator.GeneratePayoutLinesBackend(y_points, y_points.Count);
            }

            if (jackpot > 0)
            {
                for (int i = 0; i < Tempimages.Count; i++)
                {
                    for (int k = 0; k < Tempimages[i].slotImages.Count; k++)
                    {
                        StartGameAnimation(Tempimages[i].slotImages[k].gameObject);
                    }
                }
            }
            else
            {
                for (int i = 0; i < points_AnimString.Count; i++)
                {
                    points_anim = points_AnimString[i]?.Split(',')?.Select(Int32.Parse)?.ToList();

                    for (int k = 0; k < points_anim.Count; k++)
                    {
                        if (points_anim[k] >= 10)
                        {
                            StartGameAnimation(Tempimages[(points_anim[k] / 10) % 10].slotImages[points_anim[k] % 10].gameObject);
                        }
                        else
                        {
                            StartGameAnimation(Tempimages[0].slotImages[points_anim[k]].gameObject);
                        }
                    }
                }
                if (points_AnimString.Count > 0)
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
        }
        else
        {

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
    private void GenerateMatrix(int value)
    {
        for (int j = 0; j < 3; j++)
        {
            Tempimages[value].slotImages.Add(images[value].slotImages[images[value].slotImages.Count - 5 + j]);
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
        for (int i = 0; i < alltweens.Count; i++)
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

