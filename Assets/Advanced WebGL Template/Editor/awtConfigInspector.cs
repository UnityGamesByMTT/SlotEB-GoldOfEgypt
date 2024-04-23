using UnityEngine;
using UnityEditor;
using AWT.utilities;

[CustomEditor(typeof(awtConfig))]
[CanEditMultipleObjects]
public class awtConfigInspector : Editor
{
  public awtConfig configData;

  public static serializer aspectRatio;
  public static serializer fixedAspectRatioWidth;
  public static serializer fixedAspectRatioHeight;
  public static serializer maxAspectRatioWidth;
  public static serializer maxAspectRatioHeight;
  public static serializer minAspectRatioWidth;
  public static serializer minAspectRatioHeight;
  public static serializer startToLoadButton;
  public static serializer startToLoadButtonText;
  public static serializer showAppStoreButton;
  public static serializer appStoreButtonLink;
  public static serializer showMicrosoftStoreButton;
  public static serializer microsoftStoreButtonLink;
  public static serializer showPlayStoreButton;
  public static serializer playStoreButtonLink;
  public static serializer showDescription;
  public static serializer descriptionText;
  public static serializer showFacebookButton;
  public static serializer facebookButtonLink;
  public static serializer showTwitterButton;
  public static serializer twitterButtonLink;
  public static serializer showGoogleButton;
  public static serializer googleButtonLink;
  public static serializer showLinkedinButton;
  public static serializer linkedinButtonLink;
  public static serializer showYoutubeButton;
  public static serializer youtubeButtonLink;
  public static serializer showPrivacyPolicy;
  public static serializer privacyPolicyLink;
  public static serializer showTermsOfService;
  public static serializer termsOfServiceLink;
  public static serializer showCopyright;
  public static serializer copyrightLink;
  public static serializer copyrightText;
  public static serializer showGameLogo;
  public static serializer gameLogoSize;
  public static serializer showCompanyLogo;
  public static serializer companyLogoSize;
  public static serializer companyLogoLink;
  public static serializer showProgressBar;
  public static serializer showProgressText;
  public static serializer showFullscreenButton;
  public static serializer showWatchVideoButton;
  public static serializer showPlayPongButton;
  public static serializer watchVideoButtonText;
  public static serializer playPongButtonText;
  public static serializer watchVideoButtonAutoSize;
  public static serializer playPongButtonAutoSize;
  public static serializer watchVideoButtonFontSize;
  public static serializer playPongButtonFontSize;
  public static serializer watchVideoButtonYoutubeLink;
  public static serializer disableMobileWarning;
  public static serializer targetPath;
  public static serializer s11;
  public static serializer s21;
  public static serializer s31;

  static bool showOptions0 = false;
  static bool showOptions1 = false;
  static bool showOptions2 = false;
  static bool showOptions3 = false;
  static bool showOptions4;
  static bool socialMedialinksOnOffBool;
  static bool storeBadgesSettingsOnOffBool;

  public GUIStyle gUIStyle0;
  static bool bool0;
  static bool bool1;

  public GUIStyle GUIStyle0
  {
    get
    {
      return gUIStyle0;
    }
    set
    {
      //if (gUIStyle0 != null)
      //  return;

      GUISkin GUISkin0 = Resources.Load<GUISkin>(Application.dataPath + "\\GraphSkin.guiskin") as GUISkin;
      gUIStyle0 = GUISkin0.button;
    }
  }

  void OnEnable()
  {
    configData = (awtConfig)(serializedObject.targetObject);
    createSerializers();
  }

  private serializer createSerializer(string propertyName)
  {
    return new serializer
    (
      targetObject: configData,
      name: propertyName,
      memberType: serializer.MemberType.property
    );
  }

  private void createSerializers()
  {
    aspectRatio = createSerializer("aspectRatio");
    fixedAspectRatioWidth = createSerializer("fixedAspectRatioWidth");
    fixedAspectRatioHeight = createSerializer("fixedAspectRatioHeight");
    maxAspectRatioWidth = createSerializer("maxAspectRatioWidth");
    maxAspectRatioHeight = createSerializer("maxAspectRatioHeight");
    minAspectRatioWidth = createSerializer("minAspectRatioWidth");
    minAspectRatioHeight = createSerializer("minAspectRatioHeight");
    startToLoadButton = createSerializer("startToLoadButton");
    startToLoadButtonText = createSerializer("startToLoadButtonText");
    showAppStoreButton = createSerializer("showAppStoreButton");
    appStoreButtonLink = createSerializer("appStoreButtonLink");
    showMicrosoftStoreButton = createSerializer("showMicrosoftStoreButton");
    microsoftStoreButtonLink = createSerializer("microsoftStoreButtonLink");
    showPlayStoreButton = createSerializer("showPlayStoreButton");
    playStoreButtonLink = createSerializer("playStoreButtonLink");
    showDescription = createSerializer("showDescription");
    descriptionText = createSerializer("descriptionText");
    showFacebookButton = createSerializer("showFacebookButton");
    facebookButtonLink = createSerializer("facebookButtonLink");
    showTwitterButton = createSerializer("showTwitterButton");
    twitterButtonLink = createSerializer("twitterButtonLink");
    showGoogleButton = createSerializer("showGoogleButton");
    googleButtonLink = createSerializer("googleButtonLink");
    showLinkedinButton = createSerializer("showLinkedinButton");
    linkedinButtonLink = createSerializer("linkedinButtonLink");
    showYoutubeButton = createSerializer("showYoutubeButton");
    youtubeButtonLink = createSerializer("youtubeButtonLink");
    showPrivacyPolicy = createSerializer("showPrivacyPolicy");
    privacyPolicyLink = createSerializer("privacyPolicyLink");
    showTermsOfService = createSerializer("showTermsOfService");
    termsOfServiceLink = createSerializer("termsOfServiceLink");
    showCopyright = createSerializer("showCopyright");
    copyrightLink = createSerializer("copyrightLink");
    copyrightText = createSerializer("copyrightText");
    showGameLogo = createSerializer("showGameLogo");
    gameLogoSize = createSerializer("gameLogoSize");
    showCompanyLogo = createSerializer("showCompanyLogo");
    companyLogoSize = createSerializer("companyLogoSize");
    companyLogoLink = createSerializer("companyLogoLink");
    showProgressBar = createSerializer("showProgressBar");
    showProgressText = createSerializer("showProgressText");
    showFullscreenButton = createSerializer("showFullscreenButton");
    showWatchVideoButton = createSerializer("showWatchVideoButton");
    showPlayPongButton = createSerializer("showPlayPongButton");
    watchVideoButtonText = createSerializer("watchVideoButtonText");
    playPongButtonText = createSerializer("playPongButtonText");
    watchVideoButtonAutoSize = createSerializer("watchVideoButtonAutoSize");
    playPongButtonAutoSize = createSerializer("playPongButtonAutoSize");
    watchVideoButtonFontSize = createSerializer("watchVideoButtonFontSize");
    playPongButtonFontSize = createSerializer("playPongButtonFontSize");
    watchVideoButtonYoutubeLink = createSerializer("watchVideoButtonYoutubeLink");
    disableMobileWarning = createSerializer("disableMobileWarning");
    targetPath = createSerializer("targetPath");
    s11 = createSerializer("s11");
    s21 = createSerializer("s21");
    s31 = createSerializer("s31");
  }

  public override void OnInspectorGUI()
  {
    if (EditorApplication.isPlaying)
      return;
    else
      run();
  }

  private void run()
  {
    logo(out float float0);

    customInspector.buttonToInvokeMethod1("HELP", typeof(awtConfigInspector), "help");

    GUILayout.Space(float0);

    customInspector.separatorLine(5, Color.grey, 0, 30);

    showOptions2 = EditorGUILayout.BeginFoldoutHeaderGroup(showOptions2, "aspectRatio Settings", null);
    if (showOptions2)
    {
      GUILayout.Space(5);
      customInspector.inspectField.enumField_Style0(awtConfigInspector.aspectRatio, "aspectRatio", "");
      switch ((int)aspectRatio.value)
      {
        case 0:
          GUILayout.Space(10);
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.fixedAspectRatioWidth, "fixedAspectRatioWidth", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.fixedAspectRatioHeight, "fixedAspectRatioHeight", "");
          break;
        case 1:
          GUILayout.Space(10);
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.maxAspectRatioWidth, "maxAspectRatioWidth", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.maxAspectRatioHeight, "maxAspectRatioHeight", "");
          break;
        case 2:
          GUILayout.Space(10);
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.minAspectRatioWidth, "minAspectRatioWidth", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.minAspectRatioHeight, "minAspectRatioHeight", "");
          break;
        case 3:
          GUILayout.Space(10);
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.maxAspectRatioWidth, "maxAspectRatioWidth", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.maxAspectRatioHeight, "maxAspectRatioHeight", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.minAspectRatioWidth, "minAspectRatioWidth", "");
          customInspector.inspectField.floatIntField_Style0(awtConfigInspector.minAspectRatioHeight, "minAspectRatioHeight", "");
          break;
        case 4:
          GUILayout.Space(10);
          break;
        default:
          break;
      }
    }
    EditorGUILayout.EndFoldoutHeaderGroup();

    customInspector.separatorLine(5, Color.grey, 0, 30);

    showOptions3 = EditorGUILayout.BeginFoldoutHeaderGroup(showOptions3, "UI Elements Settings", null);
    if (showOptions3)
    {
      GUISkin GUISkin0 = AssetDatabase.LoadAssetAtPath("Assets/GraphSkin.guiskin", typeof(GUISkin)) as GUISkin;
      gUIStyle0 = GUISkin0.toggle;
      GUILayout.Space(15);

      Rect Rect0 = GUILayoutUtility.GetRect(0, -5);

      GUILayout.BeginHorizontal();

      if (socialMedialinksOnOffBool == true)
      {
        gUIStyle0.normal.background = gUIStyle0.onFocused.background;
      }
      else
      {
        gUIStyle0.normal.background = gUIStyle0.onHover.background;
      }


      GUI.Box(Rect0, "asdf", gUIStyle0);
      GUILayout.Space(30);
      if (GUILayout.Button("Social Media", GUISkin0.box))
      {
        if (socialMedialinksOnOffBool == true)
        {
          socialMedialinksOnOffBool = false;
        }
        else
        {
          socialMedialinksOnOffBool = true;
        }
      }
      GUILayout.EndHorizontal();

      if (socialMedialinksOnOffBool)
      {
        GUILayout.Space(15);

        customInspector.inspectField.boolField_Style0(showFacebookButton, "showFacebookButton", "");
        if ((bool)showFacebookButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(facebookButtonLink, "facebookButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showTwitterButton, "showTwitterButton", "");
        if ((bool)showTwitterButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(twitterButtonLink, "twitterButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showGoogleButton, "showGoogleButton", "");
        if ((bool)showGoogleButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(googleButtonLink, "googleButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showLinkedinButton, "showLinkedinButton", "");
        if ((bool)showLinkedinButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(linkedinButtonLink, "linkedinButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showYoutubeButton, "showYoutubeButton", "");
        if ((bool)showYoutubeButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(youtubeButtonLink, "youtubeButtonLink", "");
        }
      }

      GUILayout.Space(15);

      ////////////////////////////////////////////////////////////

      GUILayout.Space(15);

      Rect0 = GUILayoutUtility.GetRect(0, -5);

      GUILayout.BeginHorizontal();


      if (storeBadgesSettingsOnOffBool == true)
      {
        gUIStyle0.normal.background = gUIStyle0.onFocused.background;
      }
      else
      {
        gUIStyle0.normal.background = gUIStyle0.onHover.background;
      }


      GUI.Box(Rect0, "asdf", gUIStyle0);
      GUILayout.Space(30);
      if (GUILayout.Button("Store Badges", GUISkin0.box))
      {
        if (storeBadgesSettingsOnOffBool == true)
        {
          storeBadgesSettingsOnOffBool = false;
        }
        else
        {
          storeBadgesSettingsOnOffBool = true;
        }
      }
      GUILayout.EndHorizontal();

      ////////////////////////////////////////////////////////////


      GUILayout.Space(15);

      if (storeBadgesSettingsOnOffBool)
      {
        customInspector.inspectField.boolField_Style0(awtConfigInspector.showAppStoreButton, "showAppStoreButton", "");
        if ((bool)showAppStoreButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(awtConfigInspector.appStoreButtonLink, "appStoreButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(awtConfigInspector.showMicrosoftStoreButton, "showMicrosoftStoreButton", "");
        if ((bool)showAppStoreButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(awtConfigInspector.microsoftStoreButtonLink, "microsoftStoreButtonLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showPlayStoreButton, "showPlayStoreButton", "");
        if ((bool)showPlayStoreButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(playStoreButtonLink, "appStoreButtonLink", "");
        }
      }

      ////////////////////////////////////////////////////////////

      GUILayout.Space(15);

      Rect0 = GUILayoutUtility.GetRect(0, -5);

      GUILayout.BeginHorizontal();


      if (bool0 == true)
      {
        gUIStyle0.normal.background = gUIStyle0.onFocused.background;
      }
      else
      {
        gUIStyle0.normal.background = gUIStyle0.onHover.background;
      }


      GUI.Box(Rect0, "asdf", gUIStyle0);
      GUILayout.Space(30);
      if (GUILayout.Button("Footers", GUISkin0.box))
      {
        if (bool0 == true)
        {
          bool0 = false;
        }
        else
        {
          bool0 = true;
        }
      }
      GUILayout.EndHorizontal();

      ////////////////////////////////////////////////////////////


      if (bool0)
      {
        GUILayout.Space(15);

        customInspector.inspectField.boolField_Style0(showPrivacyPolicy, "showPrivacyPolicy", "");
        if ((bool)showPrivacyPolicy.value == true)
        {
          customInspector.inspectField.stringField_Style0(privacyPolicyLink, "privacyPolicyLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showTermsOfService, "showTermsOfService", "");
        if ((bool)showPrivacyPolicy.value == true)
        {
          customInspector.inspectField.stringField_Style0(termsOfServiceLink, "termsOfServiceLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showCopyright, "showCopyright", "");
        if ((bool)showCopyright.value == true)
        {
          customInspector.inspectField.stringField_Style0(copyrightLink, "copyrightLink", "");
          customInspector.inspectField.stringField_Style0(copyrightText, "copyrightText", "");
        }
      }

      GUILayout.Space(15);

      ////////////////////////////////////////////////////////////

      GUILayout.Space(15);

      Rect0 = GUILayoutUtility.GetRect(0, -5);

      GUILayout.BeginHorizontal();


      if (bool1 == true)
      {
        gUIStyle0.normal.background = gUIStyle0.onFocused.background;
      }
      else
      {
        gUIStyle0.normal.background = gUIStyle0.onHover.background;
      }


      GUI.Box(Rect0, "asdf", gUIStyle0);
      GUILayout.Space(30);
      if (GUILayout.Button("Others", GUISkin0.box))
      {
        if (bool1 == true)
        {
          bool1 = false;
        }
        else
        {
          bool1 = true;
        }
      }
      GUILayout.EndHorizontal();

      ////////////////////////////////////////////////////////////

      if (bool1)
      {
        GUILayout.Space(15);

        customInspector.inspectField.boolField_Style0(awtConfigInspector.startToLoadButton, "startToLoadButton", "");
        if ((bool)startToLoadButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(awtConfigInspector.startToLoadButtonText, "startToLoadButtonText", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showDescription, "showDescription", "");
        if ((bool)showDescription.value == true)
        {
          customInspector.inspectField.stringField_Style0(descriptionText, "descriptionText", "");
        }

        GUILayout.Space(15);

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showGameLogo, "showGameLogo", "");
        if ((bool)showGameLogo.value == true)
        {
          customInspector.inspectField.stringField_Style0(gameLogoSize, "gameLogoSize", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showCompanyLogo, "showCompanyLogo", "");
        if ((bool)showCompanyLogo.value == true)
        {
          customInspector.inspectField.stringField_Style0(companyLogoSize, "companyLogoSize", "");
          customInspector.inspectField.stringField_Style0(companyLogoLink, "companyLogoLink", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showProgressBar, "showProgressBar", "");
        customInspector.inspectField.boolField_Style0(showProgressText, "showProgressText", "");

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showFullscreenButton, "showFullscreenButton", "");

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showWatchVideoButton, "showWatchVideoButton", "");
        if ((bool)showWatchVideoButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(watchVideoButtonText, "watchVideoButtonText", "");
          customInspector.inspectField.stringField_Style0(watchVideoButtonFontSize, "watchVideoButtonFontSize", "");
          customInspector.inspectField.boolField_Style0(watchVideoButtonAutoSize, "watchVideoButtonAutoSize", "");
        }

        GUILayout.Space(15);
        customInspector.inspectField.boolField_Style0(showPlayPongButton, "showPlayPongButton", "");
        if ((bool)showPlayPongButton.value == true)
        {
          customInspector.inspectField.stringField_Style0(playPongButtonText, "playPongButtonText", "");
          customInspector.inspectField.stringField_Style0(playPongButtonFontSize, "playPongButtonFontSize", "");
          customInspector.inspectField.boolField_Style0(playPongButtonAutoSize, "playPongButtonAutoSize", "");
        }

        GUILayout.Space(15);

        customInspector.inspectField.stringField_Style0(watchVideoButtonYoutubeLink, "watchVideoButtonYoutubeLink", "");

        GUILayout.Space(15);

        customInspector.inspectField.boolField_Style0(disableMobileWarning, "disableMobileWarning", "");
      }
    }

    EditorGUILayout.EndFoldoutHeaderGroup();

    GUILayout.Space(30);

    customInspector.buttonToInvokeMethod("applyChangesToLastBuild", typeof(awt_editor), "applyChangesToLastBuild");
  }

  public static void logo(out float float0)
  {
    Texture2D logo = Resources.Load<Texture2D>("logo");

    float width = (Screen.height / 2160f) * 200;
    float height = (Screen.height / 2160f) * 200;
    float x = (EditorGUIUtility.currentViewWidth / 2) - (width / 2);

    GUI.color = Color.white;
    GUI.DrawTexture(new Rect(x, 0, width, height), logo);

    float0 = width / 1.3f;
  }

  public static void help()
  {
    var filePath = utilities0.findFilePath(Application.dataPath, "awtDocumentation.pdf");
    System.Diagnostics.Process.Start(filePath);
  }
}