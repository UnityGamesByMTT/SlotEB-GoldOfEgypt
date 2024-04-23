using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;


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

    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;

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

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private GameObject[] Pages;
    [SerializeField]
    private Button Next_Button;
    [SerializeField]
    private Button Previous_Button;

    [Header("Settings Popup")]
    [SerializeField]
    private GameObject SettingsPopup_Object;
    [SerializeField]
    private Button SettingsExit_Button;
    [SerializeField] private Button SoundButton;
    [SerializeField] private Button MusicButton;
    [SerializeField] private GameObject SoundOn;
    [SerializeField] private GameObject SoundOff;
    [SerializeField] private GameObject MusicOn;
    [SerializeField] private GameObject MusicOff;

    private int pageNum = 0;

    private bool isSoundOff=false;
    private bool isMusicOff=false;

    [SerializeField] private AudioController audioController;


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

        if (Button_BG) Button_BG.SetActive(false);

        if (Pages[0]) Pages[0].SetActive(true);

        if (Next_Button) Next_Button.onClick.RemoveAllListeners();
        if (Next_Button) Next_Button.onClick.AddListener(delegate { TurnPage(true); });

        if (Previous_Button) Previous_Button.onClick.RemoveAllListeners();
        if (Previous_Button) Previous_Button.onClick.AddListener(delegate { TurnPage(true); });

        if (SoundButton) SoundButton.onClick.RemoveAllListeners();
        if (SoundButton) SoundButton.onClick.AddListener(ToggleSound);

        if (MusicButton) MusicButton.onClick.RemoveAllListeners();
        if (MusicButton) MusicButton.onClick.AddListener(ToggleMusic);

        if (SoundOn) SoundOn.SetActive(true);
        if (SoundOff) SoundOff.SetActive(false);
        if (MusicOn) MusicOn.SetActive(true);
        if (MusicOn) MusicOff.SetActive(false);

        
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
        if (audioController) audioController.PlayButtonAudio();

        if (Menu_Object) Menu_Object.SetActive(true);
        if (Exit_Object) Exit_Object.SetActive(false);
        if (About_Object) About_Object.SetActive(false);
        if (Paytable_Object) Paytable_Object.SetActive(false);
        if (Settings_Object) Settings_Object.SetActive(false);
        if (Button_BG) Button_BG.SetActive(false);
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();

        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
        if (Popup == PaytablePopup_Object)
        {
            ResetPages();
        }
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
            if (pageNum < 3)
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
                pageNum = 3;
                if (Pages[pageNum]) Pages[pageNum].SetActive(true);
            }
        }
    }

    void ToggleMusic()
    {
        isMusicOff = !isMusicOff;
        audioController.ToggleMute(isMusicOff,"bg");

        if (isMusicOff)
        {
            MusicOff.SetActive(true);
            MusicOn.SetActive(false);
        }
        else
        {

            MusicOff.SetActive(false);
            MusicOn.SetActive(true);
        }

    }

    void ToggleSound() {
        isSoundOff = !isSoundOff;

        audioController.ToggleMute(isSoundOff, "wl");
        audioController.ToggleMute(isSoundOff, "button");

        if (isSoundOff)
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
        }
        else {

            SoundOff.SetActive(false);
            SoundOn.SetActive(true);
        }

    }
}
