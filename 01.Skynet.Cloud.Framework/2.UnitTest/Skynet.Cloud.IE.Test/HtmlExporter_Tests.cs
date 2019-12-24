// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : HtmlExporter_Tests.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-26 14:59
//           
//           
//           
//          
// 
// ======================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Html;
using UWay.Skynet.Cloud.IE.Tests.Models;
using Shouldly;
using Xunit;

namespace UWay.Skynet.Cloud.IE.Tests
{
    public class HtmlExporter_Tests
    {
        [Fact(DisplayName = "导出Html测试")]
        public async Task ExportHtml_Test()
        {
            var exporter = new HtmlExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportHtml_Test) + ".html");
            if (File.Exists(filePath)) File.Delete(filePath);
            //此处使用默认模板导出
            var result = await exporter.ExportListByTemplate(filePath, GenFu.GenFu.ListOf<ExportTestData>());
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }


        [Fact(DisplayName = "自定义模板导出Html测试")]
        public async Task ExportHtmlByTemplate_Test()
        {
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates", "tpl1.cshtml");
            var tpl = File.ReadAllText(tplPath);
            var exporter = new HtmlExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportHtmlByTemplate_Test) + ".html");
            if (File.Exists(filePath)) File.Delete(filePath);
            //此处使用默认模板导出
            var result = await exporter.ExportListByTemplate(filePath,
                GenFu.GenFu.ListOf<ExportTestData>(), tpl);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }

        [Fact(DisplayName = "导出收据")]
        public async Task ExportReceipt_Test()
        {
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates", "receipt.cshtml");
            var tpl = File.ReadAllText(tplPath);
            var exporter = new HtmlExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportReceipt_Test) + ".html");
            if (File.Exists(filePath)) File.Delete(filePath);
            //此处使用默认模板导出
            var result = await exporter.ExportByTemplate(filePath,
                new ReceiptInfo()
                {
                    Amount = 22939.43M,
                    Grade = "2019秋",
                    IdNo = "43062619890622xxxx",
                    Name = "张三",
                    Payee = "深圳市优网科技有限公司",
                    PaymentMethod = "微信支付",
                    Profession = "运动训练",
                    Remark = "学费",
                    TradeStatus = "已完成",
                    TradeTime = DateTime.Now,
                    UppercaseAmount = "贰万贰仟玖佰叁拾玖圆肆角叁分",
                    Code = "19071800001"
                }, tpl);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}