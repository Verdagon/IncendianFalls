using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DecorativeFeature {
  public readonly Root root;
  public readonly int id;
  public DecorativeFeature(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DecorativeFeatureIncarnation incarnation { get { return root.GetDecorativeFeatureIncarnation(id); } }
  public void AddObserver(IDecorativeFeatureEffectObserver observer) {
    root.AddDecorativeFeatureObserver(id, observer);
  }
  public void RemoveObserver(IDecorativeFeatureEffectObserver observer) {
    root.RemoveDecorativeFeatureObserver(id, observer);
  }
  public void Delete() {
    root.EffectDecorativeFeatureDelete(id);
  }
  public static DecorativeFeature Null = new DecorativeFeature(null, 0);
  public bool Exists() { return root != null && root.DecorativeFeatureExists(id); }
  public bool NullableIs(DecorativeFeature that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(DecorativeFeature that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public string symbolId {
    get { return incarnation.symbolId; }
  }
}
}
