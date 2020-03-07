using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using AthPlayer;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelView : MonoBehaviour {
  public GameObject playerStatusText;
  public GameObject timeAnchorMoveButton;
  public GameObject timeShiftButton;
  public GameObject interactButton;
  public GameObject defendButton;
  public GameObject counterButton;
  public GameObject fireButton;
  public GameObject mireButton;

  public LookPanelView lookPanelView;

  public void Start() {
    timeAnchorMoveButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(A) Time Anchor: Place a time anchor, which you can later Rewind to.");
    timeAnchorMoveButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    timeShiftButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(R) Rewind Time to your last Time Anchor. MP cost: 2+turns/4.");
    timeShiftButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    interactButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(I) Interact with something where you're standing.");
    interactButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    defendButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(D) Defy: Reduce incoming damage by 2/3. Tempts foes.");
    defendButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    counterButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(C) Counter: Counterattack when hit. Cost 1mp, 2mp more when hit. Tempts foes.");
    counterButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    fireButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(F) Fire: Cast fireball for " + IncendianFalls.Actions.FIRE_COST + "mp for " + IncendianFalls.Actions.FIRE_DAMAGE + " damage.");
    fireButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();

    mireButton.GetComponent<UIClickListener>().MouseEnter +=
        () => lookPanelView.ShowMessage(
            "(S) Slow: Freeze enemy for one turn. Cost " + IncendianFalls.Actions.MIRE_COST + "mp. Stacks by half each time.");
    mireButton.GetComponent<UIClickListener>().MouseExit +=
        () => lookPanelView.ClearMessage();
  }

  public void Clear() {
    playerStatusText.SetActive(false);
  }

  public void ShowPlayerStatus(Level level, Unit unit) {
    playerStatusText.SetActive(true);

    string message =
        level.GetName() + "   " +
        "HP " + unit.hp + "/" + unit.maxHp;

    var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
    if (sorcerous.Exists()) {
      message += "   " + "MP " + sorcerous.mp + "/" + sorcerous.maxMp;
    }

    playerStatusText.GetComponent<Text>().text = message;
    var size = playerStatusText.GetComponent<RectTransform>().sizeDelta;
    size.x = playerStatusText.GetComponent<Text>().preferredWidth;
    playerStatusText.GetComponent<RectTransform>().sizeDelta = size; 
  }
}
