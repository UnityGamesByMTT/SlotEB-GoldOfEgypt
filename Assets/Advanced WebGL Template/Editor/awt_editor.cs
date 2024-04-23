using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class awt_editor
{
  private static int int0;

  [PostProcessBuild]
  public static void OnPostProcessBuild(BuildTarget target, string targetPath)
  {
    string path0 = "Assets/Advanced WebGL Template/AWT Configurations.asset";

#if UNITY_EDITOR_OSX
    path0 = path0.Replace("\\", "/");
#endif

    awtConfig configData = (awtConfig)AssetDatabase.LoadAssetAtPath(path0, typeof(awtConfig));

    configData.targetPath = targetPath;

    if (target != BuildTarget.WebGL)
    {
      return;
    }

    var path3 = Path.Combine(targetPath, "awt.js");
    if (!(File.Exists(path3)))
    {
      return;
    }

    var path = Path.Combine(targetPath, "index.html");
    var indexHtml = File.ReadAllText(path);

    var path2 = Path.Combine(targetPath, "awtConfigurations.js");
    string awtConfigurationsJs = File.ReadAllText(path2);

    var path5 = Path.Combine(targetPath, "style1.css");
    string style1Css = File.ReadAllText(path5);

    configData.s11 = getBetween(indexHtml, "<!--s11-->", "<!--s12-->");
    configData.s21 = getBetween(indexHtml, "//s21//", "//s22//");
    configData.s31 = getBetween(indexHtml, "//s31//", "//s32//");

    apply(configData, ref style1Css, ref awtConfigurationsJs, ref indexHtml);

    File.WriteAllText(path, indexHtml);
    File.WriteAllText(path2, awtConfigurationsJs);
    File.WriteAllText(path5, style1Css);

    disableMobileWarning(targetPath);
  }

  public static bool checkAllFilesExists(string targetPath)
  {
    var awtJS = Path.Combine(targetPath, "awt.js");

    if (!(File.Exists(awtJS)))
      return false;
    else
      return true;
  }

  public static void apply(awtConfig configData, ref string style1Css, ref string awtConfigurationsJs, ref string indexHtml)
  {
    #region Style CSS

    if (style1Css.Contains("&&& gameLogoSize &&&") == true)
      style1Css = style1Css.Replace("&&& gameLogoSize &&&", configData.gameLogoSize);

    if (style1Css.Contains("&&& companyLogoSize &&&") == true)
      style1Css = style1Css.Replace("&&& companyLogoSize &&&", configData.companyLogoSize);

    #endregion

    #region awtConfigurationsJs

    if (string.IsNullOrEmpty(configData.maxAspectRatioWidth))
      configData.maxAspectRatioWidth = "1";

    if (string.IsNullOrEmpty(configData.maxAspectRatioHeight))
      configData.maxAspectRatioHeight = "1";

    if (string.IsNullOrEmpty(configData.minAspectRatioWidth))
      configData.minAspectRatioWidth = "1";

    if (string.IsNullOrEmpty(configData.minAspectRatioHeight))
      configData.minAspectRatioHeight = "1";

    if (string.IsNullOrEmpty(configData.fixedAspectRatioWidth))
      configData.fixedAspectRatioWidth = "1";

    if (string.IsNullOrEmpty(configData.fixedAspectRatioHeight))
      configData.fixedAspectRatioHeight = "1";

    if (awtConfigurationsJs.Contains("&&& options &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& options &&&", ((int)configData.aspectRatio).ToString());

    if (awtConfigurationsJs.Contains("&&& maxAspectRatioWidth &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& maxAspectRatioWidth &&&", configData.maxAspectRatioWidth);

    if (awtConfigurationsJs.Contains("&&& maxAspectRatioHeight &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& maxAspectRatioHeight &&&", configData.maxAspectRatioHeight);

    if (awtConfigurationsJs.Contains("&&& minAspectRatioWidth &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& minAspectRatioWidth &&&", configData.minAspectRatioWidth);

    if (awtConfigurationsJs.Contains("&&& minAspectRatioHeight &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& minAspectRatioHeight &&&", configData.minAspectRatioHeight);

    if (awtConfigurationsJs.Contains("&&& fixedAspectRatioWidth &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& fixedAspectRatioWidth &&&", configData.fixedAspectRatioWidth);

    if (awtConfigurationsJs.Contains("&&& fixedAspectRatioHeight &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& fixedAspectRatioHeight &&&", configData.fixedAspectRatioHeight);

    if (awtConfigurationsJs.Contains("&&& watchVideoButtonFontSize &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& watchVideoButtonFontSize &&&", configData.watchVideoButtonFontSize);

    if (awtConfigurationsJs.Contains("&&& playPongButtonFontSize &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& playPongButtonFontSize &&&", configData.playPongButtonFontSize);

    if (awtConfigurationsJs.Contains("&&& watchVideoButtonAutoSize &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& watchVideoButtonAutoSize &&&", configData.watchVideoButtonAutoSize.ToString().ToLower());

    if (awtConfigurationsJs.Contains("&&& playPongButtonAutoSize &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& playPongButtonAutoSize &&&", configData.playPongButtonAutoSize.ToString().ToLower());

    if (awtConfigurationsJs.Contains("&&& startToLoadButton &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& startToLoadButton &&&", configData.startToLoadButton.ToString().ToLower());

    if (awtConfigurationsJs.Contains("&&& antiAdBlocker &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& antiAdBlocker &&&", configData.antiAdBlocker.ToString().ToLower());

    if (awtConfigurationsJs.Contains("&&& forceToRotate &&&") == true)
      awtConfigurationsJs = awtConfigurationsJs.Replace("&&& forceToRotate &&&", configData.forceToRotate.ToString().ToLower());

    #endregion

    #region indexHtml

    indexHtml = boolSwitch(indexHtml, configData.showDescription, "&&& showDescription &&&", "flex", "none");
    indexHtml = indexHtml.Replace("&&& descriptionText &&&", configData.descriptionText);

    indexHtml = boolSwitch(indexHtml, configData.showPlayStoreButton, "&&& showPlayStoreButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& playStoreButtonLink &&&", configData.playStoreButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showAppStoreButton, "&&& showAppStoreButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& appStoreButtonLink &&&", configData.appStoreButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showMicrosoftStoreButton, "&&& showMicrosoftStoreButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& microsoftStoreButtonLink &&&", configData.microsoftStoreButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showFacebookButton, "&&& showFacebookButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& facebookButtonLink &&&", configData.facebookButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showTwitterButton, "&&& showTwitterButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& twitterButtonLink &&&", configData.twitterButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showGoogleButton, "&&& showGoogleButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& googleButtonLink &&&", configData.googleButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showLinkedinButton, "&&& showLinkedinButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& linkedinButtonLink &&&", configData.linkedinButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showYoutubeButton, "&&& showYoutubeButton &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& youtubeButtonLink &&&", configData.youtubeButtonLink);

    indexHtml = boolSwitch(indexHtml, configData.showGameLogo, "&&& showGameLogo &&&", "block", "none");

    indexHtml = boolSwitch(indexHtml, configData.showCompanyLogo, "&&& showCompanyLogo &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& companyLogoLink &&&", configData.companyLogoLink);

    indexHtml = boolSwitch(indexHtml, configData.showProgressBar, "&&& showProgressBar &&&", "block", "none");
    indexHtml = boolSwitch(indexHtml, configData.showProgressText, "&&& showProgressText &&&", "block", "none");
    indexHtml = boolSwitch(indexHtml, configData.showFullscreenButton, "&&& showFullscreenButton &&&", "block", "none");

    indexHtml = boolSwitch(indexHtml, configData.showPrivacyPolicy, "&&& showPrivacyPolicy &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& privacyPolicyLink &&&", configData.privacyPolicyLink);

    indexHtml = boolSwitch(indexHtml, configData.showTermsOfService, "&&& showTermsOfService &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& termsOfServiceLink &&&", configData.termsOfServiceLink);

    indexHtml = boolSwitch(indexHtml, configData.showCopyright, "&&& showCopyright &&&", "block", "none");
    indexHtml = indexHtml.Replace("&&& copyrightLink &&&", configData.copyrightLink);

    indexHtml = boolSwitch(indexHtml, configData.showWatchVideoButton, "&&& showWatchVideoButton &&&", "inline", "none");
    indexHtml = indexHtml.Replace("&&& watchVideoButtonText &&&", configData.watchVideoButtonText);

    indexHtml = boolSwitch(indexHtml, configData.showPlayPongButton, "&&& showPlayPongButton &&&", "inline", "none");
    indexHtml = indexHtml.Replace("&&& playPongButtonText &&&", configData.playPongButtonText);

    if (indexHtml.Contains("&&& copyrightText &&&") == true)
      indexHtml = indexHtml.Replace("&&& copyrightText &&&", configData.copyrightText);

    if (indexHtml.Contains("&&& startToLoadButtonText &&&") == true)
      indexHtml = indexHtml.Replace("&&& startToLoadButtonText &&&", configData.startToLoadButtonText);

    if (indexHtml.Contains("&&& forceToRotateText &&&") == true)
      indexHtml = indexHtml.Replace("&&& forceToRotateText &&&", configData.forceToRotateText);

    if (indexHtml.Contains("&&& antiAdBlockerWarningTitle &&&") == true)
      indexHtml = indexHtml.Replace("&&& antiAdBlockerWarningTitle &&&", configData.antiAdBlockerWarningTitle);

    if (indexHtml.Contains("&&& antiAdBlockerWarningDescription &&&") == true)
      indexHtml = indexHtml.Replace("&&& antiAdBlockerWarningDescription &&&", configData.antiAdBlockerWarningDescription);

    if (indexHtml.Contains("&&& watchVideoButtonYoutubeLink &&&") == true)
      indexHtml = indexHtml.Replace("&&& watchVideoButtonYoutubeLink &&&", configData.watchVideoButtonYoutubeLink.Replace("watch?v=", ""));

#if UNITY_2020_1_OR_NEWER

    indexHtml = removeBetween(indexHtml, "&&& UnityLoader1 &&&", "&&& UnityLoader2 &&&");

    indexHtml = removeBetween(indexHtml, "&&& Script1Start &&&", "&&& Script1End &&&");

    if (indexHtml.Contains("&&& Script2Start &&&") == true)
      indexHtml = indexHtml.Replace("&&& Script2Start &&&", "");

    if (indexHtml.Contains("&&& Script2End &&&") == true)
      indexHtml = indexHtml.Replace("&&& Script2End &&&", "");

    indexHtml = removeBetween(indexHtml, "&&& title2019start &&&", "&&& title2019end &&&");

    if (indexHtml.Contains("&&& title2020start &&&") == true)
      indexHtml = indexHtml.Replace("&&& title2020start &&&", "");

    if (indexHtml.Contains("&&& title2020end &&&") == true)
      indexHtml = indexHtml.Replace("&&& title2020end &&&", "");
#else
    indexHtml = removeBetween(indexHtml, "&&& Script2Start &&&", "&&& Script2End &&&");
    indexHtml = removeBetween(indexHtml, "&&& title2020start &&&", "&&& title2020end &&&");

    if (indexHtml.Contains("&&& title2019start &&&") == true)
      indexHtml = indexHtml.Replace("&&& title2019start &&&", "");

    if (indexHtml.Contains("&&& title2019end &&&") == true)
      indexHtml = indexHtml.Replace("&&& title2019end &&&", "");

    if (indexHtml.Contains("&&& Script1Start &&&") == true)
      indexHtml = indexHtml.Replace("&&& Script1Start &&&", "");

    if (indexHtml.Contains("&&& Script1End &&&") == true)
      indexHtml = indexHtml.Replace("&&& Script1End &&&", "");

    if (indexHtml.Contains("&&& UnityLoader1 &&&") == true)
      indexHtml = indexHtml.Replace("&&& UnityLoader1 &&&", "");

    if (indexHtml.Contains("&&& UnityLoader2 &&&") == true)
      indexHtml = indexHtml.Replace("&&& UnityLoader2 &&&", "");
#endif

    #endregion
  }

  public static void applyChangesToLastBuild()
  {
    string path0 = "Assets/Advanced WebGL Template/AWT Configurations.asset";

#if UNITY_EDITOR_OSX
    path0 = path0.Replace("\\", "/");
#endif

    awtConfig configData = (awtConfig)AssetDatabase.LoadAssetAtPath(path0, typeof(awtConfig));

    var targetPath = configData.targetPath;

    if (!checkAllFilesExists(targetPath))
      return;

    DirectoryInfo projectDirectory = Directory.GetParent(Application.dataPath);
    var templateFolder = projectDirectory.ToString() + "\\Assets\\WebGLTemplates\\AWT";

    var path = Path.Combine(templateFolder, "index.html");
    var indexHtml = File.ReadAllText(path);

    string s12 = getBetween(indexHtml, "<!--s11-->", "<!--s12-->");
    string s22 = getBetween(indexHtml, "//s21//", "//s22//");
    string s32 = getBetween(indexHtml, "//s31//", "//s32//");

    indexHtml = indexHtml.Replace(s12, configData.s11);
    indexHtml = indexHtml.Replace(s22, configData.s21);
    indexHtml = indexHtml.Replace(s32, configData.s31);

    var path2 = Path.Combine(templateFolder, "awtConfigurations.js");
    string awtConfigurationsJs = File.ReadAllText(path2);

    var path5 = Path.Combine(templateFolder, "style1.css");
    string style1Css = File.ReadAllText(path5);

    apply(configData, ref style1Css, ref awtConfigurationsJs, ref indexHtml);

    var buildPath = Path.Combine(targetPath, "index.html");
    var buildPath2 = Path.Combine(targetPath, "awtConfigurations.js");
    var buildPath3 = Path.Combine(targetPath, "style1.css");

    File.WriteAllText(buildPath, indexHtml);
    File.WriteAllText(buildPath2, awtConfigurationsJs);
    File.WriteAllText(buildPath3, style1Css);

    disableMobileWarning(targetPath);

        UnityEngine.Debug.Log("ALL CHANGES ARE APPLIED TO LAST BUILD!" + targetPath + " buildPath " + buildPath + " buildPath2 " + buildPath2 + " buildPath3 " + buildPath3);
    }

  public static string boolSwitch(string text, bool bool0, string textToReplace, string option0, string option1)
  {
    if (text.Contains(textToReplace) == true)
    {
      if (bool0)
      {
        text = text.Replace(textToReplace, option0);
      }
      else
      {
        text = text.Replace(textToReplace, option1);
      }
    }

    return text;
  }

  public static string removeBetween(string input, string start, string end)
  {
    int start2 = input.LastIndexOf(start) + start.Length;
    int end2 = input.IndexOf(end, start2);
    string result = input.Remove(start2, end2 - start2);

    result = result.Replace(start, "");
    result = result.Replace(end, "");

    return result;
  }

  public static string getBetween(string input, string start, string end)
  {
    int start2 = input.LastIndexOf(start) + start.Length;
    int end2 = input.IndexOf(end, start2);
    string result = input.Substring(start2, end2 - start2);

    return result;
  }

  public static void disableMobileWarning(string targetPath)
  {
#if !UNITY_2020_1_OR_NEWER

    string path4 = "Assets/Advanced WebGL Template/AWT Configurations.asset";

#if UNITY_EDITOR_OSX
            path4 = path4.Replace("\\", "/");
#endif

    awtConfig configData = (awtConfig)AssetDatabase.LoadAssetAtPath(path4, typeof(awtConfig));

    if (configData.disableMobileWarning)
    {
      var buildFolderPath = Path.Combine(targetPath, "Build");
      var info = new DirectoryInfo(buildFolderPath);
      var files = info.GetFiles("*.js");
      for (int i = 0; i < files.Length; i++)
      {
        var file = files[i];
        var filePath = file.FullName;
        var text = File.ReadAllText(filePath);
        text = text.Replace("UnityLoader.SystemInfo.mobile", "false");

        File.WriteAllText(filePath, text);
      }
    }
#endif
  }

  public static bool devMode = false;
  [UnityEditor.Callbacks.DidReloadScripts]
  static void DidReloadScripts()
  {
    EditorApplication.update += Update;
  }

  static void moveTemplates()
  {
    string assetsFolder = Application.dataPath;

    if (!assetsFolder.Contains("Assets"))
    {
      assetsFolder = assetsFolder + "\\Assets";
    }

    var path1 = assetsFolder + "\\WebGLTemplates\\AWT";
    var path3 = assetsFolder + "\\WebGLTemplates";

    //#if UNITY_EDITOR_OSX
    path1 = path1.Replace("\\", "/");
    //path2 = path2.Replace("\\", "/");
    path3 = path3.Replace("\\", "/");
    assetsFolder = assetsFolder.Replace("\\", "/");
    //#endif

    if (Directory.Exists(path1))
      return;

    string sourceDirectory0 = AWT.utilities.utilities0.findFolderPath(assetsFolder, "AWT");

    string awtTXT = Path.Combine(sourceDirectory0, "awt.txt");
    string awtConfigurationsTXT = Path.Combine(sourceDirectory0, "awtConfigurations.txt");
    string autoSizeTextMinTXT = Path.Combine(sourceDirectory0, "autoSizeText.min.txt");

    File.Move(awtTXT, Path.ChangeExtension(awtTXT, ".js"));
    File.Move(awtConfigurationsTXT, Path.ChangeExtension(awtConfigurationsTXT, ".js"));
    File.Move(autoSizeTextMinTXT, Path.ChangeExtension(autoSizeTextMinTXT, ".js"));

    if (!(Directory.Exists(path3)))
    {
      Directory.CreateDirectory(path3);
    }

    string destinationDirectory0 = assetsFolder + "\\WebGLTemplates\\AWT";

#if UNITY_EDITOR_OSX
    destinationDirectory0 = destinationDirectory0.Replace("\\", "/");
#endif

    try
    {
      Directory.Move(sourceDirectory0, destinationDirectory0);
    }
    catch (IOException exp)
    {
      UnityEngine.Debug.Log(exp.Message);
    }
  }

  static void Update()
  {
    int0++;

    if (int0 % 100 == 0)
    {
      string assetsFolder = Application.dataPath;

      if (!assetsFolder.Contains("Assets"))
      {
        assetsFolder = assetsFolder + "\\Assets";
      }

      var filePath = assetsFolder + "\\awtDeveloperMode.txt";

      if (!File.Exists(filePath))
        moveTemplates();
    }
  }
}