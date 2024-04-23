using UnityEditor;
using UnityEngine;
using static AWT.utilities.utilities0;

[CreateAssetMenu]
public class awtConfig : ScriptableObject
{
  public void saveData()
  {
#if UNITY_EDITOR
    if (!EditorApplication.isPlaying)
    {
      EditorUtility.SetDirty(this);
      AssetDatabase.SaveAssets();
      AssetDatabase.Refresh();
    }
#endif
  }

  public enum AspectRatio
  {
    FixedAspectRatio,
    MaxAspectRatio,
    MinAspectRatio,
    MaxMinAspectRatio,
    MaximizeToWindow
  }
  public enum DeviceOrientation
  {
    landscape,
    portrait,
    none
  }

  public AspectRatio _aspectRatio;
  public AspectRatio aspectRatio
  {
    get
    {
      return _aspectRatio;
    }
    set
    {
      _aspectRatio = value;

      saveData();
    }
  }

  public DeviceOrientation _forceToRotate;
  public DeviceOrientation forceToRotate
  {
    get
    {
      return _forceToRotate;
    }
    set
    {
      _forceToRotate = value;

      saveData();
    }
  }

  public string forceToRotateText
  {
    get { return _forceToRotateText; }
    set { _forceToRotateText = value; saveData(); }
  }

  public string maxAspectRatioWidth
  {
    get { return _maxAspectRatioWidth; }
    set { _maxAspectRatioWidth = value; saveData(); }
  }

  public string maxAspectRatioHeight
  {
    get { return _maxAspectRatioHeight; }
    set { _maxAspectRatioHeight = value; saveData(); }
  }

  public string minAspectRatioWidth
  {
    get { return _minAspectRatioWidth; }
    set { _minAspectRatioWidth = value; saveData(); }
  }

  public string minAspectRatioHeight
  {
    get { return _minAspectRatioHeight; }
    set { _minAspectRatioHeight = value; saveData(); }
  }

  public string fixedAspectRatioWidth
  {
    get { return _fixedAspectRatioWidth; }
    set { _fixedAspectRatioWidth = value; saveData(); }
  }

  public string fixedAspectRatioHeight
  {
    get { return _fixedAspectRatioHeight; }
    set { _fixedAspectRatioHeight = value; saveData(); }
  }

  public string antiAdBlockerWarningTitle
  {
    get { return _antiAdBlockerWarningTitle; }
    set { _antiAdBlockerWarningTitle = value; saveData(); }
  }

  public string antiAdBlockerWarningDescription
  {
    get { return _antiAdBlockerWarningDescription; }
    set { _antiAdBlockerWarningDescription = value; saveData(); }
  }

  public bool antiAdBlocker
  {
    get { return _antiAdBlocker; }
    set { _antiAdBlocker = value; saveData(); }
  }

  public bool startToLoadButton
  {
    get { return _startToLoadButton; }
    set { _startToLoadButton = value; saveData(); }
  }

  public string startToLoadButtonText
  {
    get { return _startToLoadButtonText; }
    set { _startToLoadButtonText = value; saveData(); }
  }

  public bool showAppStoreButton
  {
    get { return _showAppStoreButton; }
    set { _showAppStoreButton = value; saveData(); }
  }

  public bool showMicrosoftStoreButton
  {
    get { return _showMicrosoftStoreButton; }
    set { _showMicrosoftStoreButton = value; saveData(); }
  }

  public string appStoreButtonLink
  {
    get { return _appStoreButtonLink; }
    set { _appStoreButtonLink = value; saveData(); }
  }

  public string microsoftStoreButtonLink
  {
    get { return _microsoftStoreButtonLink; }
    set { _microsoftStoreButtonLink = value; saveData(); }
  }

  public bool showPlayStoreButton
  {
    get { return _showPlayStoreButton; }
    set { _showPlayStoreButton = value; saveData(); }
  }

  public string playStoreButtonLink
  {
    get { return _playStoreButtonLink; }
    set { _playStoreButtonLink = value; saveData(); }
  }

  public bool showDescription
  {
    get { return _showDescription; }
    set { _showDescription = value; saveData(); }
  }

  public string descriptionText
  {
    get { return _descriptionText; }
    set { _descriptionText = value; saveData(); }
  }

  public bool showFacebookButton
  {
    get { return _showFacebookButton; }
    set { _showFacebookButton = value; saveData(); }
  }

  public string facebookButtonLink
  {
    get { return _facebookButtonLink; }
    set { _facebookButtonLink = value; saveData(); }
  }

  public bool showTwitterButton
  {
    get { return _showTwitterButton; }
    set { _showTwitterButton = value; saveData(); }
  }

  public string twitterButtonLink
  {
    get { return _twitterButtonLink; }
    set { _twitterButtonLink = value; saveData(); }
  }

  public bool showGoogleButton
  {
    get { return _showGoogleButton; }
    set { _showGoogleButton = value; saveData(); }
  }

  public string googleButtonLink
  {
    get { return _googleButtonLink; }
    set { _googleButtonLink = value; saveData(); }
  }

  public bool showLinkedinButton
  {
    get { return _showLinkedinButton; }
    set { _showLinkedinButton = value; saveData(); }
  }

  public string linkedinButtonLink
  {
    get { return _linkedinButtonLink; }
    set { _linkedinButtonLink = value; saveData(); }
  }

  public bool showYoutubeButton
  {
    get { return _showYoutubeButton; }
    set { _showYoutubeButton = value; saveData(); }
  }

  public string youtubeButtonLink
  {
    get { return _youtubeButtonLink; }
    set { _youtubeButtonLink = value; saveData(); }
  }

  public bool showPrivacyPolicy
  {
    get { return _showPrivacyPolicy; }
    set { _showPrivacyPolicy = value; saveData(); }
  }

  public string privacyPolicyLink
  {
    get { return _privacyPolicyLink; }
    set { _privacyPolicyLink = value; saveData(); }
  }

  public bool showTermsOfService
  {
    get { return _showTermsOfService; }
    set { _showTermsOfService = value; saveData(); }
  }

  public string termsOfServiceLink
  {
    get { return _termsOfServiceLink; }
    set { _termsOfServiceLink = value; saveData(); }
  }

  public bool showCopyright
  {
    get { return _showCopyright; }
    set { _showCopyright = value; saveData(); }
  }

  public string copyrightLink
  {
    get { return _copyrightLink; }
    set { _copyrightLink = value; saveData(); }
  }

  public string copyrightText
  {
    get { return _copyrightText; }
    set { _copyrightText = value; saveData(); }
  }

  public bool showGameLogo
  {
    get { return _showGameLogo; }
    set { _showGameLogo = value; saveData(); }
  }

  public string gameLogoSize
  {
    get { return _gameLogoSize; }
    set { _gameLogoSize = value; saveData(); }
  }

  public bool showCompanyLogo
  {
    get { return _showCompanyLogo; }
    set { _showCompanyLogo = value; saveData(); }
  }

  public string companyLogoSize
  {
    get { return _companyLogoSize; }
    set { _companyLogoSize = value; saveData(); }
  }

  public string companyLogoLink
  {
    get { return _companyLogoLink; }
    set { _companyLogoLink = value; saveData(); }
  }

  public bool showProgressBar
  {
    get { return _showProgressBar; }
    set { _showProgressBar = value; saveData(); }
  }

  public bool showProgressText
  {
    get { return _showProgressText; }
    set { _showProgressText = value; saveData(); }
  }

  public bool showFullscreenButton
  {
    get { return _showFullscreenButton; }
    set { _showFullscreenButton = value; saveData(); }
  }

  public bool showWatchVideoButton
  {
    get { return _showWatchVideoButton; }
    set { _showWatchVideoButton = value; saveData(); }
  }

  public bool showPlayPongButton
  {
    get { return _showPlayPongButton; }
    set { _showPlayPongButton = value; saveData(); }
  }

  public string watchVideoButtonText
  {
    get { return _watchVideoButtonText; }
    set { _watchVideoButtonText = value; saveData(); }
  }

  public string playPongButtonText
  {
    get { return _playPongButtonText; }
    set { _playPongButtonText = value; saveData(); }
  }

  public bool watchVideoButtonAutoSize
  {
    get { return _watchVideoButtonAutoSize; }
    set { _watchVideoButtonAutoSize = value; saveData(); }
  }

  public bool playPongButtonAutoSize
  {
    get { return _playPongButtonAutoSize; }
    set { _playPongButtonAutoSize = value; saveData(); }
  }

  public string watchVideoButtonFontSize
  {
    get { return _watchVideoButtonFontSize; }
    set { _watchVideoButtonFontSize = value; saveData(); }
  }

  public string playPongButtonFontSize
  {
    get { return _playPongButtonFontSize; }
    set { _playPongButtonFontSize = value; saveData(); }
  }

  public string watchVideoButtonYoutubeLink
  {
    get { return _watchVideoButtonYoutubeLink; }
    set { _watchVideoButtonYoutubeLink = value; saveData(); }
  }

  public bool disableMobileWarning
  {
    get { return _disableMobileWarning; }
    set { _disableMobileWarning = value; saveData(); }
  }

  public string targetPath
  {
    get { return _targetPath; }
    set { _targetPath = value; saveData(); }
  }

  public string s11
  {
    get { return _s11; }
    set { _s11 = value; saveData(); }
  }

  public string s21
  {
    get { return _s21; }
    set { _s21 = value; saveData(); }
  }

  public string s31
  {
    get { return _s31; }
    set { _s31 = value; saveData(); }
  }

  public string _maxAspectRatioWidth = "1";
  public string _maxAspectRatioHeight = "1";
  public string _minAspectRatioWidth = "1";
  public string _minAspectRatioHeight = "1";
  public string _fixedAspectRatioWidth = "16";
  public string _fixedAspectRatioHeight = "9";
  public string _forceToRotateText = "Please Rotate Your Device!";
  public bool _antiAdBlocker = true;
  public string _antiAdBlockerWarningTitle = "Adblock Detected";
  public string _antiAdBlockerWarningDescription = "Your browser is using the Adblock Plugin. To continue website please disable adblock plugin.";
  public bool _startToLoadButton = true;
  public string _startToLoadButtonText = "RUN GAME";
  public bool _showAppStoreButton = true;
  public string _microsoftStoreButtonLink = "https://u3d.as/1Xr0";
  public bool _showMicrosoftStoreButton = true;
  public string _appStoreButtonLink = "https://u3d.as/1Xr0";
  public bool _showPlayStoreButton = true;
  public string _playStoreButtonLink = "https://u3d.as/1Xr0";
  public bool _showDescription = true;
  public string _descriptionText = "[GAME NAME] is a multiplayer, first-person shooter game. The game is in early development but is updated regularly with new content.";
  public bool _showFacebookButton = true;
  public string _facebookButtonLink = "https://u3d.as/1Xr0";
  public bool _showTwitterButton = true;
  public string _twitterButtonLink = "https://u3d.as/1Xr0";
  public bool _showGoogleButton = true;
  public string _googleButtonLink = "https://u3d.as/1Xr0";
  public bool _showLinkedinButton = true;
  public string _linkedinButtonLink = "https://u3d.as/1Xr0";
  public bool _showYoutubeButton = true;
  public string _youtubeButtonLink = "https://u3d.as/1Xr0";
  public bool _showPrivacyPolicy = true;
  public string _privacyPolicyLink = "https://u3d.as/1Xr0";
  public bool _showTermsOfService = true;
  public string _termsOfServiceLink = "https://u3d.as/1Xr0";
  public bool _showCopyright = true;
  public string _copyrightLink = "https://u3d.as/1Xr0";
  public string _copyrightText = "Copyright �2020 Agnosia Games";
  public bool _showGameLogo = true;
  public string _gameLogoSize = "50";
  public bool _showCompanyLogo = true;
  public string _companyLogoSize = "100";
  public string _companyLogoLink = "https://u3d.as/1Xr0";
  public bool _showProgressBar = true;
  public bool _showProgressText = true;
  public bool _showFullscreenButton = true;
  public bool _showWatchVideoButton = true;
  public bool _showPlayPongButton = true;
  public string _watchVideoButtonText = "WATCH TUTORIAL";
  public string _playPongButtonText = "PLAY PONG";
  public bool _watchVideoButtonAutoSize = false;
  public bool _playPongButtonAutoSize = false;
  public string _watchVideoButtonFontSize = "12px";
  public string _playPongButtonFontSize = "13px";
  public string _watchVideoButtonYoutubeLink = "https://www.youtube.com/watch?v=FIpZ1h7LEos";
  public bool _disableMobileWarning = true;

  public string _targetPath;
  public string _s11;
  public string _s21;
  public string _s31;
}