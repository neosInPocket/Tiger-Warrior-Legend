using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorialScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
	[SerializeField] private Image character;
	[SerializeField] private Image back;	
	public Action TutorialEnd;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerDown += Skip;
	}
	
	public void PlayTutor()
	{
		_text.text = "Welcome to Tiger Warrior Legend!";
		_text.gameObject.SetActive(true);
		back.gameObject.SetActive(true);
		character.gameObject.SetActive(true);
	}
	
	private void Skip(Finger finger)
	{
		Touch.onFingerDown -= Skip;
		Touch.onFingerDown += Skip1;
	}
	
	private void Skip1(Finger finger)
	{
		Touch.onFingerDown -= Skip1;
		Touch.onFingerDown += Skip2;
		_text.text = "Your goal is to pull your enemies with controlling your chain and defeat you line!";
	}
	
	private void Skip2(Finger finger)
	{
		Touch.onFingerDown -= Skip2;
		Touch.onFingerDown += Skip3;
		_text.text = "If anything reaches your line, you will lose lives";
	}
	
	private void Skip3(Finger finger)
	{
		Touch.onFingerDown -= Skip3;
		Touch.onFingerDown += Skip4;
		_text.text = "Collect coins and buy upgrades in shop, such as max lives amount";
	}
	
	private void Skip4(Finger finger)
	{
		Touch.onFingerDown -= Skip4;
		Touch.onFingerDown += Skip5;
		_text.text = "Good luck!";
	}
	
	private void Skip5(Finger finger)
	{
		Touch.onFingerDown -= Skip5;
		
		_text.gameObject.SetActive(false);
		back.gameObject.SetActive(false);
		character.gameObject.SetActive(false);
		TutorialEnd?.Invoke();
	}
}
