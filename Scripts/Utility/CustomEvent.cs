using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


[System.Serializable]
public class MyIDEvent : UnityEvent<string> { }

[System.Serializable]
public class MyChoiceSelectedEvent : UnityEvent<Choice> { }
