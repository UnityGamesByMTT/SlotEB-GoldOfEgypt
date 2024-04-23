using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ManageLineButtons : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerUpHandler,IPointerDownHandler
{

	[SerializeField]
	private SlotBehaviour slotManager;
	[SerializeField]
	private TMP_Text num_text;
	[SerializeField]
	private Sprite Disabled_Button;


	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isMobilePlatform)
		{
			Debug.Log("run on pointer enter");
			slotManager.GenerateStaticLine(num_text);
		}
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isMobilePlatform)
		{
			Debug.Log("run on pointer exit");
			slotManager.DestroyStaticLine();
		}
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
		{
			this.gameObject.GetComponent<Button>().Select();
			Debug.Log("run on pointer down");
			slotManager.GenerateStaticLine(num_text);
		}
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
		{
			Debug.Log("run on pointer up");
			slotManager.DestroyStaticLine();
			DOVirtual.DelayedCall(0.1f, () =>
			{
				this.gameObject.GetComponent<Button>().spriteState = default;
				EventSystem.current.SetSelectedGameObject(null);
				//this.gameObject.GetComponent<Image>().sprite = Disabled_Button;
			 });
		}
	}
}
