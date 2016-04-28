using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Pipelines.Loader;
using Sitecore.Pipelines;
using ControllerBuilder = System.Web.Mvc.ControllerBuilder;

namespace Foo.Pipelines.Initialize
{
    /// <summary>
    /// Replaces the standard Sitecore MVC controller factory with one that knows how to do async action invocation.
    /// </summary>
    public class InitializeAsyncControllerFactory : InitializeControllerFactory
    {
        protected override void SetControllerFactory(PipelineArgs args)
        {
            SitecoreControllerFactory controllerFactory = new SitecoreAsyncControllerFactory(ControllerBuilder.Current.GetControllerFactory());
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}