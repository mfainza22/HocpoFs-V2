using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class ModelStateExtensions
    {
        public static string ToJson(this ModelStateDictionary modelState)
        {
            string r = null;
            if (modelState.ErrorCount > 0)
            {
                var a = modelState
                             .Where(x => x.Value.Errors.Count > 0)
                             .ToDictionary(
                                 kvp => kvp.Key,
                                 kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                             );
                r = JsonConvert.SerializeObject(a);
            }

            System.Diagnostics.Debug.WriteLine(r);
            // foreach (ModelStateEntry e in m.Values)
            // {
            //     foreach (ModelError v in e.Errors)
            //     {
            //         System.Diagnostics.Debug.WriteLine(v.ErrorMessage);
            //     }
            // }

            return r;
        }
        // public static List<ModelStateViewModel> ToJson (this ModelStateDictionary modelState)
        // {
        //     foreach (ModelStateEntry e in modelState.Values)
        //     {   
        //         foreach (ModelError v in e.Errors)
        //         {
        //             var ms = new ModelStateViewModel();
        //             System.Diagnostics.Debug.WriteLine(v.ErrorMessage);
        //         }
        //     }
        // }
    }
}