using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using AthPlayer;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnOverlayClosed(int buttonIndex);

public class OldOverlayPanelView : MonoBehaviour {
  public event OnOverlayClosed OverlayClosed;

}
