using System;
using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;

namespace Domino {
  public class TileDescription {
    public readonly int id;
    public readonly float elevationStepHeight;
    public readonly float tileRotationDegrees;
    public readonly int depth; // basically elevation
    public readonly ExtrudedSymbolDescription tileSymbolDescription;
    public readonly ExtrudedSymbolDescription maybeOverlaySymbolDescription;
    public readonly ExtrudedSymbolDescription maybeFeatureSymbolDescription;
    public readonly SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId;

    public TileDescription(
        float elevationStepHeight,
        float tileRotationDegrees,
        int depth,
        ExtrudedSymbolDescription tileSymbolDescription,
        ExtrudedSymbolDescription maybeOverlaySymbolDescription,
        ExtrudedSymbolDescription maybeFeatureSymbolDescription,
        SortedDictionary<int, ExtrudedSymbolDescription> itemSymbolDescriptionByItemId) {
      this.elevationStepHeight = elevationStepHeight;
      this.tileRotationDegrees = tileRotationDegrees;
      this.depth = depth;
      this.tileSymbolDescription = tileSymbolDescription;
      this.maybeOverlaySymbolDescription = maybeOverlaySymbolDescription;
      this.maybeFeatureSymbolDescription = maybeFeatureSymbolDescription;
      this.itemSymbolDescriptionByItemId = itemSymbolDescriptionByItemId;
    }

    public TileDescription WithTileSymbolDescription(ExtrudedSymbolDescription newTileSymbolDescription) {
      return new TileDescription(
        elevationStepHeight,
        tileRotationDegrees,
        depth,
        newTileSymbolDescription,
        maybeOverlaySymbolDescription,
        maybeFeatureSymbolDescription,
        itemSymbolDescriptionByItemId);
    }

    public override bool Equals(object obj) {
      if (!(obj is TileDescription))
        return false;
      TileDescription that = obj as TileDescription;
      if (elevationStepHeight != that.elevationStepHeight)
        return false;
      if (tileRotationDegrees != that.tileRotationDegrees)
        return false;
      if (depth != that.depth)
        return false;
      if (!tileSymbolDescription.Equals(that.tileSymbolDescription))
        return false;
      if ((maybeOverlaySymbolDescription != null) != (that.maybeOverlaySymbolDescription != null))
        return false;
      if (maybeOverlaySymbolDescription != null && !maybeOverlaySymbolDescription.Equals(that.maybeOverlaySymbolDescription))
        return false;
      if ((maybeFeatureSymbolDescription != null) != (that.maybeFeatureSymbolDescription != null))
        return false;
      if (maybeFeatureSymbolDescription != null && !maybeFeatureSymbolDescription.Equals(that.maybeFeatureSymbolDescription))
        return false;
      if (itemSymbolDescriptionByItemId.Count != that.itemSymbolDescriptionByItemId.Count)
        return false;
      foreach (var thisEntry in itemSymbolDescriptionByItemId) {
        if (!that.itemSymbolDescriptionByItemId.ContainsKey(thisEntry.Key))
          return false;
        var thatValue = that.itemSymbolDescriptionByItemId[thisEntry.Key];
        if (!thisEntry.Value.Equals(thatValue))
          return false;
      }
      return true;
    }
    public override int GetHashCode() {
      int hashCode = 0;
      hashCode += 27 * elevationStepHeight.GetHashCode();
      hashCode += 31 * tileRotationDegrees.GetHashCode();
      hashCode += 37 * depth.GetHashCode();
      hashCode += 41 * tileSymbolDescription.GetHashCode();
      if (maybeOverlaySymbolDescription != null)
        hashCode += 47 * maybeOverlaySymbolDescription.GetHashCode();
      if (maybeFeatureSymbolDescription != null)
        hashCode += 53 * maybeFeatureSymbolDescription.GetHashCode();
      hashCode += 67 * itemSymbolDescriptionByItemId.Count;
      foreach (var entry in itemSymbolDescriptionByItemId) {
        hashCode += 87 * entry.Key.GetHashCode() + 93 * entry.Value.GetHashCode();
      }
      return hashCode;
    }
  }

  public class TileView : MonoBehaviour {
    private bool initialized = false;
    public bool alive {  get { return initialized;  } }

    private IClock clock;
    private ITimer timer;

    private List<SymbolView> tileSymbolViews = new List<SymbolView>();

    private SymbolView overlaySymbolView;

    private SymbolView featureSymbolView;

    private SortedDictionary<int, SymbolView> itemSymbolViewByItemId = new SortedDictionary<int, SymbolView>();

    Instantiator instantiator;

    public void Init(
      IClock clock,
      ITimer timer,
        Instantiator instantiator,
        Vector3 basePosition,
        TileDescription tileDescription) {
      this.clock = clock;
      this.timer = timer;
      this.instantiator = instantiator;

      gameObject.transform.localPosition = basePosition;

      initialized = true;
    }

    public void SetDescription(TileDescription newTileDescription) {
      foreach (var tileSymbolView in tileSymbolViews) {
        tileSymbolView.Destruct();
      }
      tileSymbolViews.Clear();

      if (overlaySymbolView != null) {
        overlaySymbolView.Destruct();
      }

      if (featureSymbolView != null) {
        featureSymbolView.Destruct();
      }

      foreach (var entry in itemSymbolViewByItemId) {
        entry.Value.Destruct();
      }
      itemSymbolViewByItemId.Clear();

      SetStuff(newTileDescription);
    }

    public void DestroyTile() {
      initialized = false;
      Destroy(this.gameObject);
    }

    private void SetStuff(TileDescription tileDescription) {
      for (int i = 0; i < tileDescription.depth; i++) {
        SymbolView tileSymbolView =
            instantiator.CreateSymbolView(clock, true, tileDescription.tileSymbolDescription);
        //MatrixBuilder tileSymbolMatrixBuilder = new MatrixBuilder(Matrix4x4.identity);

        // No idea why we need the -90 or the - before the rotation. It has to do with
        // unity's infuriating mishandling of .obj file imports.
        tileSymbolView.gameObject.transform.localRotation =
            Quaternion.Euler(new Vector3(-90, -tileDescription.tileRotationDegrees, 0));
        tileSymbolView.gameObject.transform.localScale =
            new Vector3(1, -1, tileDescription.elevationStepHeight);
        tileSymbolView.gameObject.transform.localPosition =
            new Vector3(0, -tileDescription.elevationStepHeight * i);

        tileSymbolView.gameObject.transform.SetParent(transform, false);

        tileSymbolViews.Add(tileSymbolView);
      }

      if (tileDescription.maybeOverlaySymbolDescription != null) {
        var description = tileDescription.maybeOverlaySymbolDescription;
        overlaySymbolView = instantiator.CreateSymbolView(clock, false, description);

        overlaySymbolView.gameObject.transform.localPosition =
            new Vector3(0, .01f, 0);
        overlaySymbolView.gameObject.transform.localRotation =
            Quaternion.Euler(new Vector3(-90, 0, 0));
        overlaySymbolView.gameObject.transform.localScale =
          new Vector3(
              -1 * 0.707f,
              -1 * 0.707f,
              1);

        overlaySymbolView.gameObject.transform.SetParent(transform, false);
      }

      if (tileDescription.maybeFeatureSymbolDescription != null) {
        var description = tileDescription.maybeFeatureSymbolDescription;
        featureSymbolView = instantiator.CreateSymbolView(clock, false, description);

        featureSymbolView.gameObject.transform.localPosition =
            new Vector3(0, .28f, .15f);
        featureSymbolView.gameObject.transform.localRotation =
            Quaternion.Euler(new Vector3(180 + 50, 0f, 0f));
        featureSymbolView.gameObject.transform.localScale =
          new Vector3(-.8f, -.8f, .1f);

        featureSymbolView.gameObject.transform.SetParent(transform, false);
      }

      int itemIndex = 0;
      foreach (var entry in tileDescription.itemSymbolDescriptionByItemId) {
        var description = entry.Value;
        var itemSymbolView = instantiator.CreateSymbolView(clock, false, description);

        float inscribeCircleRadius = 0.75f; // chosen cuz it looks about right
                                            // https://math.stackexchange.com/questions/666491/three-circles-within-a-larger-circle
        float itemRadius = (-3 + 2 * 1.732f) * inscribeCircleRadius;

        float itemCenterXFromTileCenter = 0;
        float itemCenterYFromTileCenter = 0;

        if (tileDescription.itemSymbolDescriptionByItemId.Count == 1) {
          itemCenterXFromTileCenter = 0;
          itemCenterYFromTileCenter = 0;
        } else if (tileDescription.itemSymbolDescriptionByItemId.Count == 2) {
          if (itemIndex == 0) {
            itemCenterXFromTileCenter = -itemRadius / 2;
            itemCenterYFromTileCenter = 0;
          } else {
            itemCenterXFromTileCenter = itemRadius / 2;
            itemCenterYFromTileCenter = 0;
          }
        } else {
          // 0.866 is cos(30)
          // I don't know why we need that / 2 there.
          float itemCenterDistanceToTileCenter = itemRadius / 0.866f / 2;

          if (itemIndex == 0) {
            itemCenterXFromTileCenter = 0;
            itemCenterYFromTileCenter = itemCenterDistanceToTileCenter;
          } else if (itemIndex == 1) {
            // 0.866 is cos(30)
            itemCenterXFromTileCenter = 0.866f * itemCenterDistanceToTileCenter;
            // 0.5f is sin(30)
            itemCenterYFromTileCenter = -0.5f * itemCenterDistanceToTileCenter;
          } else if (itemIndex == 2) {
            // 0.866 is cos(30)
            itemCenterXFromTileCenter = -0.866f * itemCenterDistanceToTileCenter;
            // 0.5f is sin(30)
            itemCenterYFromTileCenter = -0.5f * itemCenterDistanceToTileCenter;
          }

          // TODO: adjust upward if the unit is on the tile
          itemCenterYFromTileCenter += 0;
        }

        itemSymbolView.gameObject.transform.localPosition =
            new Vector3(
                itemCenterXFromTileCenter,
                .05f,
                itemCenterYFromTileCenter);
        itemSymbolView.gameObject.transform.localRotation =
            Quaternion.Euler(new Vector3(-90, 0f, 0));
        itemSymbolView.gameObject.transform.localScale =
          new Vector3(
              -1 * itemRadius,
              -1 * itemRadius,
              .1f);

        itemSymbolView.gameObject.transform.SetParent(transform, false);

        itemSymbolViewByItemId.Add(entry.Key, itemSymbolView);

        itemIndex++;
        if (itemIndex == 4) {
          break;
        }
      }

    }

    public void ShowRune(ExtrudedSymbolDescription runeSymbolDescription) {
      var symbolView = instantiator.CreateSymbolView(clock, false, runeSymbolDescription);
      symbolView.transform.localRotation = Quaternion.Euler(new Vector3(-50, 180, 0));
      symbolView.transform.localScale = new Vector3(1, 1, .1f);
      symbolView.transform.localPosition = new Vector3(0, 0.5f, -.2f);
      symbolView.transform.SetParent(gameObject.transform, false);
      symbolView.FadeInThenOut(100, 400);
      timer.ScheduleTimer(1000, () => {
        if (alive) {
          symbolView.Destruct();
        }
      });
    }

    public void Start() {
      if (!initialized) {
        throw new Exception("TileView component not initialized!");
      }
    }
  }
}