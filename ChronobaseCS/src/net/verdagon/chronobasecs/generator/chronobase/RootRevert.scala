package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS;

object RootRevert {
  def generateRootRevertMethod(
                                opt: ChronobaseOptions,
                                ss: SuperstructureS,
  ): String = {

    val instanceTypeNames =
      ss.structs.filter(_.mutability == MutableS).map(_.name) ++
        ss.lists.filter(_.mutability == MutableS).map(_.name) ++
        ss.sets.filter(_.mutability == MutableS).map(_.name) ++
        ss.maps.filter(_.mutability == MutableS).map(_.name)

    s"""
       |  public void Revert(RootIncarnation sourceIncarnation) {
       |    CheckUnlocked();
       |    // We do all the adds first so that we don't violate any strong borrows.
       |    // Then we do all the changes, because those might be flipping things to point
       |    // at things that were just made.
       |    // Then we do all the removes.
       |
       |""".stripMargin +
      instanceTypeNames.map(structName => {
        s"""
           |    foreach (var sourceIdAndVersionAndObjIncarnation in sourceIncarnation.incarnations${structName}) {
           |      var sourceObjId = sourceIdAndVersionAndObjIncarnation.Key;
           |      var sourceVersionAndObjIncarnation = sourceIdAndVersionAndObjIncarnation.Value;
           |      var sourceVersion = sourceVersionAndObjIncarnation.version;
           |      var sourceObjIncarnation = sourceVersionAndObjIncarnation.incarnation;
           |      if (!rootIncarnation.incarnations${structName}.ContainsKey(sourceObjId)) {
           |        EffectInternalCreate${structName}(sourceObjId, sourceVersionAndObjIncarnation.version, sourceObjIncarnation);
           |      }
           |    }
         """.stripMargin
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
               |            for (int i = currentObjIncarnation.list.Count - 1; i >= 0; i--) {
               |              Effect${listName}RemoveAt(objId, i);
               |            }
               |            for (int i = 0; i < sourceObjIncarnation.list.Count; i++) {
               |              Effect${listName}Add(objId, i, sourceObjIncarnation.list[i]);
               |            }
               |            // Swap out the underlying incarnation. The only visible effect this has is
               |            // changing the version number.
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
             """.stripMargin
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
             |            foreach (var objIdInCurrentObjIncarnation in new SortedSet<int>(currentObjIncarnation.set)) {
             |              if (!sourceObjIncarnation.set.Contains(objIdInCurrentObjIncarnation)) {
             |                Effect${setName}Remove(objId, objIdInCurrentObjIncarnation);
             |              }
             |            }
             |            foreach (var unitIdInSourceObjIncarnation in sourceObjIncarnation.set) {
             |              if (!currentObjIncarnation.set.Contains(unitIdInSourceObjIncarnation)) {
             |                Effect${setName}Add(objId, unitIdInSourceObjIncarnation);
             |              }
             |            }
             |            // Swap out the underlying incarnation. The only visible effect this has is
             |            // changing the version number.
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
             """.stripMargin
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
             |            foreach (var entryInCurrentObjIncarnation in new SortedDictionary<${flattenedKeyCSType}, ${flattenedElementCSType}>(currentObjIncarnation.map)) {
             |              var key = entryInCurrentObjIncarnation.Key;
             |              if (!sourceObjIncarnation.map.ContainsKey(key)) {
             |                Effect${mapName}Remove(objId, key);
             |              }
             |            }
             |            foreach (var entryInSourceObjIncarnation in sourceObjIncarnation.map) {
             |              var key = entryInSourceObjIncarnation.Key;
             |              var element = entryInSourceObjIncarnation.Value;
             |              if (!currentObjIncarnation.map.ContainsKey(key)) {
             |                Effect${mapName}Add(objId, key, element);
             |              }
             |            }
             |            // Swap out the underlying incarnation. The only visible effect this has is
             |            // changing the version number.
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
             """.stripMargin
        })
        .mkString("") +
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
                memberType match {
                  case x if x.kind.mutability == ImmutableS => {
                    s"""
                       |          if (sourceObjIncarnation.${memberName} != currentObjIncarnation.${memberName}) {
                       |            Effect${structName}Set${memberName.capitalize}(objId, sourceObjIncarnation.${memberName});
                       |          }
                       |""".stripMargin
                  }
                  case TypeS(_, _, interface @ InterfaceKindS(_, _)) => {
                    s"""
                       |          if (sourceObjIncarnation.${memberName} != currentObjIncarnation.${memberName}) {
                       |            Effect${structName}Set${memberName.capitalize}(objId, Get${toCS(memberType)}(sourceObjIncarnation.${memberName}));
                       |          }
                       |""".stripMargin
                  }
                  case _ => {
                    val memberCSType = toCS(memberType)
                    s"""
                       |          if (sourceObjIncarnation.${memberName} != currentObjIncarnation.${memberName}) {
                       |            Effect${structName}Set${memberName.capitalize}(objId, new ${memberCSType}(this, sourceObjIncarnation.${memberName}));
                       |          }
                       |""".stripMargin
                  }
                }
              }
            }).mkString("") +
            s"""
               |          // Swap out the underlying incarnation. The only visible effect this has is
               |          // changing the version number.
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
               |""".stripMargin
        })
        .mkString("") +
      instanceTypeNames.map(structName => {
        s"""
           |    foreach (var currentIdAndVersionAndObjIncarnation in new SortedDictionary<int, VersionAndIncarnation<${structName}Incarnation>>(rootIncarnation.incarnations${structName})) {
           |      if (!sourceIncarnation.incarnations${structName}.ContainsKey(currentIdAndVersionAndObjIncarnation.Key)) {
           |        var id = currentIdAndVersionAndObjIncarnation.Key;
           |        Effect${structName}Delete(id);
           |      }
           |    }
           |""".stripMargin
      })
        .mkString("") +
      s"""
         |    logger.Error("after reverting next id " + rootIncarnation.nextId);
         |  }
     """.stripMargin
  }
}
