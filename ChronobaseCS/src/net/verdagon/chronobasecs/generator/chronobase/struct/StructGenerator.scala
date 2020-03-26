package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled.{ImmutableS, MutableS, StructS, SuperstructureS}
import net.verdagon.chronobasecs.generator._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object StructGenerator {
  def generateHandles(opt: ChronobaseOptions, ss: SuperstructureS, struct: StructS): Map[String, String] = {
    struct.mutability match {
      case MutableS => {
        MutStructHandle.generateInstance(opt, struct, ss.bunches.filter(_.struct == struct.tyype)) ++
        Incarnation.generateIncarnation(opt, struct) ++
        MutStructEffects.generateEffects(opt, struct) ++
        struct.impls.flatMap(impl => {
          val methods = impl.methods.map(signature => ss.functions.find(_.signature == signature).get).distinct
          MutStructImpl.generateStructImpl(opt, struct, impl, methods)
        })
      }
      case ImmutableS => {
        ImmStruct.generateValue(opt, struct) ++
        struct.impls.flatMap(impl => {
          val methods = impl.methods.map(signature => ss.functions.find(_.signature == signature).get).distinct
          ImmStructImpl.generateImmStructImpl(opt, struct, impl, methods)
        })
      }
    }
  }

  def generateRootMethods(opt: ChronobaseOptions, struct: StructS): String = {
    struct.mutability match {
      case MutableS => {
        MutStructRootMethods.generateRootStructInstanceMethods(opt, struct)
      }
      case ImmutableS => ""
    }
  }

  def generateEffectBroadcasterMembers(opt: ChronobaseOptions, struct: StructS): String = {
    MutStructEffects.generateEffectBroadcasterMembers(struct)
  }

  def generateGlobalVisitorInterfaceMethods(struct: StructS) = {
    MutStructEffects.generateGlobalVisitorInterfaceMethods(struct)
  }

  def generateEffectBroadcasterMethods(struct: StructS) = {
    MutStructEffects.generateEffectBroadcasterMethods(struct)
  }

  def generateEffectApplierMethods(struct: StructS): String = {
    MutStructEffects.generateEffectApplierMethods(struct)
  }
}
