using System;
using System.Collections.Generic;
using UnityEngine;

namespace Domino {
  public class SymbolBarView : MonoBehaviour {
    private bool initialized = false;

    // An invisible object aligned with the top of the unit, which will contain
    // any detail symbols.
    // Lives inside this.body.
    // Nullable. Only non-null when there's details.
    // private GameObject gameObject;

    private IClock clock;
    private Instantiator instantiator;
    private List<KeyValuePair<ulong, SymbolView>> symbolsIdsAndViews;

    public void Init(
      IClock clock,
        Instantiator instantiator,
        List<(ulong, ExtrudedSymbolDescription)> newSymbolsIdsAndDescriptions) {
      this.initialized = true;
      this.clock = clock;
      this.instantiator = instantiator;
      this.symbolsIdsAndViews = new List<KeyValuePair<ulong, SymbolView>>();
      SetDescriptions(newSymbolsIdsAndDescriptions);
    }

    public void Destruct() {
      CheckInitialized();
      initialized = false;
      Destroy(gameObject);
    }

    public void CheckInitialized() {
      if (!initialized) {
        throw new System.Exception("SymbolBarView component not initialized!");
      }
    }

    public void Start() {
      CheckInitialized();
    }

    public void SetDescriptions(
        List<(ulong, ExtrudedSymbolDescription)> newSymbolsIdsAndDescriptions) {
      CheckInitialized();

      foreach (var entry in symbolsIdsAndViews) {
        entry.Value.Destruct();
      }
      symbolsIdsAndViews.Clear();

      int i = 0;
      foreach (var entry in newSymbolsIdsAndDescriptions) {
        var desiredSymbolView = instantiator.CreateSymbolView(clock, false, true, entry.Item2);
        desiredSymbolView.gameObject.transform.SetParent(gameObject.transform, false);
        symbolsIdsAndViews.Add(new KeyValuePair<ulong, SymbolView>(entry.Item1, desiredSymbolView));
        SetSymbolViewPosition(entry.Item1, desiredSymbolView, i++);
      }


      //var existingSymbolOldIndexById = new Dictionary<int, int>();
      //var existingSymbolViewById = new Dictionary<ulong, SymbolView>();
      //for (int i = 0; i < symbolsIdsAndViews.Count; i++) {
      //  existingSymbolViewById.Add(symbolsIdsAndViews[i].Key, symbolsIdsAndViews[i].Value);
      //  existingSymbolOldIndexById.Add(symbolsIdsAndViews[i].Key, i);
      //}

      //var newSymbolDescriptionIndexById = new Dictionary<int, int>();
      //for (int i = 0; i < newSymbolsIdsAndDescriptions.Count; i++) {
      //  newSymbolDescriptionIndexById.Add(newSymbolsIdsAndDescriptions[i].Key, i);
      //}

      //for (int i = 0; i < newSymbolsIdsAndDescriptions.Count; i++) {
      //  int desiredSymbolId = newSymbolsIdsAndDescriptions[i].Key;

      //  // By the end of this loop, symbolsIdsAndViews will contain at i either
      //  // nothing or the desired SymbolView for desiredSymbolId.
      //  while (true) {
      //    if (i >= symbolsIdsAndViews.Count) {
      //      // There's nothing in the existing views at this position.
      //      break;
      //    }

      //    int existingSymbolId = symbolsIdsAndViews[i].Key;
      //    if (existingSymbolId == desiredSymbolId) {
      //      // At this position, there's a SymbolView for the desired symbol id.
      //      break;
      //    }
      //    // At this position, there's a SymbolView for someone that's not the
      //    // desired symbol id.

      //    SymbolView existingSymbolView = symbolsIdsAndViews[i].Value;

      //    if (newSymbolDescriptionIndexById.ContainsKey(existingSymbolId)) {
      //      // The symbol already here is actually in the new symbols list, just
      //      // not at this position. Let's remove it from the symbolsIdsAndViews list.
      //      // Don't worry, it's still in the existingSymbolViewById list.
      //      symbolsIdsAndViews.RemoveAt(i);
      //    } else {
      //      // The symbol already here is actually nowhere in the new symbols list,
      //      // so let's delete it.
      //      existingSymbolView.Destruct();
      //      symbolsIdsAndViews.RemoveAt(i);
      //    }

      //    // continue
      //  }

      //  var desiredSymbolDescription = newSymbolsIdsAndDescriptions[i].Value;

      //  SymbolView desiredSymbolView;
      //  if (i >= symbolsIdsAndViews.Count) {
      //    if (existingSymbolViewById.ContainsKey(desiredSymbolId)) {
      //      // We had a symbol, but it wasn't in the right order so it got kicked out of the list.
      //      desiredSymbolView = existingSymbolViewById[desiredSymbolId];
      //      // Now we can add it back into the list.
      //      symbolsIdsAndViews.Add(new KeyValuePair<ulong, SymbolView>(desiredSymbolId, desiredSymbolView));
      //      // Update it's description; we have to do it sometime, here's the best place.
      //      desiredSymbolView.SetDescription(desiredSymbolDescription);
      //    } else {
      //      // Then this is a completely new symbol. Let's make one and add it to the list.
      //      desiredSymbolView = instantiator.CreateSymbolView(false, desiredSymbolDescription);
      //      desiredSymbolView.gameObject.transform.SetParent(gameObject.transform, false);
      //      symbolsIdsAndViews.Add(new KeyValuePair<ulong, SymbolView>(desiredSymbolId, desiredSymbolView));
      //    }
      //  } else {
      //    if (symbolsIdsAndViews[i].Key != desiredSymbolId)
      //      throw new Exception("wat"); // Shouldn't happen.
      //    // The symbol view already there should be for this symbol.
      //    desiredSymbolView = symbolsIdsAndViews[i].Value;
      //    // Update it's description; we have to do it sometime, here's the best place.
      //    desiredSymbolView.SetDescription(desiredSymbolDescription);
      //  }

      //  if (existingSymbolOldIndexById.ContainsKey(desiredSymbolId)) {
      //    int symbolOldIndex = existingSymbolOldIndexById[desiredSymbolId];
      //    if (symbolOldIndex != i) {
      //      UpdateSymbolViewPosition(desiredSymbolId, desiredSymbolView, i);
      //    }
      //  } else {
      //    SetSymbolViewPosition(desiredSymbolId, desiredSymbolView, i);
      //  }
      //}
    }

    //private void UpdateSymbolViewPosition(int symbolId, SymbolView symbolView, int index) {
    //  Matrix4x4 matrix = GetMatrixForPosition(index);
    //  symbolView.gameObject.transform.FromMatrix(matrix);

    //  // soon, we should add some animating right here
    //}

    private void SetSymbolViewPosition(ulong symbolId, SymbolView symbolView, int index) {
      //Matrix4x4 matrix = GetMatrixForPosition(index);
      //symbolView.gameObject.transform.FromMatrix(matrix);

      symbolView.gameObject.transform.localScale =
          new Vector3(-1 / 3f, 1 / 3f, -0.01f);
      symbolView.gameObject.transform.localPosition =
          new Vector3((index * 2 - 2) / 6.0f, 0, 0);


      //// Now flatten it.
      //transformForPosition.Scale(new Vector3(1.0f, 1.0f, 0.01f));
      //// Center it.
      //transformForPosition.Translate(new Vector3(-0.5f, -0.5f, 0));
      //// Shrink it down a bit.
      //transformForPosition.Scale(new Vector3(1.0f / 3, 1.0f / 3, 1.0f));

      //transformForPosition.Translate(new Vector3(index * 1.0f / 3, 0, 0));
      //transformForPosition.Translate(new Vector3(-1.0f / 6, 0, 0));

    }

  }
}
