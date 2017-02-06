using System.Web.Mvc;

namespace ProductManagementPortal.DependencyInjection
{
    public class StructureMapBootstrapper
    {
        // StructureMap ContollerFactory
        public class StructureMapControllerFactory : DefaultControllerFactory
        {
            //protected override IController
            //    GetControllerInstance(RequestContext requestContext,
            //    Type controllerType)
            //{
            //    try
            //    {
            //        if ((requestContext == null) || (controllerType == null))
            //            return null;

            //        return (Controller)Container.GetInstance(controllerType);
            //    }
            //    catch (StructureMapException)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
            //        throw new Exception(ObjectFactory.WhatDoIHave());
            //    }
            //}
        }

        public static class Bootstrapper
        {
            //public static void Run()
            //{
            //    ControllerBuilder.Current
            //        .SetControllerFactory(new StructureMapMVC.StructureMapControllerFactory());

            //    //Container.For<IProductFacade<ProductModel>().use.For<IProductFacade<ProductModel>
            //}
        }
    }
}