﻿using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;

namespace System.Web.Http.Tracing.Tracers
{
    /// <summary>
    /// Tracer for <see cref="IHttpControllerActivator"/>.
    /// </summary>
    internal class HttpControllerActivatorTracer : IHttpControllerActivator
    {
        private const string CreateMethodName = "Create";

        private readonly IHttpControllerActivator _innerActivator;
        private readonly ITraceWriter _traceWriter;

        public HttpControllerActivatorTracer(IHttpControllerActivator innerActivator, ITraceWriter traceWriter)
        {
            _innerActivator = innerActivator;
            _traceWriter = traceWriter;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "disposable controller is later released in ReleaseController")]
        IHttpController IHttpControllerActivator.Create(HttpControllerContext controllerContext, Type controllerType)
        {
            IHttpController controller = null;

            _traceWriter.TraceBeginEnd(
                controllerContext.Request,
                TraceCategories.ControllersCategory,
                TraceLevel.Info,
                _innerActivator.GetType().Name,
                CreateMethodName,
                beginTrace: null,
                execute: () =>
                {
                    controller = _innerActivator.Create(controllerContext, controllerType);
                },
                endTrace: (tr) =>
                {
                    tr.Message = controller == null ? SRResources.TraceNoneObjectMessage : controller.GetType().FullName;
                },
                errorTrace: null);

            if (controller != null && !(controller is HttpControllerTracer))
            {
                controller = new HttpControllerTracer(controller, _traceWriter);
            }

            return controller;
        }
    }
}
