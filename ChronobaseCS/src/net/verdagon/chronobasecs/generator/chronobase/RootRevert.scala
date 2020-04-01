package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS;

object RootRevert {
  def generateRootRevertMethod(
                                opt: ChronobaseOptions,
                                ss: SuperstructureS,
  ): String = {

    val structTypeNames = ss.structs.filter(_.mutability == MutableS).map(_.name)
    val listTypeNames = ss.lists.filter(_.mutability == MutableS).map(_.name)
    val setTypeNames = ss.sets.filter(_.mutability == MutableS).map(_.name)
    val mapTypeNames = ss.maps.filter(_.mutability == MutableS).map(_.name)

    val instanceTypeNames = structTypeNames ++ listTypeNames ++ setTypeNames ++ mapTypeNames

    s"""
       |  public void Revert(RootIncarnation sourceIncarnation) {
       |    CheckUnlocked();
       |    // We do all the adds first so that we don't violate any strong borrows.
       |    // Then we do all the changes, because those might be flipping things to point
       |    // at things that were just made.
       |    // Then we do all the removes.
       |
       |    // We collect these and then flush them at the end, because there are some
       |    // cases where we want to fill different kinds of events at the same time.
       |    // For example, when creating a set that was in the sourceIncarnation but
       |    // not in the current one, we want to reuse the set's incarnation, which
       |    // is a create and a bunch of adds. We populate both those at the same time,
       |    // and then flush all the creates before all the adds.
       |    var createEffects = new List<IEffect>();
       |    var addEffects = new List<IEffect>();
       |    var setEffects = new List<IEffect>();
       |    var removeEffects = new List<IEffect>();
       |    var deleteEffects = new List<IEffect>();
       |
       |""".stripMargin +
      ss.structs
        .filter(_.mutability == MutableS)
        .map({ case StructS(structName, _, mutability, members, _, _, _) =>
          s"""
             |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${structName}) {
             |      var objId = sourceIdAndVersionAndObjIncarnation.Key;
             |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
             |      var sourceVersion = sourceVersionAndObjIncarnation.version;
             |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
             |      if (rootIncarnation.incarnations${structName}.ContainsKey(objId)) {
             |        // Compare everything that could possibly have changed.
             |        var currentVersionAndObjIncarnation = rootIncarnation.incarnations${structName}[objId];
             |        var currentVersion = currentVersionAndObjIncarnation.version;
             |        var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
             |        if (currentVersion != sourceVersion) {
             |""".stripMargin +
            members.map({
              case StructMemberS(memberName, FinalS, memberType) => ""
              case StructMemberS(memberName, VaryingS, memberType) => {
                s"""
                   |          if (sourceObjIncarnation.${memberName} != currentObjIncarnation.${memberName}) {
                   |            setEffects.Add(new ${structName}Set${memberName.capitalize}Effect(objId, sourceObjIncarnation.${memberName}));
                   |          }
                   |""".stripMargin
              }
            }).mkString("") +
            s"""
               |          // Swap out the underlying incarnation.
               |          """.stripMargin +
            (if (opt.hash) {
              s"""
                 |            rootIncarnation.hash -=
                 |                Get${structName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${structName}[objId].version,
                 |                    rootIncarnation.incarnations${structName}[objId].incarnation);
                 |""".stripMargin
            } else "") +
            s"""
               |          rootIncarnation.incarnations${structName}[objId] = sourceVersionAndObjIncarnation;
               |          """.stripMargin +
            (if (opt.hash) {
              s"""
                 |            rootIncarnation.hash +=
                 |                Get${structName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${structName}[objId].version,
                 |                    rootIncarnation.incarnations${structName}[objId].incarnation);
                 |""".stripMargin
            } else "") +
            s"""
               |        }
               |      }
               |    }
               |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${structName}) {
               |      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
               |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
               |      var sourceVersion = sourceVersionAndObjIncarnation.version;
               |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
               |      if (!rootIncarnation.incarnations${structName}.ContainsKey(sourceObjId)) {
               |        var effect = InternalEffectCreate${structName}(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation.Copy());
               |        createEffects.Add(effect);
               |      }
               |    }
             |    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<${structName}Incarnation>>(rootIncarnation.incarnations${structName})) {
             |      if (!sourceIncarnation.incarnations${structName}.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
             |        var id = currentIdAndVersionAndObjIncarnation.Key;
             |        var effect = InternalEffect${structName}Delete(id);
             |        deleteEffects.Add(effect);
             |      }
             |    }
             |""".stripMargin
        })
        .mkString("") +
    ss.lists
        .filter(_.mutability == MutableS)
        .map({ case ListS(listName, mutability, elementType) =>
          s"""
               |      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${listName}) {
               |        var objId = sourceIdAndVersionAndObjIncarnation.Key;
               |        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
               |        var sourceVersion = sourceVersionAndObjIncarnation.version;
               |        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
               |        if (rootIncarnation.incarnations${listName}.ContainsKey(objId)) {
               |          // Compare everything that could possibly have changed.
               |          var currentVersionAndObjIncarnation = rootIncarnation.incarnations${listName}[objId];
               |          var currentVersion = currentVersionAndObjIncarnation.version;
               |          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
               |          if (currentVersion != sourceVersion) {
               |            for (int i = currentObjIncarnation.elements.Count - 1; i >= 0; i--) {
               |              removeEffects.Add(new ${listName}RemoveEffect(objId, i));
               |            }
               |            for (int i = 0; i < sourceObjIncarnation.elements.Count; i++) {
               |              addEffects.Add(new ${listName}AddEffect(objId, i, sourceObjIncarnation.elements[i]));
               |            }
               |            // Swap out the underlying incarnation.
               |""".stripMargin +
            (if (opt.hash) {
              s"""      rootIncarnation.hash -=
                 |                Get${listName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${listName}[objId].version,
                 |                    rootIncarnation.incarnations${listName}[objId].incarnation);
                 |""".stripMargin
            } else "") +
            s"""                  rootIncarnation.incarnations${listName}[objId] = sourceVersionAndObjIncarnation;
               |""".stripMargin +
            (if (opt.hash) {
              s"""            rootIncarnation.hash +=
                 |                Get${listName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${listName}[objId].version,
                 |                    rootIncarnation.incarnations${listName}[objId].incarnation);
               """.stripMargin
            } else "") +
            s"""
               |          }
               |        }
               |      }
               |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${listName}) {
               |      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
               |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
               |      var sourceVersion = sourceVersionAndObjIncarnation.version;
               |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
               |      if (!rootIncarnation.incarnations${listName}.ContainsKey(sourceObjId)) {
               |        var createEffect = InternalEffectCreate${listName}(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
               |        createEffects.Add(createEffect);
               |        for (int i = 0; i < sourceObjIncarnation.elements.Count; i++) {
               |          addEffects.Add(new ${listName}AddEffect(sourceObjId, i, sourceObjIncarnation.elements[i]));
               |        }
               |      }
               |    }
             |    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<${listName}Incarnation>>(rootIncarnation.incarnations${listName})) {
             |      if (!sourceIncarnation.incarnations${listName}.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
             |        var id = currentIdAndVersionAndObjIncarnation.Key;
             |        var currentObjIncarnation = currentIdAndVersionAndObjIncarnation.Value.incarnation;
               |        for (int i = currentObjIncarnation.elements.Count - 1; i >= 0; i--) {
               |          removeEffects.Add(new ${listName}RemoveEffect(id, i));
               |        }
             |        var effect = InternalEffect${listName}Delete(id);
             |        deleteEffects.Add(effect);
             |      }
             |    }
             |""".stripMargin
        })
        .mkString("") +
      ss.sets
        .filter(_.mutability == MutableS)
        .map({ case SetS(setName, mutability, elementType) =>
          s"""
             |      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${setName}) {
             |        var objId = sourceIdAndVersionAndObjIncarnation.Key;
             |        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
             |        var sourceVersion = sourceVersionAndObjIncarnation.version;
             |        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
             |        if (rootIncarnation.incarnations${setName}.ContainsKey(objId)) {
             |          // Compare everything that could possibly have changed.
             |          var currentVersionAndObjIncarnation = rootIncarnation.incarnations${setName}[objId];
             |          var currentVersion = currentVersionAndObjIncarnation.version;
             |          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
             |          if (currentVersion != sourceVersion) {
             |            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.elements)) {
             |              if (!sourceObjIncarnation.elements.Contains(objIdInCurrentObjIncarnation)) {
             |                removeEffects.Add(new ${setName}RemoveEffect(objId, objIdInCurrentObjIncarnation));
             |              }
             |            }
             |            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.elements) {
             |              if (!currentObjIncarnation.elements.Contains(unitIdInSourceObjIncarnation)) {
             |                addEffects.Add(new ${setName}AddEffect(objId, unitIdInSourceObjIncarnation));
             |              }
             |            }
             |            // Swap out the underlying incarnation.
             |""".stripMargin +
          (if (opt.hash) {
            s"""            rootIncarnation.hash -=
                 |                Get${setName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${setName}[objId].version,
                 |                    rootIncarnation.incarnations${setName}[objId].incarnation);
                 |
               |""".stripMargin
          } else "") +
          s"""            rootIncarnation.incarnations${setName}[objId] = sourceVersionAndObjIncarnation;
                 |""".stripMargin +
          (if (opt.hash) {
          s"""            rootIncarnation.hash +=
             |                Get${setName}Hash(
             |                    objId,
             |                    rootIncarnation.incarnations${setName}[objId].version,
             |                    rootIncarnation.incarnations${setName}[objId].incarnation);
             |
             |""".stripMargin
          } else "") +
          s"""          }
             |        }
             |      }
             |
             |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${setName}) {
             |      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
             |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
             |      var sourceVersion = sourceVersionAndObjIncarnation.version;
             |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
             |      if (!rootIncarnation.incarnations${setName}.ContainsKey(sourceObjId)) {
             |        var createEffect = InternalEffectCreate${setName}(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
             |        createEffects.Add(createEffect);
             |        foreach (var element in sourceObjIncarnation.elements) {
             |          addEffects.Add(new ${setName}AddEffect(sourceObjId, element));
             |        }
             |      }
             |    }
             |    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<${setName}Incarnation>>(rootIncarnation.incarnations${setName})) {
             |      if (!sourceIncarnation.incarnations${setName}.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
             |        var id = currentIdAndVersionAndObjIncarnation.Key;
             |        var currentObjIncarnation = currentIdAndVersionAndObjIncarnation.Value.incarnation;
             |        foreach (var element in currentObjIncarnation.elements) {
             |          removeEffects.Add(new ${setName}RemoveEffect(id, element));
             |        }
             |        var effect = InternalEffect${setName}Delete(id);
             |        deleteEffects.Add(effect);
             |      }
             |    }
             |""".stripMargin
        })
        .mkString("") +
      ss.maps
        .filter(_.mutability == MutableS)
        .map({ case MapS(mapName, mutability, keyType, elementType) =>
          val keyCSType = toCS(keyType)
          val elementCSType = toCS(elementType)
          val flattenedKeyCSType = toCS(keyType.flatten)
          val flattenedElementCSType = toCS(elementType.flatten)

          s"""
             |      foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${mapName}) {
             |        var objId = sourceIdAndVersionAndObjIncarnation.Key;
             |        var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
             |        var sourceVersion = sourceVersionAndObjIncarnation.version;
             |        var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
             |        if (rootIncarnation.incarnations${mapName}.ContainsKey(objId)) {
             |          // Compare everything that could possibly have changed.
             |          var currentVersionAndObjIncarnation = rootIncarnation.incarnations${mapName}[objId];
             |          var currentVersion = currentVersionAndObjIncarnation.version;
             |          var currentObjIncarnation = currentVersionAndObjIncarnation.incarnation;
             |          if (currentVersion != sourceVersion) {
             |            foreach (var entryInCurrentObjIncarnation in new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(currentObjIncarnation.elements)) {
             |              var key = entryInCurrentObjIncarnation.Key;
             |              if (!sourceObjIncarnation.elements.ContainsKey(key)) {
             |                removeEffects.Add(new ${mapName}RemoveEffect(objId, key));
             |              }
             |            }
             |            foreach (var entryInSourceObjIncarnation in sourceObjIncarnation.elements) {
             |              var key = entryInSourceObjIncarnation.Key;
             |              var element = entryInSourceObjIncarnation.Value;
             |              if (!currentObjIncarnation.elements.ContainsKey(key)) {
             |                addEffects.Add(new ${mapName}AddEffect(objId, key, element));
             |              }
             |            }
             |            // Swap out the underlying incarnation.
             |""".stripMargin +
          (if (opt.hash) {
            s"""            rootIncarnation.hash -=
                 |                Get${mapName}Hash(
                 |                    objId,
                 |                    rootIncarnation.incarnations${mapName}[objId].version,
                 |                    rootIncarnation.incarnations${mapName}[objId].incarnation);
                 |
                 |""".stripMargin
          } else "") +
          s"""            rootIncarnation.incarnations${mapName}[objId] = sourceVersionAndObjIncarnation;
                 |""".stripMargin +
          (if (opt.hash) {
          s"""            rootIncarnation.hash +=
             |                Get${mapName}Hash(
             |                    objId,
             |                    rootIncarnation.incarnations${mapName}[objId].version,
             |                    rootIncarnation.incarnations${mapName}[objId].incarnation);
             |
             |""".stripMargin
          } else "") +
          s"""          }
             |        }
             |      }
             |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${mapName}) {
             |      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
             |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
             |      var sourceVersion = sourceVersionAndObjIncarnation.version;
             |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
             |      if (!rootIncarnation.incarnations${mapName}.ContainsKey(sourceObjId)) {
             |        var createEffect = InternalEffectCreate${mapName}(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
             |        createEffects.Add(createEffect);
             |        foreach (var keyAndElement in sourceObjIncarnation.elements) {
             |          addEffects.Add(new ${mapName}AddEffect(sourceObjId, keyAndElement.Key, keyAndElement.Value));
             |        }
             |      }
             |    }
             |    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<${mapName}Incarnation>>(rootIncarnation.incarnations${mapName})) {
             |      if (!sourceIncarnation.incarnations${mapName}.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
             |        var id = currentIdAndVersionAndObjIncarnation.Key;
             |        var currentObjIncarnation = currentIdAndVersionAndObjIncarnation.Value.incarnation;
             |        foreach (var keyAndElement in currentObjIncarnation.elements) {
             |          removeEffects.Add(new ${mapName}RemoveEffect(id, keyAndElement.Key));
             |        }
             |        var effect = InternalEffect${mapName}Delete(id);
             |        deleteEffects.Add(effect);
             |      }
             |    }
             """.stripMargin
        })
        .mkString("") +
      s"""
         |    foreach (var effect in createEffects) {
         |      NotifyEffect(effect);
         |    }
         |    foreach (var effect in addEffects) {
         |      NotifyEffect(effect);
         |    }
         |    foreach (var effect in setEffects) {
         |      NotifyEffect(effect);
         |    }
         |    foreach (var effect in removeEffects) {
         |      NotifyEffect(effect);
         |    }
         |    foreach (var effect in deleteEffects) {
         |      NotifyEffect(effect);
         |    }
         |  }
     """.stripMargin
  }
}
