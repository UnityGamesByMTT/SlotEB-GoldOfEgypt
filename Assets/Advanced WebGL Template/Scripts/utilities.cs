using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

namespace AWT
{
  namespace utilities
  {
    [CreateAssetMenu]
    public static class utilities0
    {
      public delegate Coroutine delegate0(IEnumerator IEnumerator0);
      public static delegate0 StartCoroutine;

      public static MonoBehaviour sourceMono;

      public delegate bool delegate1();

      static utilities0()
      {
        counterInstance_L0 = new List<counterInstance>();
        checkSource();
      }

      public static void checkSource()
      {
        if (sourceMono == null)
        {
          sourceMono = UnityEngine.Object.FindObjectOfType<MonoBehaviour>();
          if (sourceMono != null)
            StartCoroutine = sourceMono.StartCoroutine;
        }
      }

      public static void waitUntil(delegate1 action0, Action action01)
      {
        checkSource();

        StartCoroutine(waitUntilC(action0, action01));
      }

      public static void runAfterXFrame(int X, Action action0)
      {
        checkSource();

        StartCoroutine(runAfterXFrame2(X, action0));
      }

      public static void runEveryXFrame(int X, Action action0)
      {
        checkSource();

        StartCoroutine(runEveryXFrame2(X, action0));
      }

      private static IEnumerator waitUntilC(delegate1 action0, Action action01)
      {
        while (!action0())
        {
          yield return null;
        }

        action01();
      }

      private static IEnumerator runEveryXFrame2(int X, Action action0)
      {
        int int0 = 0;

        while (true)
        {
          int0++;

          if (int0 % X == 0)
          {
            action0();
          }

          yield return null;
        }
      }

      private static IEnumerator runAfterXFrame2(int X, Action action0)
      {
        int int0 = 0;

        while (int0 < X)
        {
          int0++;
          yield return null;
        }

        action0();
      }

      public static float stringToFloat(string string1)
      {
        float value = 0;

        try
        {
          value = float.Parse(string1);
        }
        catch (Exception e)
        {
          string text = "";

          if (string1.Contains(","))
          {
            text = string1.Replace(',', '.');
          }
          else if (string1.Contains("."))
          {
            text = string1.Replace('.', ',');
          }

          value = float.Parse(text);
        }

        if ((int)value == value)
        {
          string text = "";

          if (string1.Contains(","))
          {
            text = string1.Replace(',', '.');
            value = float.Parse(text);
          }
          else if (string1.Contains("."))
          {
            text = string1.Replace('.', ',');
            value = float.Parse(text);
          }
        }

        return value;
      }

      public static string ToString(object object0)
      {
        if (object0 == null)
          return "";
        else
          return object0.ToString();
      }

      public static List<counterInstance> counterInstance_L0;
      public class counterInstance
      {
        public string key;
        public int executeTime = 1;

        public counterInstance(string key)
        {
          this.key = key;
        }
      }

      public static void counter(int instanceID, string magicWord, out int executeTime)
      {
        string key = instanceID + magicWord;

        var var0 = counterInstance_L0.Find((i) => (i.key == key));
        if (var0 != null)
        {
          var0.executeTime++;
        }
        else
        {
          var0 = new counterInstance(key);
          counterInstance_L0.Add(var0);
        }

        executeTime = var0.executeTime;
      }

      public static float LeaveDigit(float value, int digits)
      {
        float int1 = 1;

        for (int i = 1; i < digits + 1; i++)
        {
          int1 *= 10;
        }

        return (float)Math.Round(value * int1) / int1;
      }

      public static GameObject findTransform(GameObject GameObject0, string[] stringList0)
      {
        GameObject GameObject1 = GameObject0;

        foreach (var i in stringList0)
        {
          GameObject1 = GameObject1.transform.Find(i).gameObject;
        }

        return GameObject1;
      }

      public static string findFilePath(string path, string fileName)
      {
        string[] res = System.IO.Directory.GetFiles(path, fileName, System.IO.SearchOption.AllDirectories);
        string res2 = res.Length > 0 ? res[0] : "";
        return res2.Replace("\\", "/");
      }

      public static string findFolderPath(string path, string folderName)
      {
        string[] res = System.IO.Directory.GetDirectories(path, folderName, System.IO.SearchOption.AllDirectories);
        string res2 = res.Length > 0 ? res[0] : "";
        return res2.Replace("\\", "/");
      }
    }

    public class serializer
    {
      public object targetObject;
      public Action<object> onValueChanged;
      public string name;

      public object memberInfo;

      public enum MemberType
      {
        field,
        property,
        method
      }

      private MemberType memberType;

      public Type type;

      private object lastValue;

      public object value
      {
        get
        {
          switch (memberType)
          {
            case MemberType.field:
              return ((FieldInfo)memberInfo).GetValue(obj: targetObject);
            case MemberType.property:
              var var0 =
                ((PropertyInfo)memberInfo).GetValue
                (
                  obj: targetObject,
                  index: null
                );
              return var0;
            case MemberType.method:
              throw new NotImplementedException();
            default:
              throw new NotImplementedException();
          }
        }
        set
        {
          switch (memberType)
          {
            case MemberType.field:
              ((FieldInfo)memberInfo).SetValue
              (
                obj: targetObject,
                value: Convert.ChangeType(value, type)
              );
              break;
            case MemberType.property:
              ((PropertyInfo)memberInfo).SetValue
              (
                obj: targetObject,
                value: Convert.ChangeType(value, type),
                index: null
              );
              break;
            case MemberType.method:
              throw new NotImplementedException();
            default:
              throw new NotImplementedException();
          }
        }
      }

      public serializer(object targetObject, string name, MemberType memberType)
      {
        this.targetObject = targetObject;
        this.name = name;
        this.memberType = memberType;

        Type type0 = targetObject is Type ? ((Type)targetObject) : targetObject.GetType();

        switch (memberType)
        {
          case MemberType.field:
            memberInfo =
              type0.GetField
              (
                name: name,
                bindingAttr: BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
              );

            type = ((FieldInfo)memberInfo).FieldType;
            type = type.IsEnum ? typeof(Int32) : type;
            break;
          case MemberType.property:
            memberInfo =
              type0.GetProperty
              (
                name: name,
                bindingAttr: BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
              );

            type = ((PropertyInfo)memberInfo).PropertyType;
            type = type.IsEnum ? typeof(Int32) : type;
            break;
          case MemberType.method:
            throw new NotImplementedException();
        }

        lastValue = value;
      }
    }

    public static class runtimeInspector
    {
      public static Transform panel;

      public static inputManager InputField(string label, object targetObject, string propertyName, serializer.MemberType memberType)
      {
                GameObject GameObject0 =
                  (GameObject)UnityEngine.Object.Instantiate
                  (
                    original: Resources.Load(path: "InputField"),
                    parent: panel
                  ) as GameObject;

                serializer serializer0 =
                  new serializer
                  (
                    targetObject: targetObject,
                    name: propertyName,
                    memberType: memberType
                  );

                return new inputField(UIElement: GameObject0, serializer: serializer0, label);
              
      }

      public static inputManager ReadOnlyField(string label, object targetObject, string propertyName, serializer.MemberType memberType)
      {
                GameObject GameObject0 = (GameObject)UnityEngine.Object.Instantiate
                (
                  original: Resources.Load(path: "readOnlyField"),
                  parent: panel
                ) as GameObject;

                serializer serializer0 = new serializer
                (
                  targetObject: targetObject,
                  name: propertyName,
                  memberType: memberType
                );

                return new readOnlyField(GameObject0, serializer0, label);

               
      }

      public static inputManager slider(string label, object targetObject, string propertyName, float minSliderValue, float maxSliderValue, serializer.MemberType memberType)
      {
               GameObject GameObject0 = (GameObject)UnityEngine.Object.Instantiate
               (
                 original: Resources.Load(path: "Slider"),
                 parent: panel
               );

               serializer serializer0 = new serializer
               (
                 targetObject: targetObject,
                 name: propertyName,
                 memberType: memberType
               );

               new inputField(GameObject0, serializer0, label);

               return new Slider(GameObject0, serializer0, label, minSliderValue, maxSliderValue); ;
              
      }

      public static inputManager dropDown(string label, object targetObject, string propertyName, Type enumType, serializer.MemberType memberType)
      {
                GameObject GameObject0 =
                  (GameObject)UnityEngine.Object.Instantiate
                  (
                    original: Resources.Load(path: "dropDown"),
                    parent: panel
                  ) as GameObject;

                serializer serializer0 =
                  new serializer
                  (
                    targetObject: targetObject,
                    name: propertyName,
                    memberType: memberType
                  );

                return new dropdown(GameObject0, serializer0, label, enumType); ;
              
      }

      public class inputManager
      {
        public GameObject UIElement;
        public serializer serializer;

        public genericDelegate<bool>.get isVisible_If = null;

        private bool _isVisible = true;
        public bool isVisible
        {
          get
          {
            return _isVisible;
          }
          set
          {
            if (_isVisible == value)
              return;

            _isVisible = value;
            UIElement.SetActive(value: _isVisible);
          }
        }

        public inputManager(GameObject UIElement, serializer serializer, string label)
        {
          this.UIElement = UIElement;
          this.serializer = serializer;

          var var0 = utilities0.findTransform(UIElement, new[] { "tag", "Label" });
          var0.GetComponent<Text>().text = label;

          utilities0.runEveryXFrame(X: 10, action0: isVisibleM);
          utilities0.runEveryXFrame(X: 10, action0: Update);
        }

        public virtual void Update()
        {
        }

        public void isVisibleM()
        {
          if (isVisible_If == null)
            return;

          isVisible = isVisible_If();
        }
      }

      public class inputField : inputManager
      {
        public UnityEngine.UI.InputField InputField0;

        public inputField(GameObject UIElement, serializer serializer, string label) :
          base(UIElement: UIElement, serializer: serializer, label: label)
        {
          InputField0 =
            UIElement.GetComponentInChildren<UnityEngine.UI.InputField>();

          InputField0.onEndEdit.AddListener
          (
            call: (value) =>
            { serializer.value = value; }
          );
        }

        public override void Update()
        {
          if (serializer == null)
            return;

          if (!InputField0.isFocused)
            InputField0.text = serializer.value.ToString();
        }
      }

      public class readOnlyField : inputManager
      {
        public UnityEngine.UI.Text text0;

        public readOnlyField(GameObject UIElement, serializer serializer, string label) :
          base(UIElement: UIElement, serializer: serializer, label: label)
        {
          var var0 = utilities0.findTransform(UIElement, new[] { "value", "text" });
          text0 = var0.GetComponent<Text>();

          serializer.onValueChanged = (value) => {
            text0.text = value.ToString();
          };
        }
      }

      public class Slider : inputManager
      {
        public UnityEngine.UI.Slider Slider0;

        public Slider(GameObject UIElement, serializer serializer, string label, float minSliderValue, float maxSliderValue) :
          base(UIElement: UIElement, serializer: serializer, label: label)
        {
          Slider0 = UIElement.GetComponentInChildren<UnityEngine.UI.Slider>();
          Slider0.minValue = minSliderValue;
          Slider0.maxValue = maxSliderValue;

          Slider0.onValueChanged.AddListener
          (
            call: (value) => { serializer.value = value; }
          );

          serializer.onValueChanged = (value) =>
          {
            Slider0.value = Convert.ToSingle(value: value);
          };
        }
      }

      public class dropdown : inputManager
      {
        public UnityEngine.UI.Dropdown Dropdown0;
        public Type enumType;

        public dropdown(GameObject UIElement, serializer serializer, string label, Type enumType) :
          base(UIElement: UIElement, serializer: serializer, label: label)
        {
          this.enumType = enumType;

          Dropdown0 = UIElement.GetComponentInChildren<UnityEngine.UI.Dropdown>();
          Dropdown0.options.Clear();
          foreach (var item in Enum.GetNames(enumType: enumType))
            Dropdown0.options.Add(item: new Dropdown.OptionData(text: item));
          Dropdown0.value = (int)serializer.value;
          Dropdown0.RefreshShownValue();

          Dropdown0.onValueChanged.AddListener
          (
            call: (value) => { serializer.value = value; }
          );
          serializer.onValueChanged = (value) =>
          {
            Dropdown0.value = Convert.ToInt32(value: value);
          };
        }
      }
    }

    public static class genericDelegate<T>
    {
      public delegate T get();
    }

#if UNITY_EDITOR
    public static class customInspector
    {
      private static void createGUIWithStyle(Color color, Color backgroundColor, Color contentColor, bool enabled, Action action)
      {
        var tempColor = GUI.color;
        var tempBackgroundColor = GUI.backgroundColor;
        var tempContentColor = GUI.contentColor;
        var tempEnabled = GUI.enabled;

        GUI.color = color;
        GUI.backgroundColor = backgroundColor;
        GUI.contentColor = contentColor;
        GUI.enabled = enabled;

        action();

        GUI.color = tempColor;
        GUI.backgroundColor = tempBackgroundColor;
        GUI.contentColor = tempContentColor;
        GUI.enabled = tempEnabled;
      }

      private static void showTitle(string title, string description)
      {
        GUILayout.Label(new GUIContent(title, description), GUILayout.ExpandWidth(true));
      }

      public static void showTitleStyle0(string title, string description)
      {
        createGUIWithStyle
        (
          color: Color.white,
          backgroundColor: Color.white,
          contentColor: Color.Lerp(Color.yellow, Color.gray, 0.5f),
          enabled: true,

          action: () =>
          {
            showTitle(title, description);
          }
        );
      }

      public static void space(int int0)
      {
        for (int i = 1; i < int0; i++)
        {
          EditorGUILayout.Space();
        }
      }

      public static void separatorLine(int height, Color color, int offsetX, int space)
      {
        Color tempColor = GUI.color;
        GUI.color = color;
        GUI.Box(new Rect(offsetX, GUILayoutUtility.GetLastRect().y + GUILayoutUtility.GetLastRect().height + space / 2 - height / 2 + 1, EditorGUIUtility.currentViewWidth, height), "");
        GUI.color = tempColor;

        GUILayout.Space(space);
      }

      public static void buttonToInvokeMethod(string displayName, object targetObject, string memberName)
      {
        float width = EditorGUIUtility.currentViewWidth / 2;

        GUILayout.BeginHorizontal();

        GUILayout.Space((EditorGUIUtility.currentViewWidth / 2) - (width / 1.5f));

        MethodInfo MethodInfo0 = ((Type)targetObject).GetMethod(memberName);

        if (GUILayout.Button(displayName, GUILayout.Width(width)))
          MethodInfo0.Invoke(targetObject, new object[0]);

        GUILayout.EndHorizontal();
      }

      public static void buttonToInvokeMethod1(string displayName, object targetObject, string memberName)
      {
        float width = EditorGUIUtility.currentViewWidth / 2;

        GUILayout.BeginHorizontal();

        GUILayout.Space((EditorGUIUtility.currentViewWidth) - (width / 2f));

        MethodInfo MethodInfo0 = ((Type)targetObject).GetMethod(memberName);

        if (GUILayout.Button(displayName, GUILayout.Width(width / 3.5f)))
          MethodInfo0.Invoke(targetObject, new object[0]);

        GUILayout.EndHorizontal();
      }


      public static void twoOptionsEnumOnOffButton_s(serializer serializer, string description, string displayName)
      {
        GUI.Box(new Rect(0, GUILayoutUtility.GetLastRect().y + GUILayoutUtility.GetLastRect().height, EditorGUIUtility.currentViewWidth, 20), "");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent(displayName, description));

        object value = serializer.value;
        if ((int)serializer.value == 0)
        {
          GUI.backgroundColor = Color.Lerp(Color.green, Color.gray, 0.5f);

          if (GUILayout.Button("ON", GUILayout.Width(EditorGUIUtility.currentViewWidth / 4)))
            serializer.value = 1;

          GUI.backgroundColor = Color.white;
        }
        else
        {
          GUI.backgroundColor = Color.Lerp(Color.red, Color.gray, 0.7f);

          if (GUILayout.Button("OFF", GUILayout.Width(EditorGUIUtility.currentViewWidth / 4)))
            serializer.value = 0;

          GUI.backgroundColor = Color.white;
        }

        EditorGUILayout.EndHorizontal();
      }

      public static class inspectField
      {
        private static void enumField(serializer serializer, string displayName, string toolkit)
        {
          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          EditorGUI.BeginChangeCheck();
          GUILayout.Label(new GUIContent(displayName, toolkit), GUILayout.ExpandWidth(true));
          object value = serializer.value;
          Array enumValues = value.GetType().GetEnumValues();
          string[] foo = enumValues.OfType<object>().Select(o => o.ToString()).ToArray();
          value = EditorGUILayout.Popup((int)value, foo);

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void enumField_Style0(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              enumField(serializer, displayName, toolkit);
            }
          );
        }

        private static void boolField(serializer serializer, string displayName, string toolkit)
        {
          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          GUILayout.Label(new GUIContent(displayName, toolkit), GUILayout.ExpandWidth(true));

          EditorGUI.BeginChangeCheck();
          object value = serializer.value;
          value = EditorGUILayout.Toggle((bool)value);

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void boolField_Style0(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              boolField(serializer, displayName, toolkit);
            }
          );
        }

        private static void floatIntField(serializer serializer, string displayName, string toolkit)
        {
          float width = EditorGUIUtility.currentViewWidth / 2;

          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          GUILayout.Label(new GUIContent(displayName, toolkit), GUILayout.ExpandWidth(true));

          object value = serializer.value;
          EditorGUI.BeginChangeCheck();
          value = EditorGUILayout.TextField(Convert.ToString(value), GUILayout.Height(20), GUILayout.MinWidth(width));
          value = utilities0.stringToFloat(value.ToString());

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void floatIntField_Style0(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              floatIntField(serializer, displayName, toolkit);
            }
          );
        }
        public static void floatIntField_Style1(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: false,

            action: () =>
            {
              floatIntField(serializer, displayName, toolkit);
            }
          );
        }

        private static void stringField(serializer serializer, string displayName, string toolkit)
        {
          float width = EditorGUIUtility.currentViewWidth / 2;

          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          GUILayout.Label(new GUIContent(displayName, toolkit), GUILayout.ExpandWidth(true));

          object value = serializer.value;
          EditorGUI.BeginChangeCheck();
          value = EditorGUILayout.TextField(Convert.ToString(value), GUILayout.Height(20), GUILayout.MinWidth(width));

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void stringField_Style0(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              stringField(serializer, displayName, toolkit);
            }
          );
        }

        private static void floatIntField_slider(serializer serializer, string displayName, string toolkit, float float1, float float2)
        {
          float width = EditorGUIUtility.currentViewWidth / 2;

          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          EditorGUILayout.LabelField(new GUIContent(displayName, toolkit), GUILayout.Height(20), GUILayout.MinWidth((width / 2) - 10));

          EditorGUI.BeginChangeCheck();
          object value = serializer.value;

          if (serializer.type == typeof(float))
            value = EditorGUILayout.Slider(Convert.ToSingle(value), float1, float2, GUILayout.Height(20), GUILayout.MinWidth(width));
          else if (serializer.type == typeof(int))
            value = EditorGUILayout.IntSlider(Convert.ToInt32(value), (int)float1, (int)float2, GUILayout.Height(20), GUILayout.MinWidth(width));

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void floatIntField_slider_Style0(serializer serializer, string displayName, string toolkit, float float1, float float2)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              floatIntField_slider(serializer, displayName, toolkit, float1, float2);
            }
          );
        }

        public static void objectField(serializer serializer, string displayName, string toolkit)
        {
          EditorGUILayout.BeginHorizontal();
          GUILayout.Space(10);

          GUILayout.Label(new GUIContent(displayName, toolkit), GUILayout.ExpandWidth(true));

          EditorGUI.BeginChangeCheck();
          object value = serializer.value;
          value = EditorGUILayout.ObjectField((UnityEngine.Object)value, serializer.type, true);

          if (EditorGUI.EndChangeCheck())
            serializer.value = value;

          EditorGUILayout.EndHorizontal();
        }
        public static void objectField_Style0(serializer serializer, string displayName, string toolkit)
        {
          createGUIWithStyle
          (
            color: Color.white,
            backgroundColor: Color.Lerp(Color.cyan, Color.white, 0.5f),
            contentColor: Color.Lerp(Color.yellow, Color.white, 0.5f),
            enabled: true,

            action: () =>
            {
              objectField(serializer, displayName, toolkit);
            }
          );
        }
      }
    }
#endif
  }
}