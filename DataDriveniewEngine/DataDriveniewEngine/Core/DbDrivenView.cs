using DataDrivenViewEngine.Models;
using DataDrivenViewEngine.Models.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DataDrivenViewEngine.Core
{
    public class DbDrivenView : IView
    {
        DataDrivenViewEngineContext dbContext = new DataDrivenViewEngineContext();
        string _viewName;
        public DbDrivenView(string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentNullException("viewName", new ArgumentException("View Name cannot be null"));
            }
            _viewName = viewName;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            DataForm dataForm = dbContext.DataForms.Include("Fields").First(f => f.Name == _viewName);
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (HtmlTextWriter htmlWriter = new HtmlTextWriter(sw))
            {
                // #1 Begins
                if (!string.IsNullOrEmpty(dataForm.SubmitUrl))
                {
                    htmlWriter.AddAttribute("action", dataForm.SubmitUrl);
                    htmlWriter.AddAttribute("method", "post");
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Form);
                }
                // #1 Ends
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                foreach (var item in dataForm.Fields)
                {
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);
                    htmlWriter.WriteEncodedText(item.DisplayLabel);
                    // #2 Begins
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Id, item.FieldName);
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Name, item.FieldName);
                    // #2 Ends
                    htmlWriter.RenderBeginTag(GetHtmlRenderKey(item.DisplayType));
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);
                }
                // #3 Begins
                if (!string.IsNullOrEmpty(dataForm.SubmitUrl))
                {
                    htmlWriter.RenderEndTag();
                }
                // #3 Ends
                // #4 Begins
                if(!string.IsNullOrEmpty(dataForm.SubmitName))
                {
                    htmlWriter.AddAttribute("type", "submit");
                    htmlWriter.AddAttribute("value", dataForm.SubmitName);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Input);
                    htmlWriter.RenderEndTag();
                }
                // #4 Ends
                htmlWriter.RenderEndTag();
            }
            writer.Write(dataForm.Template.Replace("@DataFields", sb.ToString()));
        }

        private HtmlTextWriterTag GetHtmlRenderKey(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.TextBox:
                    return HtmlTextWriterTag.Input;
                case FieldType.TextArea:
                    return HtmlTextWriterTag.Textarea;
                case FieldType.DropDown:
                    return HtmlTextWriterTag.Select;
                case FieldType.CheckBox:
                    return HtmlTextWriterTag.Input;
                case FieldType.Label:
                    return HtmlTextWriterTag.Caption;
                default:
                    return HtmlTextWriterTag.Unknown;
            }
        }
    }
}