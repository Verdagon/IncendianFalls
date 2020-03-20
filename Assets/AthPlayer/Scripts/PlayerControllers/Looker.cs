using System;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;

namespace AthPlayer {
  public class Looker : IUnitEffectObserver, IUnitEffectVisitor {
    LookPanelView lookPanelView;
    Unit lookedUnit = Unit.Null;
    TerrainTile lookedTile = TerrainTile.Null;
    string lookedText;

    public Looker(LookPanelView lookPanelView) {
      this.lookPanelView = lookPanelView;
    }

    public void Clear() {
      if (lookedUnit.Exists()) {
        lookedUnit.RemoveObserver(this);
        lookedUnit = Unit.Null;
      }
      lookedTile = TerrainTile.Null;
      lookedText = "";
      lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
    }

    public void Look(string newLookedText) {
      Unit newUnit = Unit.Null;
      TerrainTile newTile = TerrainTile.Null;
      if (newUnit.NullableIs(lookedUnit) && newTile.NullableIs(lookedTile) && newLookedText == lookedText) {
        return;
      }
      Clear();

      lookPanelView.SetStuff(true, newLookedText, "", new List<KeyValuePair<SymbolDescription, string>>());
    }

    public void Look(Unit unit, TerrainTile tile) {
      string newLookedText = "";
      if (unit.NullableIs(lookedUnit) && tile.NullableIs(lookedTile) && newLookedText == lookedText) {
        return;
      }
      Clear();

      lookedUnit = unit;
      lookedTile = tile;

      if (unit.Exists()) {
        Look(unit);
      } else if (tile.Exists()) {
        Look(tile);
      } else {
        Clear();
      }
    }

    private void Look(TerrainTile tile) {
      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();

      foreach (var detail in tile.components) {
        if (detail is ItemTTCAsITerrainTileComponent itemTTC) {
          var item = itemTTC.obj.item;

          if (item is ArmorAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "0",
                              50,
                        new UnityEngine.Color(1f, 1f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Igneous Armor"));
          } else if (item is BlastRodAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                              50,
                        new UnityEngine.Color(1f, .5f, 0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Fire Rod"));
          } else if (item is SlowRodAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "w",
                              50,
                        new UnityEngine.Color(0f, .5f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Mire Staff"));
          } else if (item is GlaiveAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "s",
                              50,
                        new UnityEngine.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Glaive"));
          } else if (item is SpeedRingAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "4",
                              50,
                        new UnityEngine.Color(1f, 1f, 1f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Speed Ring"));
          } else if (item is HealthPotionAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "+",
                              50,
                        new UnityEngine.Color(.8f, 0, .8f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Life Potion"));
          } else if (item is ManaPotionAsIItem) {
            symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        ",",
                              50,
                        new UnityEngine.Color(.25f, .7f, 1.0f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
              "Mana Potion"));
          } else {
            Asserts.Assert(false, "Found item: " + item);
          }
        }
      }

      if (symbolsAndLabels.Count > 0) {
        lookPanelView.SetStuff(true, "", "", symbolsAndLabels);
      }
    }


    private void Look(Unit unit) {
      var symbolsAndLabels = new List<KeyValuePair<SymbolDescription, string>>();

      foreach (var detail in unit.components) {
        if (detail is InvincibilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    50,
                    new UnityEngine.Color(0, .5f, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is DefyingUCAsIUnitComponent Defying) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "q",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Defying"));
        } else if (detail is MiredUCAsIUnitComponent mired) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "f-3",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Mired"));
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is KamikazeAICapabilityUCAsIUnitComponent) {
        } else if (detail is GuardAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          if (bideI.obj.charge > 0) {
            symbolsAndLabels.Add(
                new KeyValuePair<SymbolDescription, string>(
                    new SymbolDescription(
                        "n",
                    50,
                        new UnityEngine.Color(1, 1, 1, 1.5f),
                        0,
                        OutlineMode.NoOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    "Biding"));
          }
        } else if (detail is HealthPotionAsIUnitComponent) {
        } else if (detail is ManaPotionAsIUnitComponent) {
        } else if (detail is SorcerousUCAsIUnitComponent) {
        } else if (detail is BaseCombatTimeUCAsIUnitComponent) {
        } else if (detail is BaseMovementTimeUCAsIUnitComponent) {
        } else if (detail is SummonAICapabilityUCAsIUnitComponent) {
        } else if (detail is BaseOffenseUCAsIUnitComponent) {
        } else if (detail is BaseDefenseUCAsIUnitComponent) {
        } else if (detail is SlowRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "w",
                    50,
                    new UnityEngine.Color(0, .5f, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Mire Staff"));
        } else if (detail is BlastRodAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "w",
                    50,
                    new UnityEngine.Color(1, .5f, 0, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Fire Rod"));
        } else if (detail is ArmorAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "0",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Igneous Armor"));
        } else if (detail is GlaiveAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "s",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Obsidian Sword"));
        } else if (detail is SpeedRingAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                new SymbolDescription(
                    "4",
                    50,
                    new UnityEngine.Color(1, 1, 1, 1.5f),
                    0,
                    OutlineMode.NoOutline,
                    new UnityEngine.Color(0, 0, 0)),
                "Ring of Speed"));
        } else if (detail is TemporaryCloneAICapabilityUCAsIUnitComponent) {
        } else if (detail is TutorialDefyCounterUCAsIUnitComponent) {
        } else if (detail is LightningChargingUCAsIUnitComponent) {
        } else if (detail is LightningChargedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "r-3",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Lightning Charged"));
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Previous Incarnation"));
        } else if (detail is DoomedUCAsIUnitComponent) {
          symbolsAndLabels.Add(
              new KeyValuePair<SymbolDescription, string>(
                  new SymbolDescription(
                      "k",
                    50,
                      new UnityEngine.Color(1, 1, 1, 1.5f),
                      0,
                      OutlineMode.NoOutline,
                      new UnityEngine.Color(0, 0, 0)),
                  "Previous Incarnation"));
        } else {
          Debug.LogError("Unknown detail type: " + detail);
        }
      }

      lookPanelView.SetStuff(true, unit.classId, unit.hp + " / " + unit.maxHp, symbolsAndLabels);

      unit.AddObserver(this);
    }

    public void OnUnitEffect(IUnitEffect effect) { effect.visit(this); }
    public void visitUnitCreateEffect(UnitCreateEffect effect) { }
    public void visitUnitSetAliveEffect(UnitSetAliveEffect effect) { }
    public void visitUnitSetHpEffect(UnitSetHpEffect effect) { }
    public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect effect) { }
    public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect effect) { }
    public void visitUnitSetLocationEffect(UnitSetLocationEffect effect) { }
    public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect effect) { }
    public void visitUnitDeleteEffect(UnitDeleteEffect effect) {
      if (!lookedUnit.Exists()) {
        Clear();
      }
    }
  }
}
