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

    // G = in grid units
    public const int PANEL_GH = 3;

    public const int TIME_ANCHOR_MOVE_CAPABILITY_ID = 1;
    public const int REVERT_CAPABILITY_ID = 2;
    public const int FIRE_BOMB_CAPABILITY_ID = 3;
    public const int FIRE_CAPABILITY_ID = 4;
    public const int MIRE_CAPABILITY_ID = 5;
    public const int INTERACT_CAPABILITY_ID = 6;
    public const int DEFEND_CAPABILITY_ID = 7;
    public const int COUNTER_CAPABILITY_ID = 8;
    public const int BLAZE_CAPABILITY_ID = 9;
    public const int EXPLOSION_CAPABILITY_ID = 10;

    public event OnCapabilityButtonClicked CapabilityButtonClicked;

    Unit unit;
    OverlayPanelView playerStatusView;
    Looker looker;
    List<int> textOverlayObjectIds = new List<int>();

    public PlayerPanelView(IClock cinematicTimer, OverlayPaneler overlayPaneler, Looker looker, Unit unit) {
      this.unit = unit;
      this.looker = looker;

      this.playerStatusView = overlayPaneler.MakePanel(0, 0, overlayPaneler.screenGW, 3);
      playerStatusView.AddBackground(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0, 0, 0, 0));

      textOverlayObjectIds = new List<int>();

      int position = 0;
      bool canChronomancy = false;
      if (canChronomancy) {
        AddButton(overlayPaneler.screenGW, position++, TIME_ANCHOR_MOVE_CAPABILITY_ID, "k", "(A) Time Anchor: Place a time anchor, which you can later Rewind to.");
        AddButton(overlayPaneler.screenGW, position++, REVERT_CAPABILITY_ID, "1", "(R) Rewind Time to your last Time Anchor. MP cost: 2+turns/4.");
      }
      bool canInteract = true;
      if (canInteract) {
        AddButton(overlayPaneler.screenGW, position++, INTERACT_CAPABILITY_ID, "2", "(E) Interact with something where you're standing.");
      }
      bool canDefy = true;
      if (canDefy) {
        AddButton(overlayPaneler.screenGW, position++, DEFEND_CAPABILITY_ID, "q", "(D) Defy: Reduce incoming damage by 2/3. Tempts foes.");
      }
      bool canCounter = false;
      if (canCounter) {
        AddButton(overlayPaneler.screenGW, position++, COUNTER_CAPABILITY_ID, "v", "(C) Counter: Counterattack when hit. Cost 1mp, 2mp more when hit. Tempts foes.");
      }
      bool canFire = false;
      if (canFire) {
        AddButton(overlayPaneler.screenGW, position++, FIRE_CAPABILITY_ID, "r", "(F) Fire: Cast fireball for " + IncendianFalls.Actions.FIRE_COST + "mp for " + IncendianFalls.Actions.FIRE_DAMAGE + " damage.");
      }
      bool canFireBomb = false;
      if (canFireBomb) {
        AddButton(overlayPaneler.screenGW, position++, FIRE_BOMB_CAPABILITY_ID, "r", "(F) Fire Bomb: Places a bomb for " + IncendianFalls.Actions.FIRE_BOMB_COST + "mp which will explode after a couple turns for " + IncendianFalls.Actions.FIRE_BOMB_DAMAGE + " damage.");
      }
      bool canBlaze = true;
      if (canBlaze) {
        AddButton(overlayPaneler.screenGW, position++, BLAZE_CAPABILITY_ID, "w", "(B) Blaze: Do " + IncendianFalls.Actions.BLAZE_DAMAGE + " per turn for " + IncendianFalls.Actions.BLAZE_DURATION + " turns for " + IncendianFalls.Actions.FIRE_BOMB_COST + "mp.");
      }
      bool canExplosion = true;
      if (canExplosion) {
        AddButton(overlayPaneler.screenGW, position++, EXPLOSION_CAPABILITY_ID, "w", "(X) Explosion: Do " + IncendianFalls.Actions.EXPLOSION_DAMAGE + " to an 8-space area for " + IncendianFalls.Actions.FIRE_BOMB_COST + "mp.");
      }
      bool canMire = false;
      if (canMire) {
        AddButton(overlayPaneler.screenGW, position++, MIRE_CAPABILITY_ID, "f", "(S) Slow: Freeze enemy for one turn. Cost " + IncendianFalls.Actions.MIRE_COST + "mp. Stacks by half each time.");
      }

      UpdatePlayerStatus();
    }

    private int AddButton(int screenGW, int position, int capabilityId, string symbol, string hoverText) {
      float marginBetweenButtons = .2f;
      float paddingInsideButton = .25f;
      float buttonSize = 2f;
      int timeAnchorMoveButtonId =
        playerStatusView.AddButton(
          0, screenGW - 3, 3 + position * 2 + (position + 1) * .2f, 3, 2, 0,
          new UnityEngine.Color(.4f, .4f, .4f), new UnityEngine.Color(0, 0, 0, 0), new UnityEngine.Color(1.2f, 1.2f, 1.2f),
          () => CapabilityButtonClicked?.Invoke(capabilityId),
          () => looker.SetTooltip(hoverText),
          () => looker.SetTooltip(""));
      float symbolX = screenGW - buttonSize - 1 + paddingInsideButton;
      float symbolY = 3 + position * buttonSize + (position + 1) * marginBetweenButtons + paddingInsideButton;
      float symbolSize = buttonSize - paddingInsideButton * 2;
      playerStatusView.AddSymbol(
        timeAnchorMoveButtonId, symbolX, symbolY, symbolSize, 1, new UnityEngine.Color(1, 1, 1), Fonts.SYMBOLS_OVERLAY_FONT, symbol); ; ;
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
      textOverlayObjectIds = playerStatusView.AddString(0, 1, 1, 0, new UnityEngine.Color(1, 1, 1), Fonts.PROSE_OVERLAY_FONT, message);
    }
  }
}
