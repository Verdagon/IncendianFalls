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

    public const int TIME_ANCHOR_MOVE_CAPABILITY_ID = 1;
    public const int REVERT_CAPABILITY_ID = 2;
    public const int FIRE_BOMB_CAPABILITY_ID = 3;
    public const int FIRE_CAPABILITY_ID = 4;
    public const int MIRE_CAPABILITY_ID = 5;
    public const int INTERACT_CAPABILITY_ID = 6;
    public const int DEFEND_CAPABILITY_ID = 7;
    public const int COUNTER_CAPABILITY_ID = 8;

    public event OnCapabilityButtonClicked CapabilityButtonClicked;

    IClock cinematicTimer;
    OverlayPaneler overlayPaneler;
    Unit unit;
    OverlayPanelView playerStatusView;
    Looker looker;
    List<int> textOverlayObjectIds = new List<int>();

    public PlayerPanelView(IClock cinematicTimer, OverlayPaneler overlayPaneler, Looker looker, Unit unit) {
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
        AddButton(position++, TIME_ANCHOR_MOVE_CAPABILITY_ID, "k", "(A) Time Anchor: Place a time anchor, which you can later Rewind to.");
        AddButton(position++, REVERT_CAPABILITY_ID, "1", "(R) Rewind Time to your last Time Anchor. MP cost: 2+turns/4.");
      }
      bool canInteract = true;
      if (canInteract) {
        AddButton(position++, INTERACT_CAPABILITY_ID, "2", "(E) Interact with something where you're standing.");
      }
      bool canDefy = true;
      if (canDefy) {
        AddButton(position++, DEFEND_CAPABILITY_ID, "q", "(D) Defy: Reduce incoming damage by 2/3. Tempts foes.");
      }
      bool canCounter = false;
      if (canCounter) {
        AddButton(position++, COUNTER_CAPABILITY_ID, "v", "(C) Counter: Counterattack when hit. Cost 1mp, 2mp more when hit. Tempts foes.");
      }
      bool canFire = false;
      if (canFire) {
        AddButton(position++, FIRE_CAPABILITY_ID, "r", "(F) Fire: Cast fireball for " + IncendianFalls.Actions.FIRE_COST + "mp for " + IncendianFalls.Actions.FIRE_DAMAGE + " damage.");
      }
      bool canFireBomb = true;
      if (canFireBomb) {
        AddButton(position++, FIRE_BOMB_CAPABILITY_ID, "r", "(F) Fire Bomb: Places a bomb for " + IncendianFalls.Actions.FIRE_BOMB_COST + "mp which will explode after a couple turns for " + IncendianFalls.Actions.FIRE_BOMB_DAMAGE + " damage.");
      }
      bool canMire = true;
      if (canMire) {
        AddButton(position++, MIRE_CAPABILITY_ID, "f", "(S) Slow: Freeze enemy for one turn. Cost " + IncendianFalls.Actions.MIRE_COST + "mp. Stacks by half each time.");
      }

      UpdatePlayerStatus();
    }

    private int AddButton(int position, int capabilityId, string symbol, string hoverText) {
      float marginBetweenButtons = .2f;
      float paddingInsideButton = .25f;
      float buttonSize = 2f;
      int timeAnchorMoveButtonId =
        playerStatusView.AddButton(
          0, 70 - 3, 3 + position * 2 + (position + 1) * .2f, 3, 2, 0,
          new UnityEngine.Color(.4f, .4f, .4f), new UnityEngine.Color(0, 0, 0, 0), new UnityEngine.Color(1.2f, 1.2f, 1.2f),
          () => CapabilityButtonClicked?.Invoke(capabilityId),
          () => looker.SetTooltip(hoverText),
          () => looker.SetTooltip(""));
      float symbolX = 70 - buttonSize - 1 + paddingInsideButton;
      float symbolY = 3 + position * buttonSize + (position + 1) * marginBetweenButtons + paddingInsideButton;
      float symbolSize = buttonSize - paddingInsideButton * 2;
      playerStatusView.AddSymbol(
        timeAnchorMoveButtonId, symbolX, symbolY, symbolSize, 1, new UnityEngine.Color(1, 1, 1), new OverlayFont("symbols", 2f), symbol); ; ;
      return timeAnchorMoveButtonId;
    }

    public void Destroy() {
      playerStatusView.ScheduleClose(0);
      playerStatusView = null;
    }

    public void UpdatePlayerStatus() {
      foreach (var id in textOverlayObjectIds) {
        playerStatusView.Remove(id);
      }

      string message = "HP " + unit.hp + "/" + unit.maxHp;
      var sorcerous = unit.components.GetOnlySorcerousUCOrNull();
      if (sorcerous.Exists()) {
        message += "   " + "MP " + sorcerous.mp + "/" + sorcerous.maxMp;
      }
      textOverlayObjectIds = playerStatusView.AddString(0, 1, 1, 0, new UnityEngine.Color(1, 1, 1), new OverlayFont("prose", 1.4f), message);
    }
  }
}
