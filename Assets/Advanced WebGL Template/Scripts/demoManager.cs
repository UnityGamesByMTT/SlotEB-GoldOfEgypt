using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class demoManager : MonoBehaviour
{
  public Text dateText;
  public Text timeText;

  public Color[] colors0;
  public List<Color> Color_L0;

  public List<Renderer> renderers;

  void Start()
  {
    InvokeRepeating("updateDateAndTime", 0, 1);
  }

  public void updateDateAndTime()
  {
    dateText.text = DateTime.Now.ToLongDateString();
    timeText.text = DateTime.Now.ToLongTimeString();
  }
}