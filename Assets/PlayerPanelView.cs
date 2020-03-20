using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using AthPlayer;
using UnityEngine;
using UnityEngine.UI;

namespace Domino {
  public class PlayerPanelView {
    public delegate void OnCapabilityButtonClicked(int capabilityId);

    public static readonly int TIME_ANCHOR_MOVE_CAPABILITY_ID = 1;
    public static readonly int REVERT_CAPABILITY_ID = 2;
    public static readonly int FIRE_BOMB_CAPABILITY_ID = 3;
    public static readonly int FIRE_CAPABILITY_ID = 4;
    public static readonly int MIRE_CAPABILITY_ID = 5;
    public static readonly int INTERACT_CAPABILITY_ID = 6;
    public static readonly int DEFEND_CAPABILITY_ID = 7;
    public static readonly int COUNTER_CAPABILITY_ID = 8;

    public event OnCapabilityButtonClicked CapabilityButtonClicked;

    IClock cinematicTimer;
    OverlayPaneler overlayPaneler;
    Unit unit;
    OverlayPanelView playerStatusView;
    Looker looker;
    List<int> textOverlayObjectIds = new List<int>();

    public PlayerPanelView(IClock cinematicTimer, OverlayPaneler overlayPaneler, Looker looker, Unit unit) {
      throw new System.Exception("le");
      this.cinematicTimer = cinematicTimer;
      this.overlayPaneler = overlayPaneler;
      this.unit = unit;
      this.looker = looker;

      this.playerStatusView = overlayPaneler.MakePanel(cinematicTimer, 0, 0, 100, 50, 70, 3, .6667f);
      playerStatusView.AddBackground(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0, 0, 0, 0));

      textOverlayObjectIds = new List<int>();

      int position = 0;
      bool canChronomancy = true;
      if (canChronomancy) {
        AddButton(position++, "k", "(A) Time Anchor: Place a time anchor, which you can later Rewind to.");
        AddButton(position++, "1", "(R) Rewind Time to your last Time Anchor. MP cost: 2+turns/4.");
      }
      bool canInteract = true;
      if (canInteract) {
        AddButton(position++, "2", "(E) Interact with something where you're standing.");
      }
      bool canDefy = true;
      if (canDefy) {
        AddButton(position++, "q", "(D) Defy: Reduce incoming damage by 2/3. Tempts foes.");
      }
      bool canCounter = false;
      if (canCounter) {
        AddButton(position++, "v", "(C) Counter: Counterattack when hit. Cost 1mp, 2mp more when hit. Tempts foes.");
      }
      bool canFire = false;
      if (canFire) {
        AddButton(position++, "r", "(F) Fire: Cast fireball for " + IncendianFalls.Actions.FIRE_COST + "mp for " + IncendianFalls.Actions.FIRE_DAMAGE + " damage.");
      }
      bool canFireBomb = true;
      if (canFireBomb) {
        AddButton(position++, "r", "(F) Fire Bomb: Places a bomb for " + IncendianFalls.Actions.FIRE_BOMB_COST + "mp which will explode after a couple turns for " + IncendianFalls.Actions.FIRE_BOMB_DAMAGE + " damage.");
      }
      bool canMire = true;
      if (canMire) {
        AddButton(position++, "f", "(S) Slow: Freeze enemy for one turn. Cost " + IncendianFalls.Actions.MIRE_COST + "mp. Stacks by half each time.");
      }

      UpdatePlayerStatus();
    }

    private int AddButton(int position, string symbol, string hoverText) {
      int timeAnchorMoveButtonId =
        playerStatusView.AddButton(
          0, 70 - 3, 3 + position * 3, 3, 2, 0,
          new UnityEngine.Color(.2f, .2f, 1f), new UnityEngine.Color(0, 0, 0, 0), new UnityEngine.Color(.3f, .3f, .3f),
          () => CapabilityButtonClicked?.Invoke(TIME_ANCHOR_MOVE_CAPABILITY_ID),
          () => looker.Look(hoverText),
          () => looker.Look(""));
      playerStatusView.AddSymbol(
        timeAnchorMoveButtonId, 70 - 3, 3 + position * 3, 10f * 2, 1, new UnityEngine.Color(1, 1, 1), new OverlayFont("symbols", .3f), symbol);
      return timeAnchorMoveButtonId;
    }

    public void Clear() {
      playerStatusView.ScheduleClose(0);
      playerStatusView = null;
    }

    public void UpdatePlayerStatus() {
      throw new System.Exception("le");
      foreach (var id in textOverlayObjectIds) {
        playerStatusView.Remove(id);
      }

      string message = "HP " + unit.hp + "/" + unit.maxHp;
      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        message += "   " + "MP " + sorcerous.mp + "/" + sorcerous.maxMp;
      }
      playerStatusView.AddString(0, 1, 1, 0, new UnityEngine.Color(1, 1, 1), new OverlayFont("prose", 2f), message);
    }
  }
}
