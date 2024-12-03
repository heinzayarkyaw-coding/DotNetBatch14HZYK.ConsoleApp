using System.Data;
using DotNetBatch14HZYK.ConsoleApp.AdoDotNetExamples;
using DotNetBatch14HZYK.ConsoleApp.DapperExamples;
using DotNetBatch14HZYK.ConsoleApp.EFCoreExamples;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Edit("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC");
//adoDotNetExample.Edit("A3F0E6E2-8842-4FE1-AE65-D74CD5AFF184");
//adoDotNetExample.Create("Hello", "Hey", "How");
//adoDotNetExample.Delete("d19afdec-8cee-429a-b469-a4fe1dae4efd");
//adoDotNetExample.Update("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC", "111Update", "222Update", "333Update");


//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Edit("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC");
//dapperExample.Create("TripBlog", "MgMg", "Hello");
//dapperExample.Delete("4B57A7D1-6CC8-48D4-A863-DDA646B51678");
//dapperExample.Update("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC", "12Upd", "23Upd", "34Upd");



EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Read();
eFCoreExample.Edit("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC");
eFCoreExample.Create("1", "2", "3");
eFCoreExample.Update("A9B45D8C-BCC4-4119-9C33-A40BBD579DAC", "12", "13", "14");
eFCoreExample.Delete("387933B8-C81C-4502-8555-A3E3855DF521");