using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PayoutCalculation : MonoBehaviour
{
    [SerializeField]
    private int x_Distance;
    [SerializeField]
    private int y_Distance;

    [SerializeField]
    private Transform LineContainer;
    [SerializeField]
    private GameObject Line_Prefab;

    [SerializeField]
    private Vector2 InitialLinePosition = new Vector2(-315, 100);


    //internal void GeneratePayoutLine(int possibilities,int min = 5, int sub4 = 1, int sub3 = 1)
    //{
    //    switch (possibilities)
    //    {
    //        //case 1:
    //        //    MiddleLine(min, sub4, sub3);
    //        //    break;
    //        //case 2:
    //        //    TopLine(min, sub4, sub3);
    //        //    break;
    //        //case 3:
    //        //    BottomLine(min, sub4, sub3);
    //        //    break;
    //        //case 4:
    //        //    VLine(min, sub4, sub3);
    //        //    break;
    //        //case 5:
    //        //    ReverseVLine(min, sub4, sub3);
    //        //    break;
    //        //case 6:
    //        //    UpperZigZagLine(min, sub4, sub3);
    //        //    break;
    //        //case 7:
    //        //    LowerZigZagLine(min, sub4, sub3);
    //        //    break;
    //        //case 8:
    //        //    ZLine(min, sub4, sub3);
    //        //    break;
    //        //case 9:
    //        //    ReverseZLine(min, sub4, sub3);
    //        //    break;
    //        //case 10:
    //        //    UnevenLine(min, sub4, sub3);
    //        //    break;
    //        //case 11:
    //        //    ReverseUnevenLine(min, sub4, sub3);
    //        //    break;
    //        //case 12:
    //        //    ULine(min, sub4, sub3);
    //        //    break;
    //        //case 13:
    //        //    ReverseULine(min, sub4, sub3);
    //        //    break;
    //        //case 14:
    //        //    WLine(min, sub4, sub3);
    //        //    break;
    //        //case 15:
    //        //    ReverseWLine(min, sub4, sub3);
    //        //    break;
    //        //case 16:
    //        //    ReverseSmallTLine(min, sub4, sub3);
    //        //    break;
    //        //case 17:
    //        //    SmallTLine(min, sub4, sub3);
    //        //    break;
    //        //case 18:
    //        //    TLine(min, sub4, sub3);
    //        //    break;
    //        //case 19:
    //        //    ReverseTLine(min, sub4, sub3);
    //        //    break;
    //        //case 20:
    //        //    SmileyLine(min, sub4, sub3);
    //        //    break;
    //    }
    //}

    GameObject TempObj = null;

    internal void GeneratePayoutLinesBackend(List<int> x_index, List<int> y_index, int Count, bool isStatic = false)
    {
        GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
        MyLineObj.transform.localPosition = new Vector2(InitialLinePosition.x, InitialLinePosition.y);
        UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
        for (int i = 0; i < Count; i++)
        {
            var points = new Vector2() { x = x_index[i] * x_Distance, y = y_index[i] * -y_Distance };
            var pointlist = new List<Vector2>(MyLine.Points);
            pointlist.Add(points);
            MyLine.Points = pointlist.ToArray();
        }
        var newpointlist = new List<Vector2>(MyLine.Points);
        newpointlist.RemoveAt(0);
        MyLine.Points = newpointlist.ToArray();

        if(isStatic)
        {
            TempObj = MyLineObj;
        }
    }

    internal void ResetStaticLine()
    {
        if(TempObj!=null)
        {
            Destroy(TempObj);
            TempObj = null;
        }
    }

    internal void ResetLines()
    {
        foreach (Transform child in LineContainer)
        {
            Destroy(child.gameObject);
        }
    }


    #region PayoutLine Functions

    //private void MiddleLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    for (int i = 0; i < 5; i++)
    //    {
    //        var points = new Vector2() { x = x_Points[i], y = y_Points[0] };
    //        var pointlist = new List<Vector2>(MyLine.Points);
    //        pointlist.Add(points);
    //        MyLine.Points = pointlist.ToArray();
    //    }
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void TopLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    for (int i = 0; i < 5; i++)
    //    {
    //        var points = new Vector2() { x = x_Points[i], y = y_Points[0] };
    //        var pointlist = new List<Vector2>(MyLine.Points);
    //        pointlist.Add(points);
    //        MyLine.Points = pointlist.ToArray();
    //    }
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void BottomLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    for (int i = 0; i < 5; i++)
    //    {
    //        var points = new Vector2() { x = x_Points[i], y = y_Points[0] };
    //        var pointlist = new List<Vector2>(MyLine.Points);
    //        pointlist.Add(points);
    //        MyLine.Points = pointlist.ToArray();
    //    }
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void VLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = -y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseVLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void UpperZigZagLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void LowerZigZagLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ZLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseZLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void UnevenLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseUnevenLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ULine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseULine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void WLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseWLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void ReverseSmallTLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void SmallTLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line2Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[1] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}

    //private void TLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}
    //private void ReverseTLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line3Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}
    //private void SmileyLine()
    //{
    //    GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
    //    MyLineObj.transform.localPosition = line1Position;
    //    UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
    //    var points = new Vector2() { x = x_Points[0], y = y_Points[0] };
    //    var pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[1], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[2], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[3], y = -y_Points[2] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    points = new Vector2() { x = x_Points[4], y = y_Points[0] };
    //    pointlist = new List<Vector2>(MyLine.Points);
    //    pointlist.Add(points);
    //    MyLine.Points = pointlist.ToArray();
    //    var newpointlist = new List<Vector2>(MyLine.Points);
    //    newpointlist.RemoveAt(0);
    //    MyLine.Points = newpointlist.ToArray();
    //}
    #endregion
}
