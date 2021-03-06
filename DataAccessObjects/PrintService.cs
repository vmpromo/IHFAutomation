﻿// Name: Returns.aspx.cs
// Type: class file 
// Description: Code behind class for Returns screen
//
//$Revision:   1.10  $
//
// Version   Date        Author     Reason
//  1.11     06/03/18    J Duru     PutAway label added for DC Improvements
//                       M Cackett
//  1.12     14/03/18    M Cackett  Modified PrintPutAway to generate LPN
//                       S Remedios 
using System;
using System.Xml;
using System.Text;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IPrintService")]
public interface IPrintService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintLabel", ReplyAction = "http://tempuri.org/IPrintService/PrintLabelResponse")]
    string PrintLabel(string labelName, string machineName, string printerType, int labelId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintPackDocuments", ReplyAction = "http://tempuri.org/IPrintService/PrintPackDocumentsResponse")]
    string PrintPackDocuments(decimal ordernumeber, string MachineName, string DocumentType, string userid);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintPackageDocument", ReplyAction = "http://tempuri.org/IPrintService/PrintPackageDocumentResponse")]
    string PrintPackageDocument(string MachineName, decimal consignmentId, string DocumentType);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintFailedTote", ReplyAction = "http://tempuri.org/IPrintService/PrintFailedToteResponse")]
    string PrintFailedTote(decimal ordernumeber, string MachineName, string UserID);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintStoreManifest", ReplyAction = "http://tempuri.org/IPrintService/PrintStoreManifestResponse")]
    string PrintStoreManifest(decimal manifestid, string MachineName, string UserID);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/TestPrint", ReplyAction = "http://tempuri.org/IPrintService/TestPrintResponse")]
    string TestPrint(string MachineName);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/GetAvailablePrinters", ReplyAction = "http://tempuri.org/IPrintService/GetAvailablePrintersResponse")]
    string GetAvailablePrinters(string UserID);

    // PutAway label added for DC Improvements
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IPrintService/PrintPutAway")]
    void PrintPutAway(string lpn, string location, string skubarcode, string sku, string machineName);

} 

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IPrintServiceChannel : IPrintService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class PrintService : System.ServiceModel.ClientBase<IPrintService>, IPrintService
{

    public PrintService()
        : this(GetBinding(), GetEndpoint())
    {

    }

    public PrintService(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public PrintService(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public PrintService(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public PrintService(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public string PrintLabel(string labelName, string machineName, string printerType, int labelId)
    {
        return base.Channel.PrintLabel(labelName, machineName, printerType, labelId);
    }

    public string PrintLabel(string labelName, string machineName, string printerType, int labelId, bool labelIdSpecified)
    {
        return this.PrintLabel(labelName, machineName, printerType, labelId);
    }

    public string PrintPackDocuments(decimal ordernumeber, string MachineName, string DocumentType, string userid)
    {
        return base.Channel.PrintPackDocuments(ordernumeber, MachineName, DocumentType, userid);
    }

    public string PrintPackDocuments(decimal ordernumeber, bool ordernumeberSpecified, string MachineName, string DocumentType, string userid)
    {

        return PrintPackDocuments(ordernumeber, MachineName, DocumentType, userid);    
    }

    public string PrintPackageDocument(string MachineName, decimal consignmentId, string DocumentType)
    {
        return base.Channel.PrintPackageDocument(MachineName, consignmentId, DocumentType);
    }

    public string PrintFailedTote(decimal ordernumeber, string MachineName, string UserID)
    {
        return base.Channel.PrintFailedTote(ordernumeber, MachineName, UserID);
    }

    public string PrintFailedTote(decimal ordernumeber, bool ordernumeberSpecified,string MachineName, string UserID)
    {
        return this.PrintFailedTote(ordernumeber, MachineName, UserID);
    }

    public string PrintStoreManifest(decimal manifestid, string MachineName, string UserID)
    {
        return base.Channel.PrintStoreManifest(manifestid, MachineName, UserID);
    }

    // PutAway label added for DC Improvements
    public void PrintPutAway(string lpn, string location, string skubarcode, string sku, string machineName)
    {
        base.Channel.PrintPutAway(lpn, location, skubarcode, sku, machineName);
    }

    public void PrintPutAway(string lpn, string location, string skubarcode, string sku, string machineName, bool lpnSpecified)
    {
        this.PrintPutAway(lpn, location, skubarcode, sku, machineName);
    }


    public string TestPrint(string MachineName)
    {
        return base.Channel.TestPrint(MachineName);
    }

    public string GetAvailablePrinters(string UserID)
    {
        return base.Channel.GetAvailablePrinters(UserID);
    }

    private static Binding GetBinding()
    {

        TimeSpan span = new TimeSpan(0, 1, 0);

        return new BasicHttpBinding
        {
            Name = "BasicHttpBinding_IPrintService",
            CloseTimeout = span,
            OpenTimeout = span,
            ReceiveTimeout = span,
            SendTimeout = span,
            AllowCookies = false,
            BypassProxyOnLocal = false,
            HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
            MaxBufferSize = 65536,
            MaxBufferPoolSize = 524288,
            MaxReceivedMessageSize = 65536,
            MessageEncoding = WSMessageEncoding.Text,
            TextEncoding = Encoding.UTF8,
            TransferMode = TransferMode.Buffered,
            UseDefaultWebProxy = true,
            ReaderQuotas = new XmlDictionaryReaderQuotas
            {
                MaxDepth = 32,
                MaxStringContentLength = 8192,
                MaxArrayLength = 16384,
                MaxBytesPerRead = 4096,
                MaxNameTableCharCount = 16384,
            },

            Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.None,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.None,
                    ProxyCredentialType = HttpProxyCredentialType.None,
                    Realm = string.Empty
                },
                Message = new BasicHttpMessageSecurity
                {
                    ClientCredentialType = BasicHttpMessageCredentialType.UserName,
                    AlgorithmSuite = SecurityAlgorithmSuite.Default
                },

            }
        };


    }

    private static bool printserverselect = true;

    private static EndpointAddress GetEndpoint()
    {
       string setting = ConfigurationManager.AppSettings["dualPrintingEnabled"];
       bool dualPrintingEnabled = false;

       if (setting != null) {
	       bool.TryParse(setting, out dualPrintingEnabled);
	   }

       if (dualPrintingEnabled) {
        printserverselect = !printserverselect;

        return ( printserverselect ?
            new EndpointAddress(ConfigurationManager.AppSettings["PrintServiceAddress"]) :
            new EndpointAddress(ConfigurationManager.AppSettings["PrintServiceAddress2"]));
       }
       else
           return new EndpointAddress(ConfigurationManager.AppSettings["PrintServiceAddress"]);

    }
}




