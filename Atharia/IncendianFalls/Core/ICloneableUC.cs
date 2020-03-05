using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface ICloneableUC {
  ICloneableUC AsICloneableUC();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(ICloneableUC that);
  bool NullableIs(ICloneableUC that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  ICloneableUC ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
