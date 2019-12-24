// ======================================================================
// 
//           Copyright (C) 2019-2030 深圳市优网科技有限公司
//           All rights reserved
// 
//           filename : WordExporter_Tests.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-26 14:59
//           
//           
//           
//          
// 
// ======================================================================

using System.IO;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.IE.Tests.Models;
using UWay.Skynet.Cloud.IE.Word;
using Shouldly;
using Xunit;

namespace UWay.Skynet.Cloud.IE.Tests
{
    public class WordExporter_Tests
    {
        [Fact(DisplayName = "导出Word测试")]
        public async Task ExportWord_Test()
        {
            var exporter = new WordExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportWord_Test) + ".docx");
            if (File.Exists(filePath)) File.Delete(filePath);
            //此处使用默认模板导出
            var result = await exporter.ExportListByTemplate(filePath, GenFu.GenFu.ListOf<ExportTestData>());
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }


        [Fact(DisplayName = "自定义模板导出Word测试")]
        public async Task ExportWordByTemplate_Test()
        {
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates", "tpl1.cshtml");
            var tpl = File.ReadAllText(tplPath);
            var exporter = new WordExporter();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportWordByTemplate_Test) + ".docx");
            if (File.Exists(filePath)) File.Delete(filePath);
            //此处使用默认模板导出
            var result = await exporter.ExportListByTemplate(filePath,
                GenFu.GenFu.ListOf<ExportTestData>(), tpl);
            result.ShouldNotBeNull();
            File.Exists(filePath).ShouldBeTrue();
        }
    }
}