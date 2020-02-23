package net.verdagon.chronobasecs.generator.chronobase.interface

import net.verdagon.chronobasecs.compiled.{ImmutableS, InterfaceS, MutableS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions
import net.verdagon.chronobasecs.generator.{MutInterface, MutInterfaceImpl, RootInterfaceMethods}

object InterfaceGenerator {
  def generateHandles(opt: ChronobaseOptions, ssS: SuperstructureS, interface: InterfaceS): Map[String, String] = {
    interface.mutability match {
      case MutableS => {
        MutInterface.generateInterface(opt, ssS, interface)// ++
//        interface.ancestorInterfaces
//          .flatMap(superInterface => {
//            MutInterfaceImpl.generateInterfaceImpl(interface.tyype, superInterface)
//          })
//          .toMap
      }
      case ImmutableS => {
        ImmInterface.generateInterface(opt, interface)
      }
    }
  }

  def generateRootMethods(opt: ChronobaseOptions, interface: InterfaceS): String = {
    interface.mutability match {
      case MutableS => {
        RootInterfaceMethods.generateRootInterfaceInstanceMethods(opt, interface)
      }
      case ImmutableS => ""
    }
  }
}
