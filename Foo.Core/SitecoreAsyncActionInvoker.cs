using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Sitecore.Mvc.Controllers;

namespace Foo
{
    /// <summary>
    /// Literally all this does is provider an IAsyncActionInvoker wrapper the same way SitecoreActionInvoker wraps non-IAsyncActionInvokers
    /// This instructs ASP.NET MVC to perform async invocation for controller actions.
    /// </summary>
    public class SitecoreAsyncActionInvoker : SitecoreActionInvoker, IAsyncActionInvoker
    {
        private readonly IAsyncActionInvoker _innerInvoker;

        public SitecoreAsyncActionInvoker(IAsyncActionInvoker innerInvoker, string controllerName) : base(innerInvoker, controllerName)
        {
            _innerInvoker = innerInvoker;
        }

        public IAsyncResult BeginInvokeAction(ControllerContext controllerContext, string actionName, AsyncCallback callback, object state)
        {
            return _innerInvoker.BeginInvokeAction(controllerContext, actionName, callback, state);
        }

        public bool EndInvokeAction(IAsyncResult asyncResult)
        {
            return _innerInvoker.EndInvokeAction(asyncResult);
        }
    }
}
