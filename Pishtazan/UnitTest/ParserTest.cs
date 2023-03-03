using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Services.Services;
using System;
using WebApi;
using Xunit;

namespace UnitTest
{
    public class Tests
    {
        private IServiceProvider _serviceProvider;
        [SetUp]
        public void Setup()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>()
                    .Build();
            _serviceProvider = webHost.Services.CreateScope().ServiceProvider;
        }

        [Test]
        [TestCase(@"<Employee>
                  <PersonnelInfo>
                      <FirstName> Tanmay </FirstName>
                      <LastName> Patil </LastName>
                      <BasicSalary>1234567890</BasicSalary>
                      <Allowance>111</Allowance>
                      <Transportation>111</Transportation>
                      <TotalSallary>232424</TotalSallary>
                      <SalaryDate>2023-02-03</SalaryDate>
                  </PersonnelInfo>
                <PersonnelInfo>
                      <FirstName> Tanmay </FirstName>
                      <LastName> Patil </LastName>
                      <BasicSalary>1234567890</BasicSalary>
                      <Allowance>111</Allowance>
                      <Transportation>111</Transportation>
                      <TotalSallary>232424</TotalSallary>
                      <SalaryDate>2023-02-03</SalaryDate>
                  </PersonnelInfo>
            </Employee>
       ")]

        public void XmlParserTest(string data)
        {
            IXmlParser xmlParser = _serviceProvider.GetRequiredService<IXmlParser>();
            var res = xmlParser.Parse(data);
            Assert.AreEqual(res.Count, 2);

            
        }
        [Test]
        [TestCase(@"[{
                        ""FirstName"" : ""Tanmay"",
                        ""LastName"" : ""Patil"",
                        ""BasicSalary"" : 1234567890,
                        ""Allowance"" : 111,
                        ""Transportation"" : 111,
                        ""TotalSallary"" : 232424,
                        ""SalaryDate"" : ""2023-02-03""
                    },
                    {
                        ""FirstName"" : ""Tanmay"",
                        ""LastName"" : ""Patil"",
                        ""BasicSalary"" : 1234567890,
                        ""Allowance"" : 111,
                        ""Transportation"" : 111,
                        ""TotalSallary"" : 232424,
                        ""SalaryDate"" : ""2023-02-03""
                    }]
")]
        public void JsonParserTest(string data)
        {
            IJsonParser jsonParser = _serviceProvider.GetRequiredService<IJsonParser>();
            var res = jsonParser.Parse(data);
            Assert.AreEqual(res.Count, 2);
        }

        [Test]
        [TestCase(@"FirstName,LastName,BasicSalary,Allowance,Transportation,TotalSallary,SalaryDate
Tanmay,Patil,1234567890,111,111,232424,2023-02-03
Tanmay,Patil,1234567890,111,111,232424,2023-02-03")]
        public void csvParser(string data)
        {
            ICSVParser csvParser = _serviceProvider.GetRequiredService<ICSVParser>();
            var res = csvParser.Parse(data);
            Assert.AreEqual(res.Count, 2);
        }
        [Test]
        [TestCase(@"FirstName/LastName/BasicSalary/Allowance/Transportation/TotalSallary/SalaryDate
Tanmay/Patil/1234567890/111/111/232424/2023-02-03
Tanmay/Patil/1234567890/111/111/232424/2023-02-03")]
        public void CustomParser(string data)
        {
            ICustomParser customParser = _serviceProvider.GetRequiredService<ICustomParser>();
            var res = customParser.Parse(data);
            Assert.AreEqual(res.Count, 2);
        }
        [Test]
        [TestCase("xml",@"<Employee>
                  <PersonnelInfo>
                      <FirstName> Tanmay </FirstName>
                      <LastName> Patil </LastName>
                      <BasicSalary>1234567890</BasicSalary>
                      <Allowance>111</Allowance>
                      <Transportation>111</Transportation>
                      <TotalSallary>232424</TotalSallary>
                      <SalaryDate>2023-02-03</SalaryDate>
                  </PersonnelInfo>
                <PersonnelInfo>
                      <FirstName> Tanmay </FirstName>
                      <LastName> Patil </LastName>
                      <BasicSalary>1234567890</BasicSalary>
                      <Allowance>111</Allowance>
                      <Transportation>111</Transportation>
                      <TotalSallary>232424</TotalSallary>
                      <SalaryDate>2023-02-03</SalaryDate>
                  </PersonnelInfo>
            </Employee>
       ")]
        [TestCase("json", @"[{
                        ""FirstName"" : ""Tanmay"",
                        ""LastName"" : ""Patil"",
                        ""BasicSalary"" : 1234567890,
                        ""Allowance"" : 111,
                        ""Transportation"" : 111,
                        ""TotalSallary"" : 232424,
                        ""SalaryDate"" : ""2023-02-03""
                    },
                    {
                        ""FirstName"" : ""Tanmay"",
                        ""LastName"" : ""Patil"",
                        ""BasicSalary"" : 1234567890,
                        ""Allowance"" : 111,
                        ""Transportation"" : 111,
                        ""TotalSallary"" : 232424,
                        ""SalaryDate"" : ""2023-02-03""
                    }]
")]
        [TestCase("csv", @"FirstName,LastName,BasicSalary,Allowance,Transportation,TotalSallary,SalaryDate
Tanmay,Patil,1234567890,111,111,232424,2023-02-03
Tanmay,Patil,1234567890,111,111,232424,2023-02-03")]
        [TestCase("custom",@"FirstName/LastName/BasicSalary/Allowance/Transportation/TotalSallary/SalaryDate
Tanmay/Patil/1234567890/111/111/232424/2023-02-03
Tanmay/Patil/1234567890/111/111/232424/2023-02-03")]
        public void ParserTest(string type ,string data)
        {
            IParser parser = _serviceProvider.GetRequiredService<IParser>();
            var res2 = parser.Parse(type, data);
            Assert.AreEqual(res2.Count, 2);
        }
    }
}