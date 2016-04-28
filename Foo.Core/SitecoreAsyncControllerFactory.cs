using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Controllers;

namespace Foo
{
    /// <summary>
    /// Patches the normal Sitecore controller factory to enable executing async actions and using async/await
    /// The ActionInvoker that Sitecore MVC wraps the inner action invoker with does not implement IAsyncActionInvoker,
    /// which means ASP.NET MVC does not try to execute it async if needed, and precludes async/await.
    /// </summary>
    public class SitecoreAsyncControllerFactory : SitecoreControllerFactory
    {
        public SitecoreAsyncControllerFactory(IControllerFactory innerFactory) : base(innerFactory)
        {
        }

        protected override void PrepareController(IController controller, string controllerName)
        {
            if (!MvcSettings.DetailedErrorOnMissingAction)
            {
                return;
            }
            Controller controller2 = controller as Controller;
            if (controller2 == null)
            {
                return;
            }

            /* BEGIN PATCH FOR ASYNC INVOCATION (the rest of this method is stock) */
            IAsyncActionInvoker asyncInvoker = controller2.ActionInvoker as IAsyncActionInvoker;

            if (asyncInvoker != null)
            {
                controller2.ActionInvoker = new SitecoreAsyncActionInvoker(asyncInvoker, controllerName);
                return;
            }
            /* END PATCH FOR ASYNC INVOCATION */

            IActionInvoker actionInvoker = controller2.ActionInvoker;
            if (actionInvoker == null)
            {
                return;
            }
            controller2.ActionInvoker = new SitecoreActionInvoker(actionInvoker, controllerName);
        }
    }
}