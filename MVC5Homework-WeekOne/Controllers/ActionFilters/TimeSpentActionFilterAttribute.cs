using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Homework_WeekOne.Controllers.ActionFilters
{
    public class TimeSpentActionFilterAttribute : ActionFilterAttribute
    {
        static Stopwatch STOPWATCH;

        public TimeSpentActionFilterAttribute()
        {
            STOPWATCH = new Stopwatch();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            STOPWATCH.Reset();
            STOPWATCH.Start();
            Debug.WriteLine("@OnActionExecuting, Time's running!");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            STOPWATCH.Stop();
            Debug.WriteLine($"@OnActionExecuted, Time spent: {STOPWATCH.Elapsed}");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            STOPWATCH.Reset();
            STOPWATCH.Start();
            Debug.WriteLine("@OnResultExecuting, Time's running!");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            STOPWATCH.Stop();
            Debug.WriteLine($"@OnResultExecuted, Time spent: {STOPWATCH.Elapsed}");
        }
    }
}