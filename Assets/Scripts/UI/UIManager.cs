using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;

    [SerializeField]
    private Button About_Button;
    [SerializeField]
    private GameObject About_Object;
    [SerializeField]
    private RectTransform About_RT;

    [Header("Settings UI")]
    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;
    [SerializeField]
    private Button Terms_Button;
    [SerializeField]
    private Button Privacy_Button;

    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

    [SerializeField]
    private Button Paytable_Button;
    [SerializeField]
    private GameObject Paytable_Object;
    [SerializeField]
    private RectTransform Paytable_RT;

    [SerializeField]
    private GameObject Button_BG;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;
    [SerializeField]
    private Image AboutLogo_Image;
    [SerializeField]
    private Button Support_Button;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private TMP_Text[] SymbolsText;
    [SerializeField]
    private TMP_Text[] SpecialSymbolsText;

    [Header("Settings Popup")]
    [SerializeField]
    private GameObject SettingsPopup_Object;
    [SerializeField]
    private Button SettingsExit_Button;
    [SerializeField]
    private Button Sound_Button;
    [SerializeField]
    private Button Music_Button;

    [SerializeField]
    private GameObject MusicOn_Object;
    [SerializeField]
    private GameObject MusicOff_Object;
    [SerializeField]
    private GameObject SoundOn_Object;
    [SerializeField]
    private GameObject SoundOff_Object;

    [Header("Win Popup")]
    [SerializeField]
    private Sprite BigWin_Sprite;
    [SerializeField]
    private Sprite HugeWin_Sprite;
    [SerializeField]
    private Sprite MegaWin_Sprite;
    [SerializeField]
    private Sprite Jackpot_Sprite;
    [SerializeField]
    private Image Win_Image;
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private TMP_Text Win_Text;

    [Header("FreeSpins Popup")]
    [SerializeField]
    private GameObject FreeSpinPopup_Object;
    [SerializeField]
    private TMP_Text Free_Text;
    [SerializeField]
    private Button FreeSpin_Button;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private Button GameExit_Button;

    [SerializeField]
    private SlotBehaviour slotManager;

    private bool isMusic = true;
    private bool isSound = true;

    private int FreeSpins;

    [SerializeField]
    private GameObject[] Pages;
    [SerializeField]
    private Button Next_Button;
    [SerializeField]
    private Button Previous_Button;
    private int pageNum = 0;

    private void Start()
    {

        if (Menu_Button) Menu_Button.onClick.RemoveAllListeners();
        if (Menu_Button) Menu_Button.onClick.AddListener(OpenMenu);

        if (Exit_Button) Exit_Button.onClick.RemoveAllListeners();
        if (Exit_Button) Exit_Button.onClick.AddListener(CloseMenu);

        if (About_Button) About_Button.onClick.RemoveAllListeners();
        if (About_Button) About_Button.onClick.AddListener(delegate { OpenPopup(AboutPopup_Object); });

        if (AboutExit_Button) AboutExit_Button.onClick.RemoveAllListeners();
        if (AboutExit_Button) AboutExit_Button.onClick.AddListener(delegate { ClosePopup(AboutPopup_Object); });

        if (Paytable_Button) Paytable_Button.onClick.RemoveAllListeners();
        if (Paytable_Button) Paytable_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Settings_Button) Settings_Button.onClick.RemoveAllListeners();
        if (Settings_Button) Settings_Button.onClick.AddListener(delegate { OpenPopup(SettingsPopup_Object); });

        if (SettingsExit_Button) SettingsExit_Button.onClick.RemoveAllListeners();
        if (SettingsExit_Button) SettingsExit_Button.onClick.AddListener(delegate { ClosePopup(SettingsPopup_Object); });

        if (MusicOn_Object) MusicOn_Object.SetActive(true);
        if (MusicOff_Object) MusicOff_Object.SetActive(false);

        if (SoundOn_Object) SoundOn_Object.SetActive(true);
        if (SoundOff_Object) SoundOff_Object.SetActive(false);

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);

        if (FreeSpin_Button) FreeSpin_Button.onClick.RemoveAllListeners();
        if (FreeSpin_Button) FreeSpin_Button.onClick.AddListener(delegate { StartFreeSpins(FreeSpins); });

        if (Pages[0]) Pages[0].SetActive(true);
        if (Next_Button) Next_Button.onClick.RemoveAllListeners();
        if (Next_Button) Next_Button.onClick.AddListener(delegate { TurnPage(true); });

        if (Previous_Button) Previous_Button.onClick.RemoveAllListeners();
        if (Previous_Button) Previous_Button.onClick.AddListener(delegate { TurnPage(true); });

        if (audioController) audioController.ToggleMute(false);

        isMusic = true;
        isSound = true;

        if (Sound_Button) Sound_Button.onClick.RemoveAllListeners();
        if (Sound_Button) Sound_Button.onClick.AddListener(ToggleSound);

        if (Music_Button) Music_Button.onClick.RemoveAllListeners();
        if (Music_Button) Music_Button.onClick.AddListener(ToggleMusic);

    }

    internal void PopulateWin(int value, double amount)
    {
        switch (value)
        {
            case 1:
                if (Win_Image) Win_Image.sprite = BigWin_Sprite;
                break;
            case 2:
                if (Win_Image) Win_Image.sprite = HugeWin_Sprite;
                break;
            case 3:
                if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
                break;
            case 4:
                if (Win_Image) Win_Image.sprite = Jackpot_Sprite;
                break;
        }

        StartPopupAnim(amount);
    }

    private void StartFreeSpins(int spins)
    {
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(false);
        slotManager.FreeSpin(spins);
    }

    internal void FreeSpinProcess(int spins)
    {
        FreeSpins = spins;
        if (FreeSpinPopup_Object) FreeSpinPopup_Object.SetActive(true);
        if (Free_Text) Free_Text.text = spins.ToString();
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void StartPopupAnim(double amount)
    {
        int initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString();
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(false);
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
        });
    }

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText, List<string> Specialsymbols)
    {
        if (Support_Button) Support_Button.onClick.RemoveAllListeners();
        if (Support_Button) Support_Button.onClick.AddListener(delegate { UrlButtons(SupportUrl); });

        if (Terms_Button) Terms_Button.onClick.RemoveAllListeners();
        if (Terms_Button) Terms_Button.onClick.AddListener(delegate { UrlButtons(TermsUrl); });

        if (Privacy_Button) Privacy_Button.onClick.RemoveAllListeners();
        if (Privacy_Button) Privacy_Button.onClick.AddListener(delegate { UrlButtons(PrivacyUrl); });

        StartCoroutine(DownloadImage(AbtImgUrl));
        PopulateSymbolsPayout(symbolsText);
        PopulateSpecialSymbols(Specialsymbols);
    }

    private void PopulateSpecialSymbols(List<string> Specialtext)
    {
        for (int i = 0; i < SpecialSymbolsText.Length; i++)
        {
            if (SpecialSymbolsText[i]) SpecialSymbolsText[i].text = Specialtext[i];
        }
    }

    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            string text = null;
            if (paylines.symbols[i].multiplier._5x != 0)
            {
                text += "5x - " + paylines.symbols[i].multiplier._5x;
            }
            if (paylines.symbols[i].multiplier._4x != 0)
            {
                text += "\n4x - " + paylines.symbols[i].multiplier._4x;
            }
            if (paylines.symbols[i].multiplier._3x != 0)
            {
                text += "\n3x - " + paylines.symbols[i].multiplier._3x;
            }
            if (paylines.symbols[i].multiplier._2x != 0)
            {
                text += "\n2x - " + paylines.symbols[i].multiplier._2x;
            }
            if (SymbolsText[i]) SymbolsText[i].text = text;
        }
    }

    private void CallOnExitFunction()
    {
        slotManager.CallCloseSocket();
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    private void OpenMenu()
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Menu_Object) Menu_Object.SetActive(false);
        if (Exit_Object) Exit_Object.SetActive(true);
        if (About_Object) About_Object.SetActive(true);
        if (Paytable_Object) Paytable_Object.SetActive(true);
        if (Settings_Object) Settings_Object.SetActive(true);
        if (Button_BG) Button_BG.SetActive(true);
    }

    private void CloseMenu()
    {
        DOVirtual.DelayedCall(0.1f, () =>
        {
            if (Menu_Object) Menu_Object.SetActive(true);
            if (Exit_Object) Exit_Object.SetActive(false);
            if (About_Object) About_Object.SetActive(false);
            if (Paytable_Object) Paytable_Object.SetActive(false);
            if (Settings_Object) Settings_Object.SetActive(false);
        });
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    private void ResetPages()
    {
        pageNum = 0;
        foreach (GameObject pages in Pages)
        {
            pages.SetActive(false);
        }
        Pages[0].SetActive(true);
    }

    private void TurnPage(bool type)
    {
        if (audioController) audioController.PlayButtonAudio();

        if (type)
        {
            if (pageNum < 4)
            {
                if (Pages[pageNum]) Pages[pageNum].SetActive(false);
                pageNum++;
                if (Pages[pageNum]) Pages[pageNum].SetActive(true);
            }
            else
            {
                if (Pages[pageNum]) Pages[pageNum].SetActive(false);
                pageNum = 0;
                if (Pages[pageNum]) Pages[pageNum].SetActive(true);
            }
        }
        else
        {
            if (pageNum > 0)
            {
                if (Pages[pageNum]) Pages[pageNum].SetActive(false);
                pageNum--;
                if (Pages[pageNum]) Pages[pageNum].SetActive(true);
            }
            else
            {
                if (Pages[pageNum]) Pages[pageNum].SetActive(false);
                pageNum = 4;
                if (Pages[pageNum]) Pages[pageNum].SetActive(true);
            }
        }
    }

    private void ToggleMusic()
    {
        isMusic = !isMusic;
        if (isMusic)
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(true);
            if (MusicOff_Object) MusicOff_Object.SetActive(false);
            audioController.ToggleMute(false, "bg");
        }
        else
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(false);
            if (MusicOff_Object) MusicOff_Object.SetActive(true);
            audioController.ToggleMute(true, "bg");
        }
    }

    private void UrlButtons(string url)
    {
        Application.OpenURL(url);
    }

    private void ToggleSound()
    {
        isSound = !isSound;
        if (isSound)
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(true);
            if (SoundOff_Object) SoundOff_Object.SetActive(false);
            if (audioController) audioController.ToggleMute(false, "button");
            if (audioController) audioController.ToggleMute(false, "wl");
        }
        else
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(false);
            if (SoundOff_Object) SoundOff_Object.SetActive(true);
            if (audioController) audioController.ToggleMute(true, "button");
            if (audioController) audioController.ToggleMute(true, "wl");
        }
    }

    private IEnumerator DownloadImage(string url)
    {
        // Create a UnityWebRequest object to download the image
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // Wait for the download to complete
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Apply the sprite to the target image
            AboutLogo_Image.sprite = sprite;
        }
        else
        {
            Debug.LogError("Error downloading image: " + request.error);
        }
    }
}
