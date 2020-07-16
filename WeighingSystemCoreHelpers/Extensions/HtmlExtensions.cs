using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace WeighingSystemCoreHelpers.Extensions
{
    public static class HtmlExtensions
    {
        public static List<SelectListItem> ToSelectListItem<T>(this IList<T> listObject, string valueMember, string textMember)
        {
            var selectListItems = new List<SelectListItem>();
            foreach (var obj in (List<T>)listObject)
            {
                PropertyInfo prop = typeof(T).GetProperty(valueMember);
                var value = prop.GetValue(obj);

                prop = typeof(T).GetProperty(textMember);
                var text = prop.GetValue(obj);

                var selectListItem = new SelectListItem() { Value = Convert.ToString(value), Text = Convert.ToString(text) };
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }

        public static List<SelectListItem> ToSelectListItem<T>(this IList<T> listObject, string valueMember, string textMember, string dataAttr = "")
        {

            var dataAttrs = string.IsNullOrEmpty(dataAttr) ? new string[0] : dataAttr.Split(',');

            var selectListItems = new List<SelectListItem>();
            foreach (var obj in (List<T>)listObject)
            {
                PropertyInfo prop = typeof(T).GetProperty(valueMember);
                var value = prop.GetValue(obj);

                prop = typeof(T).GetProperty(textMember);
                var text = prop.GetValue(obj);

                var selectListItem = new SelectListItem() { Value = Convert.ToString(value), Text = Convert.ToString(text), };
                selectListItems.Add(selectListItem);
            }
            return selectListItems;
        }

    }

}