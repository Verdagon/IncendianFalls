using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DownStaircaseFeature {
  public readonly Root root;
  public readonly int id;
  public DownStaircaseFeature(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public DownStaircaseFeatureIncarnation incarnation { get { return root.GetDownStaircaseFeatureIncarnation(id); } }
  public void AddObserver(IDownStaircaseFeatureEffectObserver observer) {
    root.AddDownStaircaseFeatureObserver(id, observer);
  }
  public void RemoveObserver(IDownStaircaseFeatureEffectObserver observer) {
    root.RemoveDownStaircaseFeatureObserver(id, observer);
  }
  public void Delete() {
    root.EffectDownStaircaseFeatureDelete(id);
  }
  public static DownStaircaseFeature Null = new DownStaircaseFeature(null, 0);
  public bool Exists() { return root != null && root.DownStaircaseFeatureExists(id); }
  public bool NullableIs(DownStaircaseFeature that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(DownStaircaseFeature that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
