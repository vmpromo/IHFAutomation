using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Com.MetaPack.DeliveryManager
{
    /// <summary>
    /// This is the client stub class for DM API 4.x.
    /// </summary>
    public class DeliveryManagerClient
    {
        private string mUrl;
        private ICredentials mCredentials;
        private AllocationServiceService mAllocationServiceService;
        private ConsignmentServiceService mConsignmentServiceService;
        private ConsignmentSearchServiceService mConsignmentSearchServiceService;
        private ConsignmentTrackingServiceService mConsignmentTrackingServiceService;
        private DebugServiceService mDebugServiceService;
        private InformationServiceService mInformationServiceService;
        private ManifestServiceService mManifestServiceService;
        private SetupServiceService mSetupServiceService;

        public DeliveryManagerClient(string url, ICredentials credentials)
        {
            mAllocationServiceService = new AllocationServiceService();
            mConsignmentServiceService = new ConsignmentServiceService();
            mConsignmentSearchServiceService = new ConsignmentSearchServiceService();
            mConsignmentTrackingServiceService = new ConsignmentTrackingServiceService();
            mDebugServiceService = new DebugServiceService();
            mInformationServiceService = new InformationServiceService();
            mManifestServiceService = new ManifestServiceService();
            mSetupServiceService = new SetupServiceService();
            this.Url = url;
            this.Credentials = credentials;
        }

        public string Url
        {
            get
            {
                return mUrl;
            }
            set
            {
                mAllocationServiceService.Url = value + "/AllocationService";
                mConsignmentServiceService.Url = value + "/ConsignmentService";
                mConsignmentSearchServiceService.Url = value + "/ConsignmentSearchService";
                mConsignmentTrackingServiceService.Url = value + "/ConsignmentTrackingService";
                mDebugServiceService.Url = value + "/DebugService";
                mInformationServiceService.Url = value + "/InformationService";
                mManifestServiceService.Url = value + "/ManifestService";
                mSetupServiceService.Url = value + "/SetupService";
                mUrl = value;
            }
        }

        public ICredentials Credentials
        {
            get
            {
                return mCredentials;
            }
            set
            {
                mAllocationServiceService.Credentials = value;
                mConsignmentServiceService.Credentials = value;
                mConsignmentSearchServiceService.Credentials = value;
                mConsignmentTrackingServiceService.Credentials = value;
                mDebugServiceService.Credentials = value;
                mInformationServiceService.Credentials = value;
                mManifestServiceService.Credentials = value;
                mSetupServiceService.Credentials = value;
                mCredentials = value;
            }
        }

        public AllocationServiceService AllocationService { get { return mAllocationServiceService; } }
        public ConsignmentServiceService ConsignmentService { get { return mConsignmentServiceService; } }
        public ConsignmentSearchServiceService ConsignmentSearchService { get { return mConsignmentSearchServiceService; } }
        public ConsignmentTrackingServiceService ConsignmentTrackingService { get { return mConsignmentTrackingServiceService; } }
        public DebugServiceService DebugService { get { return mDebugServiceService; } }
        public InformationServiceService InformationService { get { return mInformationServiceService; } }
        public ManifestServiceService ManifestService { get { return mManifestServiceService; } }
        public SetupServiceService SetupService { get { return mSetupServiceService; } }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "AllocationServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(DeliveryOption))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Property))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Product))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Parcel))]
    public partial class AllocationServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback deallocateOperationCompleted;

        private System.Threading.SendOrPostCallback findDeliveryOptionsOperationCompleted;

        private System.Threading.SendOrPostCallback findDeliveryOptionsWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback findDeliveryOptionsForConsignmentOperationCompleted;

        private System.Threading.SendOrPostCallback findDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback createAndAllocateConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback createAndAllocateConsignmentsWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback verifyAllocationOperationCompleted;

        private System.Threading.SendOrPostCallback allocateConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback allocateConsignmentsWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback despatchConsignmentOperationCompleted;

        private System.Threading.SendOrPostCallback despatchConsignmentWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback verifyAllocationWithBookingCodeOperationCompleted;

        /// <remarks/>
        public AllocationServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/AllocationService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event deallocateCompletedEventHandler deallocateCompleted;

        /// <remarks/>
        public event findDeliveryOptionsCompletedEventHandler findDeliveryOptionsCompleted;

        /// <remarks/>
        public event findDeliveryOptionsWithBookingCodeCompletedEventHandler findDeliveryOptionsWithBookingCodeCompleted;

        /// <remarks/>
        public event findDeliveryOptionsForConsignmentCompletedEventHandler findDeliveryOptionsForConsignmentCompleted;

        /// <remarks/>
        public event findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventHandler findDeliveryOptionsForConsignmentWithBookingCodeCompleted;

        /// <remarks/>
        public event createAndAllocateConsignmentsCompletedEventHandler createAndAllocateConsignmentsCompleted;

        /// <remarks/>
        public event createAndAllocateConsignmentsWithBookingCodeCompletedEventHandler createAndAllocateConsignmentsWithBookingCodeCompleted;

        /// <remarks/>
        public event verifyAllocationCompletedEventHandler verifyAllocationCompleted;

        /// <remarks/>
        public event allocateConsignmentsCompletedEventHandler allocateConsignmentsCompleted;

        /// <remarks/>
        public event allocateConsignmentsWithBookingCodeCompletedEventHandler allocateConsignmentsWithBookingCodeCompleted;

        /// <remarks/>
        public event despatchConsignmentCompletedEventHandler despatchConsignmentCompleted;

        /// <remarks/>
        public event despatchConsignmentWithBookingCodeCompletedEventHandler despatchConsignmentWithBookingCodeCompleted;

        /// <remarks/>
        public event verifyAllocationWithBookingCodeCompletedEventHandler verifyAllocationWithBookingCodeCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("deallocateReturn")]
        public string[] deallocate(string[] consignmentCodes)
        {
            object[] results = this.Invoke("deallocate", new object[] {
                    consignmentCodes});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Begindeallocate(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("deallocate", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public string[] Enddeallocate(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void deallocateAsync(string[] consignmentCodes)
        {
            this.deallocateAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void deallocateAsync(string[] consignmentCodes, object userState)
        {
            if ((this.deallocateOperationCompleted == null))
            {
                this.deallocateOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeallocateOperationCompleted);
            }
            this.InvokeAsync("deallocate", new object[] {
                    consignmentCodes}, this.deallocateOperationCompleted, userState);
        }

        private void OndeallocateOperationCompleted(object arg)
        {
            if ((this.deallocateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deallocateCompleted(this, new deallocateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findDeliveryOptionsReturn")]
        public DeliveryOption[] findDeliveryOptions(Consignment consignment, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("findDeliveryOptions", new object[] {
                    consignment,
                    filter,
                    calculateTaxAndDuty});
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindDeliveryOptions(Consignment consignment, AllocationFilter filter, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findDeliveryOptions", new object[] {
                    consignment,
                    filter,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DeliveryOption[] EndfindDeliveryOptions(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public void findDeliveryOptionsAsync(Consignment consignment, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            this.findDeliveryOptionsAsync(consignment, filter, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void findDeliveryOptionsAsync(Consignment consignment, AllocationFilter filter, bool calculateTaxAndDuty, object userState)
        {
            if ((this.findDeliveryOptionsOperationCompleted == null))
            {
                this.findDeliveryOptionsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDeliveryOptionsOperationCompleted);
            }
            this.InvokeAsync("findDeliveryOptions", new object[] {
                    consignment,
                    filter,
                    calculateTaxAndDuty}, this.findDeliveryOptionsOperationCompleted, userState);
        }

        private void OnfindDeliveryOptionsOperationCompleted(object arg)
        {
            if ((this.findDeliveryOptionsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findDeliveryOptionsCompleted(this, new findDeliveryOptionsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findDeliveryOptionsWithBookingCodeReturn")]
        public DeliveryOption[] findDeliveryOptionsWithBookingCode(Consignment consignment, string bookingCode, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("findDeliveryOptionsWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty});
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindDeliveryOptionsWithBookingCode(Consignment consignment, string bookingCode, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findDeliveryOptionsWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DeliveryOption[] EndfindDeliveryOptionsWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public void findDeliveryOptionsWithBookingCodeAsync(Consignment consignment, string bookingCode, bool calculateTaxAndDuty)
        {
            this.findDeliveryOptionsWithBookingCodeAsync(consignment, bookingCode, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void findDeliveryOptionsWithBookingCodeAsync(Consignment consignment, string bookingCode, bool calculateTaxAndDuty, object userState)
        {
            if ((this.findDeliveryOptionsWithBookingCodeOperationCompleted == null))
            {
                this.findDeliveryOptionsWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDeliveryOptionsWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("findDeliveryOptionsWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty}, this.findDeliveryOptionsWithBookingCodeOperationCompleted, userState);
        }

        private void OnfindDeliveryOptionsWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.findDeliveryOptionsWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findDeliveryOptionsWithBookingCodeCompleted(this, new findDeliveryOptionsWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findDeliveryOptionsForConsignmentReturn")]
        public DeliveryOption[] findDeliveryOptionsForConsignment(string consignmentCode, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("findDeliveryOptionsForConsignment", new object[] {
                    consignmentCode,
                    filter,
                    calculateTaxAndDuty});
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindDeliveryOptionsForConsignment(string consignmentCode, AllocationFilter filter, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findDeliveryOptionsForConsignment", new object[] {
                    consignmentCode,
                    filter,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DeliveryOption[] EndfindDeliveryOptionsForConsignment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public void findDeliveryOptionsForConsignmentAsync(string consignmentCode, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            this.findDeliveryOptionsForConsignmentAsync(consignmentCode, filter, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void findDeliveryOptionsForConsignmentAsync(string consignmentCode, AllocationFilter filter, bool calculateTaxAndDuty, object userState)
        {
            if ((this.findDeliveryOptionsForConsignmentOperationCompleted == null))
            {
                this.findDeliveryOptionsForConsignmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDeliveryOptionsForConsignmentOperationCompleted);
            }
            this.InvokeAsync("findDeliveryOptionsForConsignment", new object[] {
                    consignmentCode,
                    filter,
                    calculateTaxAndDuty}, this.findDeliveryOptionsForConsignmentOperationCompleted, userState);
        }

        private void OnfindDeliveryOptionsForConsignmentOperationCompleted(object arg)
        {
            if ((this.findDeliveryOptionsForConsignmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findDeliveryOptionsForConsignmentCompleted(this, new findDeliveryOptionsForConsignmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findDeliveryOptionsForConsignmentWithBookingCodeReturn")]
        public DeliveryOption[] findDeliveryOptionsForConsignmentWithBookingCode(string consignmentCode, string bookingCode, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("findDeliveryOptionsForConsignmentWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    calculateTaxAndDuty});
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindDeliveryOptionsForConsignmentWithBookingCode(string consignmentCode, string bookingCode, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findDeliveryOptionsForConsignmentWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DeliveryOption[] EndfindDeliveryOptionsForConsignmentWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DeliveryOption[])(results[0]));
        }

        /// <remarks/>
        public void findDeliveryOptionsForConsignmentWithBookingCodeAsync(string consignmentCode, string bookingCode, bool calculateTaxAndDuty)
        {
            this.findDeliveryOptionsForConsignmentWithBookingCodeAsync(consignmentCode, bookingCode, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void findDeliveryOptionsForConsignmentWithBookingCodeAsync(string consignmentCode, string bookingCode, bool calculateTaxAndDuty, object userState)
        {
            if ((this.findDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted == null))
            {
                this.findDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("findDeliveryOptionsForConsignmentWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    calculateTaxAndDuty}, this.findDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted, userState);
        }

        private void OnfindDeliveryOptionsForConsignmentWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.findDeliveryOptionsForConsignmentWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findDeliveryOptionsForConsignmentWithBookingCodeCompleted(this, new findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createAndAllocateConsignmentsReturn")]
        public Consignment[] createAndAllocateConsignments(Consignment[] consignments, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("createAndAllocateConsignments", new object[] {
                    consignments,
                    filter,
                    calculateTaxAndDuty});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateAndAllocateConsignments(Consignment[] consignments, AllocationFilter filter, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createAndAllocateConsignments", new object[] {
                    consignments,
                    filter,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndcreateAndAllocateConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void createAndAllocateConsignmentsAsync(Consignment[] consignments, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            this.createAndAllocateConsignmentsAsync(consignments, filter, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void createAndAllocateConsignmentsAsync(Consignment[] consignments, AllocationFilter filter, bool calculateTaxAndDuty, object userState)
        {
            if ((this.createAndAllocateConsignmentsOperationCompleted == null))
            {
                this.createAndAllocateConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateAndAllocateConsignmentsOperationCompleted);
            }
            this.InvokeAsync("createAndAllocateConsignments", new object[] {
                    consignments,
                    filter,
                    calculateTaxAndDuty}, this.createAndAllocateConsignmentsOperationCompleted, userState);
        }

        private void OncreateAndAllocateConsignmentsOperationCompleted(object arg)
        {
            if ((this.createAndAllocateConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createAndAllocateConsignmentsCompleted(this, new createAndAllocateConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createAndAllocateConsignmentsWithBookingCodeReturn")]
        public Consignment[] createAndAllocateConsignmentsWithBookingCode(Consignment[] consignments, string bookingCode, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("createAndAllocateConsignmentsWithBookingCode", new object[] {
                    consignments,
                    bookingCode,
                    calculateTaxAndDuty});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateAndAllocateConsignmentsWithBookingCode(Consignment[] consignments, string bookingCode, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createAndAllocateConsignmentsWithBookingCode", new object[] {
                    consignments,
                    bookingCode,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndcreateAndAllocateConsignmentsWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void createAndAllocateConsignmentsWithBookingCodeAsync(Consignment[] consignments, string bookingCode, bool calculateTaxAndDuty)
        {
            this.createAndAllocateConsignmentsWithBookingCodeAsync(consignments, bookingCode, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void createAndAllocateConsignmentsWithBookingCodeAsync(Consignment[] consignments, string bookingCode, bool calculateTaxAndDuty, object userState)
        {
            if ((this.createAndAllocateConsignmentsWithBookingCodeOperationCompleted == null))
            {
                this.createAndAllocateConsignmentsWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateAndAllocateConsignmentsWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("createAndAllocateConsignmentsWithBookingCode", new object[] {
                    consignments,
                    bookingCode,
                    calculateTaxAndDuty}, this.createAndAllocateConsignmentsWithBookingCodeOperationCompleted, userState);
        }

        private void OncreateAndAllocateConsignmentsWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.createAndAllocateConsignmentsWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createAndAllocateConsignmentsWithBookingCodeCompleted(this, new createAndAllocateConsignmentsWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("verifyAllocationReturn")]
        public Consignment verifyAllocation(string consignmentCode, AllocationFilter filter, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("verifyAllocation", new object[] {
                    consignmentCode,
                    filter,
                    recalculateTaxAndDuty});
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginverifyAllocation(string consignmentCode, AllocationFilter filter, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("verifyAllocation", new object[] {
                    consignmentCode,
                    filter,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment EndverifyAllocation(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public void verifyAllocationAsync(string consignmentCode, AllocationFilter filter, bool recalculateTaxAndDuty)
        {
            this.verifyAllocationAsync(consignmentCode, filter, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void verifyAllocationAsync(string consignmentCode, AllocationFilter filter, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.verifyAllocationOperationCompleted == null))
            {
                this.verifyAllocationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnverifyAllocationOperationCompleted);
            }
            this.InvokeAsync("verifyAllocation", new object[] {
                    consignmentCode,
                    filter,
                    recalculateTaxAndDuty}, this.verifyAllocationOperationCompleted, userState);
        }

        private void OnverifyAllocationOperationCompleted(object arg)
        {
            if ((this.verifyAllocationCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.verifyAllocationCompleted(this, new verifyAllocationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("allocateConsignmentsReturn")]
        public Consignment[] allocateConsignments(string[] consignmentCodes, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("allocateConsignments", new object[] {
                    consignmentCodes,
                    filter,
                    calculateTaxAndDuty});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginallocateConsignments(string[] consignmentCodes, AllocationFilter filter, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("allocateConsignments", new object[] {
                    consignmentCodes,
                    filter,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndallocateConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void allocateConsignmentsAsync(string[] consignmentCodes, AllocationFilter filter, bool calculateTaxAndDuty)
        {
            this.allocateConsignmentsAsync(consignmentCodes, filter, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void allocateConsignmentsAsync(string[] consignmentCodes, AllocationFilter filter, bool calculateTaxAndDuty, object userState)
        {
            if ((this.allocateConsignmentsOperationCompleted == null))
            {
                this.allocateConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnallocateConsignmentsOperationCompleted);
            }
            this.InvokeAsync("allocateConsignments", new object[] {
                    consignmentCodes,
                    filter,
                    calculateTaxAndDuty}, this.allocateConsignmentsOperationCompleted, userState);
        }

        private void OnallocateConsignmentsOperationCompleted(object arg)
        {
            if ((this.allocateConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.allocateConsignmentsCompleted(this, new allocateConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("allocateConsignmentsWithBookingCodeReturn")]
        public Consignment[] allocateConsignmentsWithBookingCode(string[] consignmentCodes, string bookingCode, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("allocateConsignmentsWithBookingCode", new object[] {
                    consignmentCodes,
                    bookingCode,
                    calculateTaxAndDuty});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginallocateConsignmentsWithBookingCode(string[] consignmentCodes, string bookingCode, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("allocateConsignmentsWithBookingCode", new object[] {
                    consignmentCodes,
                    bookingCode,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndallocateConsignmentsWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void allocateConsignmentsWithBookingCodeAsync(string[] consignmentCodes, string bookingCode, bool calculateTaxAndDuty)
        {
            this.allocateConsignmentsWithBookingCodeAsync(consignmentCodes, bookingCode, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void allocateConsignmentsWithBookingCodeAsync(string[] consignmentCodes, string bookingCode, bool calculateTaxAndDuty, object userState)
        {
            if ((this.allocateConsignmentsWithBookingCodeOperationCompleted == null))
            {
                this.allocateConsignmentsWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnallocateConsignmentsWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("allocateConsignmentsWithBookingCode", new object[] {
                    consignmentCodes,
                    bookingCode,
                    calculateTaxAndDuty}, this.allocateConsignmentsWithBookingCodeOperationCompleted, userState);
        }

        private void OnallocateConsignmentsWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.allocateConsignmentsWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.allocateConsignmentsWithBookingCodeCompleted(this, new allocateConsignmentsWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("despatchConsignmentReturn")]
        public DespatchedConsignment despatchConsignment(Consignment consignment, AllocationFilter allocationFilter, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("despatchConsignment", new object[] {
                    consignment,
                    allocationFilter,
                    calculateTaxAndDuty});
            return ((DespatchedConsignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindespatchConsignment(Consignment consignment, AllocationFilter allocationFilter, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("despatchConsignment", new object[] {
                    consignment,
                    allocationFilter,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DespatchedConsignment EnddespatchConsignment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DespatchedConsignment)(results[0]));
        }

        /// <remarks/>
        public void despatchConsignmentAsync(Consignment consignment, AllocationFilter allocationFilter, bool calculateTaxAndDuty)
        {
            this.despatchConsignmentAsync(consignment, allocationFilter, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void despatchConsignmentAsync(Consignment consignment, AllocationFilter allocationFilter, bool calculateTaxAndDuty, object userState)
        {
            if ((this.despatchConsignmentOperationCompleted == null))
            {
                this.despatchConsignmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OndespatchConsignmentOperationCompleted);
            }
            this.InvokeAsync("despatchConsignment", new object[] {
                    consignment,
                    allocationFilter,
                    calculateTaxAndDuty}, this.despatchConsignmentOperationCompleted, userState);
        }

        private void OndespatchConsignmentOperationCompleted(object arg)
        {
            if ((this.despatchConsignmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.despatchConsignmentCompleted(this, new despatchConsignmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("despatchConsignmentWithBookingCodeReturn")]
        public DespatchedConsignment despatchConsignmentWithBookingCode(Consignment consignment, string bookingCode, bool calculateTaxAndDuty)
        {
            object[] results = this.Invoke("despatchConsignmentWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty});
            return ((DespatchedConsignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindespatchConsignmentWithBookingCode(Consignment consignment, string bookingCode, bool calculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("despatchConsignmentWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public DespatchedConsignment EnddespatchConsignmentWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DespatchedConsignment)(results[0]));
        }

        /// <remarks/>
        public void despatchConsignmentWithBookingCodeAsync(Consignment consignment, string bookingCode, bool calculateTaxAndDuty)
        {
            this.despatchConsignmentWithBookingCodeAsync(consignment, bookingCode, calculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void despatchConsignmentWithBookingCodeAsync(Consignment consignment, string bookingCode, bool calculateTaxAndDuty, object userState)
        {
            if ((this.despatchConsignmentWithBookingCodeOperationCompleted == null))
            {
                this.despatchConsignmentWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndespatchConsignmentWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("despatchConsignmentWithBookingCode", new object[] {
                    consignment,
                    bookingCode,
                    calculateTaxAndDuty}, this.despatchConsignmentWithBookingCodeOperationCompleted, userState);
        }

        private void OndespatchConsignmentWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.despatchConsignmentWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.despatchConsignmentWithBookingCodeCompleted(this, new despatchConsignmentWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("verifyAllocationWithBookingCodeReturn")]
        public Consignment verifyAllocationWithBookingCode(string consignmentCode, string bookingCode, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("verifyAllocationWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    recalculateTaxAndDuty});
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginverifyAllocationWithBookingCode(string consignmentCode, string bookingCode, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("verifyAllocationWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment EndverifyAllocationWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public void verifyAllocationWithBookingCodeAsync(string consignmentCode, string bookingCode, bool recalculateTaxAndDuty)
        {
            this.verifyAllocationWithBookingCodeAsync(consignmentCode, bookingCode, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void verifyAllocationWithBookingCodeAsync(string consignmentCode, string bookingCode, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.verifyAllocationWithBookingCodeOperationCompleted == null))
            {
                this.verifyAllocationWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnverifyAllocationWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("verifyAllocationWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode,
                    recalculateTaxAndDuty}, this.verifyAllocationWithBookingCodeOperationCompleted, userState);
        }

        private void OnverifyAllocationWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.verifyAllocationWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.verifyAllocationWithBookingCodeCompleted(this, new verifyAllocationWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Consignment
    {

        private bool alreadyPalletisedGoodsFlagField;

        private string cardNumberField;

        private string carrierCodeField;

        private string carrierConsignmentCodeField;

        private string carrierNameField;

        private string carrierServiceCodeField;

        private string carrierServiceNameField;

        private double carrierServiceVATRateField;

        private string cartonNumberField;

        private string cashOnDeliveryCurrencyCodeField;

        private DateRange committedCollectionWindowField;

        private DateRange committedDeliveryWindowField;

        private string consDestinationReferenceField;

        private string consOriginReferenceField;

        private string consRecipientReferenceField;

        private string consReferenceField;

        private string consSenderReferenceField;

        private string consignmentCodeField;

        private bool consignmentLevelDetailsFlagField;

        private double consignmentValueField;

        private string consignmentValueCurrencyCodeField;

        private double consignmentValueCurrencyRateField;

        private double consignmentWeightField;

        private string custom1Field;

        private string custom10Field;

        private string custom2Field;

        private string custom3Field;

        private string custom4Field;

        private string custom5Field;

        private string custom6Field;

        private string custom7Field;

        private string custom8Field;

        private string custom9Field;

        private bool customsDocumentationRequiredField;

        private System.Nullable<System.DateTime> cutOffDateField;

        private System.Nullable<System.DateTime> despatchDateField;

        private System.Nullable<System.DateTime> earliestDeliveryDateField;

        private string endVatNumberField;

        private bool fragileGoodsFlagField;

        private System.Nullable<System.DateTime> guaranteedDeliveryDateField;

        private bool hazardousGoodsFlagField;

        private double insuranceValueField;

        private string insuranceValueCurrencyCodeField;

        private double insuranceValueCurrencyRateField;

        private string languageCodeField;

        private bool liquidGoodsFlagField;

        private string manifestGroupCodeField;

        private double maxDimensionField;

        private string metaCampaignKeyField;

        private string metaCustomerKeyField;

        private bool moreThanOneMetreGoodsFlagField;

        private bool moreThanTwentyFiveKgGoodsFlagField;

        private System.Nullable<System.DateTime> orderDateField;

        private string orderNumberField;

        private double orderValueField;

        private int parcelCountField;

        private Parcel[] parcelsField;

        private string pickTicketNumberField;

        private string pickupPointField;

        private string podRequiredField;

        private Property[] propertiesField;

        private Address recipientAddressField;

        private string recipientCodeField;

        private string recipientContactPhoneField;

        private string recipientEmailField;

        private string recipientMobilePhoneField;

        private string recipientNameField;

        private string recipientNotificationTypeField;

        private string recipientPhoneField;

        private string recipientTimeZoneField;

        private string recipientVatNumberField;

        private Address returnAddressField;

        private Address senderAddressField;

        private string senderCodeField;

        private string senderContactPhoneField;

        private string senderEmailField;

        private string senderMobilePhoneField;

        private string senderNameField;

        private string senderNotificationTypeField;

        private string senderPhoneField;

        private string senderTimeZoneField;

        private string senderVatNumberField;

        private string shipmentTypeCodeField;

        private string shippingAccountField;

        private double shippingChargeField;

        private string shippingChargeCurrencyCodeField;

        private double shippingChargeCurrencyRateField;

        private double shippingCostField;

        private string shippingCostCurrencyCodeField;

        private double shippingCostCurrencyRateField;

        private string signatoryOnCustomsField;

        private string specialInstructions1Field;

        private string specialInstructions2Field;

        private string startVatNumberField;

        private string statusField;

        private double taxAndDutyField;

        private string taxAndDutyCurrencyCodeField;

        private double taxAndDutyCurrencyRateField;

        private string taxAndDutyStatusTextField;

        private string taxDutyDeclarationCurrencyCodeField;

        private string termsOfTradeCodeField;

        private string transactionTypeField;

        private bool twoManLiftFlagField;

        /// <remarks/>
        public bool alreadyPalletisedGoodsFlag
        {
            get
            {
                return this.alreadyPalletisedGoodsFlagField;
            }
            set
            {
                this.alreadyPalletisedGoodsFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string cardNumber
        {
            get
            {
                return this.cardNumberField;
            }
            set
            {
                this.cardNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCode
        {
            get
            {
                return this.carrierCodeField;
            }
            set
            {
                this.carrierCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierConsignmentCode
        {
            get
            {
                return this.carrierConsignmentCodeField;
            }
            set
            {
                this.carrierConsignmentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierName
        {
            get
            {
                return this.carrierNameField;
            }
            set
            {
                this.carrierNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierServiceCode
        {
            get
            {
                return this.carrierServiceCodeField;
            }
            set
            {
                this.carrierServiceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierServiceName
        {
            get
            {
                return this.carrierServiceNameField;
            }
            set
            {
                this.carrierServiceNameField = value;
            }
        }

        /// <remarks/>
        public double carrierServiceVATRate
        {
            get
            {
                return this.carrierServiceVATRateField;
            }
            set
            {
                this.carrierServiceVATRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string cartonNumber
        {
            get
            {
                return this.cartonNumberField;
            }
            set
            {
                this.cartonNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string cashOnDeliveryCurrencyCode
        {
            get
            {
                return this.cashOnDeliveryCurrencyCodeField;
            }
            set
            {
                this.cashOnDeliveryCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange committedCollectionWindow
        {
            get
            {
                return this.committedCollectionWindowField;
            }
            set
            {
                this.committedCollectionWindowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange committedDeliveryWindow
        {
            get
            {
                return this.committedDeliveryWindowField;
            }
            set
            {
                this.committedDeliveryWindowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consDestinationReference
        {
            get
            {
                return this.consDestinationReferenceField;
            }
            set
            {
                this.consDestinationReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consOriginReference
        {
            get
            {
                return this.consOriginReferenceField;
            }
            set
            {
                this.consOriginReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consRecipientReference
        {
            get
            {
                return this.consRecipientReferenceField;
            }
            set
            {
                this.consRecipientReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consReference
        {
            get
            {
                return this.consReferenceField;
            }
            set
            {
                this.consReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consSenderReference
        {
            get
            {
                return this.consSenderReferenceField;
            }
            set
            {
                this.consSenderReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consignmentCode
        {
            get
            {
                return this.consignmentCodeField;
            }
            set
            {
                this.consignmentCodeField = value;
            }
        }

        /// <remarks/>
        public bool consignmentLevelDetailsFlag
        {
            get
            {
                return this.consignmentLevelDetailsFlagField;
            }
            set
            {
                this.consignmentLevelDetailsFlagField = value;
            }
        }

        /// <remarks/>
        public double consignmentValue
        {
            get
            {
                return this.consignmentValueField;
            }
            set
            {
                this.consignmentValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consignmentValueCurrencyCode
        {
            get
            {
                return this.consignmentValueCurrencyCodeField;
            }
            set
            {
                this.consignmentValueCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double consignmentValueCurrencyRate
        {
            get
            {
                return this.consignmentValueCurrencyRateField;
            }
            set
            {
                this.consignmentValueCurrencyRateField = value;
            }
        }

        /// <remarks/>
        public double consignmentWeight
        {
            get
            {
                return this.consignmentWeightField;
            }
            set
            {
                this.consignmentWeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom1
        {
            get
            {
                return this.custom1Field;
            }
            set
            {
                this.custom1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom10
        {
            get
            {
                return this.custom10Field;
            }
            set
            {
                this.custom10Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom2
        {
            get
            {
                return this.custom2Field;
            }
            set
            {
                this.custom2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom3
        {
            get
            {
                return this.custom3Field;
            }
            set
            {
                this.custom3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom4
        {
            get
            {
                return this.custom4Field;
            }
            set
            {
                this.custom4Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom5
        {
            get
            {
                return this.custom5Field;
            }
            set
            {
                this.custom5Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom6
        {
            get
            {
                return this.custom6Field;
            }
            set
            {
                this.custom6Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom7
        {
            get
            {
                return this.custom7Field;
            }
            set
            {
                this.custom7Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom8
        {
            get
            {
                return this.custom8Field;
            }
            set
            {
                this.custom8Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string custom9
        {
            get
            {
                return this.custom9Field;
            }
            set
            {
                this.custom9Field = value;
            }
        }

        /// <remarks/>
        public bool customsDocumentationRequired
        {
            get
            {
                return this.customsDocumentationRequiredField;
            }
            set
            {
                this.customsDocumentationRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> cutOffDate
        {
            get
            {
                return this.cutOffDateField;
            }
            set
            {
                this.cutOffDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> despatchDate
        {
            get
            {
                return this.despatchDateField;
            }
            set
            {
                this.despatchDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> earliestDeliveryDate
        {
            get
            {
                return this.earliestDeliveryDateField;
            }
            set
            {
                this.earliestDeliveryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string endVatNumber
        {
            get
            {
                return this.endVatNumberField;
            }
            set
            {
                this.endVatNumberField = value;
            }
        }

        /// <remarks/>
        public bool fragileGoodsFlag
        {
            get
            {
                return this.fragileGoodsFlagField;
            }
            set
            {
                this.fragileGoodsFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> guaranteedDeliveryDate
        {
            get
            {
                return this.guaranteedDeliveryDateField;
            }
            set
            {
                this.guaranteedDeliveryDateField = value;
            }
        }

        /// <remarks/>
        public bool hazardousGoodsFlag
        {
            get
            {
                return this.hazardousGoodsFlagField;
            }
            set
            {
                this.hazardousGoodsFlagField = value;
            }
        }

        /// <remarks/>
        public double insuranceValue
        {
            get
            {
                return this.insuranceValueField;
            }
            set
            {
                this.insuranceValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string insuranceValueCurrencyCode
        {
            get
            {
                return this.insuranceValueCurrencyCodeField;
            }
            set
            {
                this.insuranceValueCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double insuranceValueCurrencyRate
        {
            get
            {
                return this.insuranceValueCurrencyRateField;
            }
            set
            {
                this.insuranceValueCurrencyRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string languageCode
        {
            get
            {
                return this.languageCodeField;
            }
            set
            {
                this.languageCodeField = value;
            }
        }

        /// <remarks/>
        public bool liquidGoodsFlag
        {
            get
            {
                return this.liquidGoodsFlagField;
            }
            set
            {
                this.liquidGoodsFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string manifestGroupCode
        {
            get
            {
                return this.manifestGroupCodeField;
            }
            set
            {
                this.manifestGroupCodeField = value;
            }
        }

        /// <remarks/>
        public double maxDimension
        {
            get
            {
                return this.maxDimensionField;
            }
            set
            {
                this.maxDimensionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string metaCampaignKey
        {
            get
            {
                return this.metaCampaignKeyField;
            }
            set
            {
                this.metaCampaignKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string metaCustomerKey
        {
            get
            {
                return this.metaCustomerKeyField;
            }
            set
            {
                this.metaCustomerKeyField = value;
            }
        }

        /// <remarks/>
        public bool moreThanOneMetreGoodsFlag
        {
            get
            {
                return this.moreThanOneMetreGoodsFlagField;
            }
            set
            {
                this.moreThanOneMetreGoodsFlagField = value;
            }
        }

        /// <remarks/>
        public bool moreThanTwentyFiveKgGoodsFlag
        {
            get
            {
                return this.moreThanTwentyFiveKgGoodsFlagField;
            }
            set
            {
                this.moreThanTwentyFiveKgGoodsFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> orderDate
        {
            get
            {
                return this.orderDateField;
            }
            set
            {
                this.orderDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string orderNumber
        {
            get
            {
                return this.orderNumberField;
            }
            set
            {
                this.orderNumberField = value;
            }
        }

        /// <remarks/>
        public double orderValue
        {
            get
            {
                return this.orderValueField;
            }
            set
            {
                this.orderValueField = value;
            }
        }

        /// <remarks/>
        public int parcelCount
        {
            get
            {
                return this.parcelCountField;
            }
            set
            {
                this.parcelCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Parcel[] parcels
        {
            get
            {
                return this.parcelsField;
            }
            set
            {
                this.parcelsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string pickTicketNumber
        {
            get
            {
                return this.pickTicketNumberField;
            }
            set
            {
                this.pickTicketNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string pickupPoint
        {
            get
            {
                return this.pickupPointField;
            }
            set
            {
                this.pickupPointField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string podRequired
        {
            get
            {
                return this.podRequiredField;
            }
            set
            {
                this.podRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Property[] properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Address recipientAddress
        {
            get
            {
                return this.recipientAddressField;
            }
            set
            {
                this.recipientAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientCode
        {
            get
            {
                return this.recipientCodeField;
            }
            set
            {
                this.recipientCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientContactPhone
        {
            get
            {
                return this.recipientContactPhoneField;
            }
            set
            {
                this.recipientContactPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientEmail
        {
            get
            {
                return this.recipientEmailField;
            }
            set
            {
                this.recipientEmailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientMobilePhone
        {
            get
            {
                return this.recipientMobilePhoneField;
            }
            set
            {
                this.recipientMobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientName
        {
            get
            {
                return this.recipientNameField;
            }
            set
            {
                this.recipientNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientNotificationType
        {
            get
            {
                return this.recipientNotificationTypeField;
            }
            set
            {
                this.recipientNotificationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientPhone
        {
            get
            {
                return this.recipientPhoneField;
            }
            set
            {
                this.recipientPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientTimeZone
        {
            get
            {
                return this.recipientTimeZoneField;
            }
            set
            {
                this.recipientTimeZoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientVatNumber
        {
            get
            {
                return this.recipientVatNumberField;
            }
            set
            {
                this.recipientVatNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Address returnAddress
        {
            get
            {
                return this.returnAddressField;
            }
            set
            {
                this.returnAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Address senderAddress
        {
            get
            {
                return this.senderAddressField;
            }
            set
            {
                this.senderAddressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderCode
        {
            get
            {
                return this.senderCodeField;
            }
            set
            {
                this.senderCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderContactPhone
        {
            get
            {
                return this.senderContactPhoneField;
            }
            set
            {
                this.senderContactPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderEmail
        {
            get
            {
                return this.senderEmailField;
            }
            set
            {
                this.senderEmailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderMobilePhone
        {
            get
            {
                return this.senderMobilePhoneField;
            }
            set
            {
                this.senderMobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderName
        {
            get
            {
                return this.senderNameField;
            }
            set
            {
                this.senderNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderNotificationType
        {
            get
            {
                return this.senderNotificationTypeField;
            }
            set
            {
                this.senderNotificationTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderPhone
        {
            get
            {
                return this.senderPhoneField;
            }
            set
            {
                this.senderPhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderTimeZone
        {
            get
            {
                return this.senderTimeZoneField;
            }
            set
            {
                this.senderTimeZoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderVatNumber
        {
            get
            {
                return this.senderVatNumberField;
            }
            set
            {
                this.senderVatNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string shipmentTypeCode
        {
            get
            {
                return this.shipmentTypeCodeField;
            }
            set
            {
                this.shipmentTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string shippingAccount
        {
            get
            {
                return this.shippingAccountField;
            }
            set
            {
                this.shippingAccountField = value;
            }
        }

        /// <remarks/>
        public double shippingCharge
        {
            get
            {
                return this.shippingChargeField;
            }
            set
            {
                this.shippingChargeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string shippingChargeCurrencyCode
        {
            get
            {
                return this.shippingChargeCurrencyCodeField;
            }
            set
            {
                this.shippingChargeCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double shippingChargeCurrencyRate
        {
            get
            {
                return this.shippingChargeCurrencyRateField;
            }
            set
            {
                this.shippingChargeCurrencyRateField = value;
            }
        }

        /// <remarks/>
        public double shippingCost
        {
            get
            {
                return this.shippingCostField;
            }
            set
            {
                this.shippingCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string shippingCostCurrencyCode
        {
            get
            {
                return this.shippingCostCurrencyCodeField;
            }
            set
            {
                this.shippingCostCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double shippingCostCurrencyRate
        {
            get
            {
                return this.shippingCostCurrencyRateField;
            }
            set
            {
                this.shippingCostCurrencyRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string signatoryOnCustoms
        {
            get
            {
                return this.signatoryOnCustomsField;
            }
            set
            {
                this.signatoryOnCustomsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string specialInstructions1
        {
            get
            {
                return this.specialInstructions1Field;
            }
            set
            {
                this.specialInstructions1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string specialInstructions2
        {
            get
            {
                return this.specialInstructions2Field;
            }
            set
            {
                this.specialInstructions2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string startVatNumber
        {
            get
            {
                return this.startVatNumberField;
            }
            set
            {
                this.startVatNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public double taxAndDuty
        {
            get
            {
                return this.taxAndDutyField;
            }
            set
            {
                this.taxAndDutyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxAndDutyCurrencyCode
        {
            get
            {
                return this.taxAndDutyCurrencyCodeField;
            }
            set
            {
                this.taxAndDutyCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double taxAndDutyCurrencyRate
        {
            get
            {
                return this.taxAndDutyCurrencyRateField;
            }
            set
            {
                this.taxAndDutyCurrencyRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxAndDutyStatusText
        {
            get
            {
                return this.taxAndDutyStatusTextField;
            }
            set
            {
                this.taxAndDutyStatusTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxDutyDeclarationCurrencyCode
        {
            get
            {
                return this.taxDutyDeclarationCurrencyCodeField;
            }
            set
            {
                this.taxDutyDeclarationCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string termsOfTradeCode
        {
            get
            {
                return this.termsOfTradeCodeField;
            }
            set
            {
                this.termsOfTradeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string transactionType
        {
            get
            {
                return this.transactionTypeField;
            }
            set
            {
                this.transactionTypeField = value;
            }
        }

        /// <remarks/>
        public bool twoManLiftFlag
        {
            get
            {
                return this.twoManLiftFlagField;
            }
            set
            {
                this.twoManLiftFlagField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class DateRange
    {

        private System.Nullable<System.DateTime> fromField;

        private System.Nullable<System.DateTime> toField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> from
        {
            get
            {
                return this.fromField;
            }
            set
            {
                this.fromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> to
        {
            get
            {
                return this.toField;
            }
            set
            {
                this.toField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Labels
    {

        private string documentsPdfField;

        private string labelsPdfField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string documentsPdf
        {
            get
            {
                return this.documentsPdfField;
            }
            set
            {
                this.documentsPdfField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string labelsPdf
        {
            get
            {
                return this.labelsPdfField;
            }
            set
            {
                this.labelsPdfField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class DespatchedConsignment
    {

        private Consignment consignmentField;

        private Labels labelsField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Consignment consignment
        {
            get
            {
                return this.consignmentField;
            }
            set
            {
                this.consignmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Labels labels
        {
            get
            {
                return this.labelsField;
            }
            set
            {
                this.labelsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class DeliveryOption
    {

        private string bookingCodeField;

        private string carrierCodeField;

        private string carrierCustom1Field;

        private string carrierCustom2Field;

        private string carrierCustom3Field;

        private string carrierServiceCodeField;

        private string carrierServiceTypeCodeField;

        private DateRange[] collectionSlotsField;

        private DateRange collectionWindowField;

        private System.Nullable<System.DateTime> cutOffDateTimeField;

        private DateRange[] deliverySlotsField;

        private DateRange deliveryWindowField;

        private string[] groupCodesField;

        private string nameField;

        private bool nominatableCollectionSlotField;

        private bool nominatableDeliverySlotField;

        private string recipientTimeZoneField;

        private double scoreField;

        private string senderTimeZoneField;

        private double shippingChargeField;

        private double shippingCostField;

        private double taxAndDutyField;

        private string taxAndDutyStatusTextField;

        private double vatRateField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string bookingCode
        {
            get
            {
                return this.bookingCodeField;
            }
            set
            {
                this.bookingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCode
        {
            get
            {
                return this.carrierCodeField;
            }
            set
            {
                this.carrierCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCustom1
        {
            get
            {
                return this.carrierCustom1Field;
            }
            set
            {
                this.carrierCustom1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCustom2
        {
            get
            {
                return this.carrierCustom2Field;
            }
            set
            {
                this.carrierCustom2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCustom3
        {
            get
            {
                return this.carrierCustom3Field;
            }
            set
            {
                this.carrierCustom3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierServiceCode
        {
            get
            {
                return this.carrierServiceCodeField;
            }
            set
            {
                this.carrierServiceCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierServiceTypeCode
        {
            get
            {
                return this.carrierServiceTypeCodeField;
            }
            set
            {
                this.carrierServiceTypeCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] collectionSlots
        {
            get
            {
                return this.collectionSlotsField;
            }
            set
            {
                this.collectionSlotsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange collectionWindow
        {
            get
            {
                return this.collectionWindowField;
            }
            set
            {
                this.collectionWindowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> cutOffDateTime
        {
            get
            {
                return this.cutOffDateTimeField;
            }
            set
            {
                this.cutOffDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] deliverySlots
        {
            get
            {
                return this.deliverySlotsField;
            }
            set
            {
                this.deliverySlotsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange deliveryWindow
        {
            get
            {
                return this.deliveryWindowField;
            }
            set
            {
                this.deliveryWindowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] groupCodes
        {
            get
            {
                return this.groupCodesField;
            }
            set
            {
                this.groupCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public bool nominatableCollectionSlot
        {
            get
            {
                return this.nominatableCollectionSlotField;
            }
            set
            {
                this.nominatableCollectionSlotField = value;
            }
        }

        /// <remarks/>
        public bool nominatableDeliverySlot
        {
            get
            {
                return this.nominatableDeliverySlotField;
            }
            set
            {
                this.nominatableDeliverySlotField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientTimeZone
        {
            get
            {
                return this.recipientTimeZoneField;
            }
            set
            {
                this.recipientTimeZoneField = value;
            }
        }

        /// <remarks/>
        public double score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderTimeZone
        {
            get
            {
                return this.senderTimeZoneField;
            }
            set
            {
                this.senderTimeZoneField = value;
            }
        }

        /// <remarks/>
        public double shippingCharge
        {
            get
            {
                return this.shippingChargeField;
            }
            set
            {
                this.shippingChargeField = value;
            }
        }

        /// <remarks/>
        public double shippingCost
        {
            get
            {
                return this.shippingCostField;
            }
            set
            {
                this.shippingCostField = value;
            }
        }

        /// <remarks/>
        public double taxAndDuty
        {
            get
            {
                return this.taxAndDutyField;
            }
            set
            {
                this.taxAndDutyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxAndDutyStatusText
        {
            get
            {
                return this.taxAndDutyStatusTextField;
            }
            set
            {
                this.taxAndDutyStatusTextField = value;
            }
        }

        /// <remarks/>
        public double vatRate
        {
            get
            {
                return this.vatRateField;
            }
            set
            {
                this.vatRateField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class WorkingDays
    {

        private bool fridayField;

        private bool mondayField;

        private bool saturdayField;

        private bool sundayField;

        private bool thursdayField;

        private bool tuesdayField;

        private bool wednesdayField;

        /// <remarks/>
        public bool friday
        {
            get
            {
                return this.fridayField;
            }
            set
            {
                this.fridayField = value;
            }
        }

        /// <remarks/>
        public bool monday
        {
            get
            {
                return this.mondayField;
            }
            set
            {
                this.mondayField = value;
            }
        }

        /// <remarks/>
        public bool saturday
        {
            get
            {
                return this.saturdayField;
            }
            set
            {
                this.saturdayField = value;
            }
        }

        /// <remarks/>
        public bool sunday
        {
            get
            {
                return this.sundayField;
            }
            set
            {
                this.sundayField = value;
            }
        }

        /// <remarks/>
        public bool thursday
        {
            get
            {
                return this.thursdayField;
            }
            set
            {
                this.thursdayField = value;
            }
        }

        /// <remarks/>
        public bool tuesday
        {
            get
            {
                return this.tuesdayField;
            }
            set
            {
                this.tuesdayField = value;
            }
        }

        /// <remarks/>
        public bool wednesday
        {
            get
            {
                return this.wednesdayField;
            }
            set
            {
                this.wednesdayField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class AllocationFilter
    {

        private string[] acceptableCarrierCodesField;

        private string[] acceptableCarrierServiceCodesField;

        private string[] acceptableCarrierServiceGroupCodesField;

        private string[] acceptableCarrierServiceTypeCodesField;

        private WorkingDays acceptableCollectionDaysField;

        private DateRange[] acceptableCollectionSlotsField;

        private WorkingDays acceptableDeliveryDaysField;

        private DateRange[] acceptableDeliverySlotsField;

        private string allocationSchemeCodeField;

        private bool expandGroupsField;

        private int filterGroup1Field;

        private int filterGroup2Field;

        private int filterGroup3Field;

        private bool firstCollectionOnlyField;

        private int maxAnalysisDayCountField;

        private double maxCostField;

        private int maxDatesPerServiceField;

        private double maxScoreField;

        private double minScoreField;

        private int preFilterSortOrder1Field;

        private int preFilterSortOrder2Field;

        private int preFilterSortOrder3Field;

        private int sortOrder1Field;

        private int sortOrder2Field;

        private int sortOrder3Field;

        private string[] unacceptableCarrierCodesField;

        private string[] unacceptableCarrierServiceCodesField;

        private string[] unacceptableCarrierServiceGroupCodesField;

        private string[] unacceptableCarrierServiceTypeCodesField;

        private WorkingDays unacceptableCollectionDaysField;

        private DateRange[] unacceptableCollectionSlotsField;

        private WorkingDays unacceptableDeliveryDaysField;

        private DateRange[] unacceptableDeliverySlotsField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] acceptableCarrierCodes
        {
            get
            {
                return this.acceptableCarrierCodesField;
            }
            set
            {
                this.acceptableCarrierCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] acceptableCarrierServiceCodes
        {
            get
            {
                return this.acceptableCarrierServiceCodesField;
            }
            set
            {
                this.acceptableCarrierServiceCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] acceptableCarrierServiceGroupCodes
        {
            get
            {
                return this.acceptableCarrierServiceGroupCodesField;
            }
            set
            {
                this.acceptableCarrierServiceGroupCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] acceptableCarrierServiceTypeCodes
        {
            get
            {
                return this.acceptableCarrierServiceTypeCodesField;
            }
            set
            {
                this.acceptableCarrierServiceTypeCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public WorkingDays acceptableCollectionDays
        {
            get
            {
                return this.acceptableCollectionDaysField;
            }
            set
            {
                this.acceptableCollectionDaysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] acceptableCollectionSlots
        {
            get
            {
                return this.acceptableCollectionSlotsField;
            }
            set
            {
                this.acceptableCollectionSlotsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public WorkingDays acceptableDeliveryDays
        {
            get
            {
                return this.acceptableDeliveryDaysField;
            }
            set
            {
                this.acceptableDeliveryDaysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] acceptableDeliverySlots
        {
            get
            {
                return this.acceptableDeliverySlotsField;
            }
            set
            {
                this.acceptableDeliverySlotsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string allocationSchemeCode
        {
            get
            {
                return this.allocationSchemeCodeField;
            }
            set
            {
                this.allocationSchemeCodeField = value;
            }
        }

        /// <remarks/>
        public bool expandGroups
        {
            get
            {
                return this.expandGroupsField;
            }
            set
            {
                this.expandGroupsField = value;
            }
        }

        /// <remarks/>
        public int filterGroup1
        {
            get
            {
                return this.filterGroup1Field;
            }
            set
            {
                this.filterGroup1Field = value;
            }
        }

        /// <remarks/>
        public int filterGroup2
        {
            get
            {
                return this.filterGroup2Field;
            }
            set
            {
                this.filterGroup2Field = value;
            }
        }

        /// <remarks/>
        public int filterGroup3
        {
            get
            {
                return this.filterGroup3Field;
            }
            set
            {
                this.filterGroup3Field = value;
            }
        }

        /// <remarks/>
        public bool firstCollectionOnly
        {
            get
            {
                return this.firstCollectionOnlyField;
            }
            set
            {
                this.firstCollectionOnlyField = value;
            }
        }

        /// <remarks/>
        public int maxAnalysisDayCount
        {
            get
            {
                return this.maxAnalysisDayCountField;
            }
            set
            {
                this.maxAnalysisDayCountField = value;
            }
        }

        /// <remarks/>
        public double maxCost
        {
            get
            {
                return this.maxCostField;
            }
            set
            {
                this.maxCostField = value;
            }
        }

        /// <remarks/>
        public int maxDatesPerService
        {
            get
            {
                return this.maxDatesPerServiceField;
            }
            set
            {
                this.maxDatesPerServiceField = value;
            }
        }

        /// <remarks/>
        public double maxScore
        {
            get
            {
                return this.maxScoreField;
            }
            set
            {
                this.maxScoreField = value;
            }
        }

        /// <remarks/>
        public double minScore
        {
            get
            {
                return this.minScoreField;
            }
            set
            {
                this.minScoreField = value;
            }
        }

        /// <remarks/>
        public int preFilterSortOrder1
        {
            get
            {
                return this.preFilterSortOrder1Field;
            }
            set
            {
                this.preFilterSortOrder1Field = value;
            }
        }

        /// <remarks/>
        public int preFilterSortOrder2
        {
            get
            {
                return this.preFilterSortOrder2Field;
            }
            set
            {
                this.preFilterSortOrder2Field = value;
            }
        }

        /// <remarks/>
        public int preFilterSortOrder3
        {
            get
            {
                return this.preFilterSortOrder3Field;
            }
            set
            {
                this.preFilterSortOrder3Field = value;
            }
        }

        /// <remarks/>
        public int sortOrder1
        {
            get
            {
                return this.sortOrder1Field;
            }
            set
            {
                this.sortOrder1Field = value;
            }
        }

        /// <remarks/>
        public int sortOrder2
        {
            get
            {
                return this.sortOrder2Field;
            }
            set
            {
                this.sortOrder2Field = value;
            }
        }

        /// <remarks/>
        public int sortOrder3
        {
            get
            {
                return this.sortOrder3Field;
            }
            set
            {
                this.sortOrder3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] unacceptableCarrierCodes
        {
            get
            {
                return this.unacceptableCarrierCodesField;
            }
            set
            {
                this.unacceptableCarrierCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] unacceptableCarrierServiceCodes
        {
            get
            {
                return this.unacceptableCarrierServiceCodesField;
            }
            set
            {
                this.unacceptableCarrierServiceCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] unacceptableCarrierServiceGroupCodes
        {
            get
            {
                return this.unacceptableCarrierServiceGroupCodesField;
            }
            set
            {
                this.unacceptableCarrierServiceGroupCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] unacceptableCarrierServiceTypeCodes
        {
            get
            {
                return this.unacceptableCarrierServiceTypeCodesField;
            }
            set
            {
                this.unacceptableCarrierServiceTypeCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public WorkingDays unacceptableCollectionDays
        {
            get
            {
                return this.unacceptableCollectionDaysField;
            }
            set
            {
                this.unacceptableCollectionDaysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] unacceptableCollectionSlots
        {
            get
            {
                return this.unacceptableCollectionSlotsField;
            }
            set
            {
                this.unacceptableCollectionSlotsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public WorkingDays unacceptableDeliveryDays
        {
            get
            {
                return this.unacceptableDeliveryDaysField;
            }
            set
            {
                this.unacceptableDeliveryDaysField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public DateRange[] unacceptableDeliverySlots
        {
            get
            {
                return this.unacceptableDeliverySlotsField;
            }
            set
            {
                this.unacceptableDeliverySlotsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(VerifiedAddress))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Address
    {

        private string companyNameField;

        private string countryCodeField;

        private string line1Field;

        private string line2Field;

        private string line3Field;

        private string line4Field;

        private string postCodeField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string companyName
        {
            get
            {
                return this.companyNameField;
            }
            set
            {
                this.companyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string countryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string line1
        {
            get
            {
                return this.line1Field;
            }
            set
            {
                this.line1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string line2
        {
            get
            {
                return this.line2Field;
            }
            set
            {
                this.line2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string line3
        {
            get
            {
                return this.line3Field;
            }
            set
            {
                this.line3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string line4
        {
            get
            {
                return this.line4Field;
            }
            set
            {
                this.line4Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string postCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class VerifiedAddress : Address
    {

        private bool changedCompanyNameField;

        private bool changedCountryCodeField;

        private bool changedLine1Field;

        private bool changedLine2Field;

        private bool changedLine3Field;

        private bool changedLine4Field;

        private bool changedPostCodeField;

        private string statusField;

        /// <remarks/>
        public bool changedCompanyName
        {
            get
            {
                return this.changedCompanyNameField;
            }
            set
            {
                this.changedCompanyNameField = value;
            }
        }

        /// <remarks/>
        public bool changedCountryCode
        {
            get
            {
                return this.changedCountryCodeField;
            }
            set
            {
                this.changedCountryCodeField = value;
            }
        }

        /// <remarks/>
        public bool changedLine1
        {
            get
            {
                return this.changedLine1Field;
            }
            set
            {
                this.changedLine1Field = value;
            }
        }

        /// <remarks/>
        public bool changedLine2
        {
            get
            {
                return this.changedLine2Field;
            }
            set
            {
                this.changedLine2Field = value;
            }
        }

        /// <remarks/>
        public bool changedLine3
        {
            get
            {
                return this.changedLine3Field;
            }
            set
            {
                this.changedLine3Field = value;
            }
        }

        /// <remarks/>
        public bool changedLine4
        {
            get
            {
                return this.changedLine4Field;
            }
            set
            {
                this.changedLine4Field = value;
            }
        }

        /// <remarks/>
        public bool changedPostCode
        {
            get
            {
                return this.changedPostCodeField;
            }
            set
            {
                this.changedPostCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Property
    {

        private string propertyNameField;

        private string propertyValueField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string propertyName
        {
            get
            {
                return this.propertyNameField;
            }
            set
            {
                this.propertyNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string propertyValue
        {
            get
            {
                return this.propertyValueField;
            }
            set
            {
                this.propertyValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Product
    {

        private string countryOfOriginField;

        private string fabricContentField;

        private string harmonisedProductCodeField;

        private string[] miscellaneousInfoField;

        private string productCodeField;

        private string productDescriptionField;

        private long productQuantityField;

        private string productTypeDescriptionField;

        private double totalProductValueField;

        private double unitProductWeightField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string countryOfOrigin
        {
            get
            {
                return this.countryOfOriginField;
            }
            set
            {
                this.countryOfOriginField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string fabricContent
        {
            get
            {
                return this.fabricContentField;
            }
            set
            {
                this.fabricContentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string harmonisedProductCode
        {
            get
            {
                return this.harmonisedProductCodeField;
            }
            set
            {
                this.harmonisedProductCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] miscellaneousInfo
        {
            get
            {
                return this.miscellaneousInfoField;
            }
            set
            {
                this.miscellaneousInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string productCode
        {
            get
            {
                return this.productCodeField;
            }
            set
            {
                this.productCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string productDescription
        {
            get
            {
                return this.productDescriptionField;
            }
            set
            {
                this.productDescriptionField = value;
            }
        }

        /// <remarks/>
        public long productQuantity
        {
            get
            {
                return this.productQuantityField;
            }
            set
            {
                this.productQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string productTypeDescription
        {
            get
            {
                return this.productTypeDescriptionField;
            }
            set
            {
                this.productTypeDescriptionField = value;
            }
        }

        /// <remarks/>
        public double totalProductValue
        {
            get
            {
                return this.totalProductValueField;
            }
            set
            {
                this.totalProductValueField = value;
            }
        }

        /// <remarks/>
        public double unitProductWeight
        {
            get
            {
                return this.unitProductWeightField;
            }
            set
            {
                this.unitProductWeightField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Parcel
    {

        private string cartonIdField;

        private string codeField;

        private string destinationReferenceField;

        private double dutyPaidField;

        private int numberField;

        private string originReferenceField;

        private string outerConsignmentCodeField;

        private int outerParcelNumberField;

        private double parcelDepthField;

        private double parcelHeightField;

        private double parcelValueField;

        private double parcelWeightField;

        private double parcelWidthField;

        private Product[] productsField;

        private string recipientReferenceField;

        private string referenceField;

        private string senderReferenceField;

        private string trackingCodeField;

        private string trackingUrlField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string cartonId
        {
            get
            {
                return this.cartonIdField;
            }
            set
            {
                this.cartonIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string destinationReference
        {
            get
            {
                return this.destinationReferenceField;
            }
            set
            {
                this.destinationReferenceField = value;
            }
        }

        /// <remarks/>
        public double dutyPaid
        {
            get
            {
                return this.dutyPaidField;
            }
            set
            {
                this.dutyPaidField = value;
            }
        }

        /// <remarks/>
        public int number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string originReference
        {
            get
            {
                return this.originReferenceField;
            }
            set
            {
                this.originReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string outerConsignmentCode
        {
            get
            {
                return this.outerConsignmentCodeField;
            }
            set
            {
                this.outerConsignmentCodeField = value;
            }
        }

        /// <remarks/>
        public int outerParcelNumber
        {
            get
            {
                return this.outerParcelNumberField;
            }
            set
            {
                this.outerParcelNumberField = value;
            }
        }

        /// <remarks/>
        public double parcelDepth
        {
            get
            {
                return this.parcelDepthField;
            }
            set
            {
                this.parcelDepthField = value;
            }
        }

        /// <remarks/>
        public double parcelHeight
        {
            get
            {
                return this.parcelHeightField;
            }
            set
            {
                this.parcelHeightField = value;
            }
        }

        /// <remarks/>
        public double parcelValue
        {
            get
            {
                return this.parcelValueField;
            }
            set
            {
                this.parcelValueField = value;
            }
        }

        /// <remarks/>
        public double parcelWeight
        {
            get
            {
                return this.parcelWeightField;
            }
            set
            {
                this.parcelWeightField = value;
            }
        }

        /// <remarks/>
        public double parcelWidth
        {
            get
            {
                return this.parcelWidthField;
            }
            set
            {
                this.parcelWidthField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Product[] products
        {
            get
            {
                return this.productsField;
            }
            set
            {
                this.productsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string recipientReference
        {
            get
            {
                return this.recipientReferenceField;
            }
            set
            {
                this.recipientReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string senderReference
        {
            get
            {
                return this.senderReferenceField;
            }
            set
            {
                this.senderReferenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string trackingCode
        {
            get
            {
                return this.trackingCodeField;
            }
            set
            {
                this.trackingCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string trackingUrl
        {
            get
            {
                return this.trackingUrlField;
            }
            set
            {
                this.trackingUrlField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void deallocateCompletedEventHandler(object sender, deallocateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deallocateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal deallocateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findDeliveryOptionsCompletedEventHandler(object sender, findDeliveryOptionsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findDeliveryOptionsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findDeliveryOptionsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DeliveryOption[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DeliveryOption[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findDeliveryOptionsWithBookingCodeCompletedEventHandler(object sender, findDeliveryOptionsWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findDeliveryOptionsWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findDeliveryOptionsWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DeliveryOption[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DeliveryOption[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findDeliveryOptionsForConsignmentCompletedEventHandler(object sender, findDeliveryOptionsForConsignmentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findDeliveryOptionsForConsignmentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findDeliveryOptionsForConsignmentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DeliveryOption[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DeliveryOption[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventHandler(object sender, findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findDeliveryOptionsForConsignmentWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DeliveryOption[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DeliveryOption[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createAndAllocateConsignmentsCompletedEventHandler(object sender, createAndAllocateConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createAndAllocateConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createAndAllocateConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createAndAllocateConsignmentsWithBookingCodeCompletedEventHandler(object sender, createAndAllocateConsignmentsWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createAndAllocateConsignmentsWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createAndAllocateConsignmentsWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void verifyAllocationCompletedEventHandler(object sender, verifyAllocationCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class verifyAllocationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal verifyAllocationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void allocateConsignmentsCompletedEventHandler(object sender, allocateConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class allocateConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal allocateConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void allocateConsignmentsWithBookingCodeCompletedEventHandler(object sender, allocateConsignmentsWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class allocateConsignmentsWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal allocateConsignmentsWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void despatchConsignmentCompletedEventHandler(object sender, despatchConsignmentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class despatchConsignmentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal despatchConsignmentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DespatchedConsignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DespatchedConsignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void despatchConsignmentWithBookingCodeCompletedEventHandler(object sender, despatchConsignmentWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class despatchConsignmentWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal despatchConsignmentWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public DespatchedConsignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((DespatchedConsignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void verifyAllocationWithBookingCodeCompletedEventHandler(object sender, verifyAllocationWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class verifyAllocationWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal verifyAllocationWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ConsignmentSearchServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchRecord))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchOrderBy))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(SearchCondition))]
    public partial class ConsignmentSearchServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback searchOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentByConsignmentCodeOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentsByOrderReferenceOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentsByCarrierConsignmentCodeOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentsByCartonIdOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentsByParcelCodeOperationCompleted;

        private System.Threading.SendOrPostCallback searchForConsignmentsOperationCompleted;

        /// <remarks/>
        public ConsignmentSearchServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/ConsignmentSearchService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event searchCompletedEventHandler searchCompleted;

        /// <remarks/>
        public event findConsignmentByConsignmentCodeCompletedEventHandler findConsignmentByConsignmentCodeCompleted;

        /// <remarks/>
        public event findConsignmentsByOrderReferenceCompletedEventHandler findConsignmentsByOrderReferenceCompleted;

        /// <remarks/>
        public event findConsignmentsByCarrierConsignmentCodeCompletedEventHandler findConsignmentsByCarrierConsignmentCodeCompleted;

        /// <remarks/>
        public event findConsignmentsByCartonIdCompletedEventHandler findConsignmentsByCartonIdCompleted;

        /// <remarks/>
        public event findConsignmentsByParcelCodeCompletedEventHandler findConsignmentsByParcelCodeCompleted;

        /// <remarks/>
        public event searchForConsignmentsCompletedEventHandler searchForConsignmentsCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("searchReturn")]
        public SearchResult search(SearchParams parameters)
        {
            object[] results = this.Invoke("search", new object[] {
                    parameters});
            return ((SearchResult)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginsearch(SearchParams parameters, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("search", new object[] {
                    parameters}, callback, asyncState);
        }

        /// <remarks/>
        public SearchResult Endsearch(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((SearchResult)(results[0]));
        }

        /// <remarks/>
        public void searchAsync(SearchParams parameters)
        {
            this.searchAsync(parameters, null);
        }

        /// <remarks/>
        public void searchAsync(SearchParams parameters, object userState)
        {
            if ((this.searchOperationCompleted == null))
            {
                this.searchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsearchOperationCompleted);
            }
            this.InvokeAsync("search", new object[] {
                    parameters}, this.searchOperationCompleted, userState);
        }

        private void OnsearchOperationCompleted(object arg)
        {
            if ((this.searchCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.searchCompleted(this, new searchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentByConsignmentCodeReturn")]
        public Consignment findConsignmentByConsignmentCode(string consignmentCode)
        {
            object[] results = this.Invoke("findConsignmentByConsignmentCode", new object[] {
                    consignmentCode});
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentByConsignmentCode(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentByConsignmentCode", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment EndfindConsignmentByConsignmentCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public void findConsignmentByConsignmentCodeAsync(string consignmentCode)
        {
            this.findConsignmentByConsignmentCodeAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void findConsignmentByConsignmentCodeAsync(string consignmentCode, object userState)
        {
            if ((this.findConsignmentByConsignmentCodeOperationCompleted == null))
            {
                this.findConsignmentByConsignmentCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentByConsignmentCodeOperationCompleted);
            }
            this.InvokeAsync("findConsignmentByConsignmentCode", new object[] {
                    consignmentCode}, this.findConsignmentByConsignmentCodeOperationCompleted, userState);
        }

        private void OnfindConsignmentByConsignmentCodeOperationCompleted(object arg)
        {
            if ((this.findConsignmentByConsignmentCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentByConsignmentCodeCompleted(this, new findConsignmentByConsignmentCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentsByOrderReferenceReturn")]
        public Consignment[] findConsignmentsByOrderReference(string orderReference)
        {
            object[] results = this.Invoke("findConsignmentsByOrderReference", new object[] {
                    orderReference});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentsByOrderReference(string orderReference, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentsByOrderReference", new object[] {
                    orderReference}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndfindConsignmentsByOrderReference(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentsByOrderReferenceAsync(string orderReference)
        {
            this.findConsignmentsByOrderReferenceAsync(orderReference, null);
        }

        /// <remarks/>
        public void findConsignmentsByOrderReferenceAsync(string orderReference, object userState)
        {
            if ((this.findConsignmentsByOrderReferenceOperationCompleted == null))
            {
                this.findConsignmentsByOrderReferenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentsByOrderReferenceOperationCompleted);
            }
            this.InvokeAsync("findConsignmentsByOrderReference", new object[] {
                    orderReference}, this.findConsignmentsByOrderReferenceOperationCompleted, userState);
        }

        private void OnfindConsignmentsByOrderReferenceOperationCompleted(object arg)
        {
            if ((this.findConsignmentsByOrderReferenceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentsByOrderReferenceCompleted(this, new findConsignmentsByOrderReferenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentsByCarrierConsignmentCodeReturn")]
        public Consignment[] findConsignmentsByCarrierConsignmentCode(string carrierConsignmentCode)
        {
            object[] results = this.Invoke("findConsignmentsByCarrierConsignmentCode", new object[] {
                    carrierConsignmentCode});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentsByCarrierConsignmentCode(string carrierConsignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentsByCarrierConsignmentCode", new object[] {
                    carrierConsignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndfindConsignmentsByCarrierConsignmentCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentsByCarrierConsignmentCodeAsync(string carrierConsignmentCode)
        {
            this.findConsignmentsByCarrierConsignmentCodeAsync(carrierConsignmentCode, null);
        }

        /// <remarks/>
        public void findConsignmentsByCarrierConsignmentCodeAsync(string carrierConsignmentCode, object userState)
        {
            if ((this.findConsignmentsByCarrierConsignmentCodeOperationCompleted == null))
            {
                this.findConsignmentsByCarrierConsignmentCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentsByCarrierConsignmentCodeOperationCompleted);
            }
            this.InvokeAsync("findConsignmentsByCarrierConsignmentCode", new object[] {
                    carrierConsignmentCode}, this.findConsignmentsByCarrierConsignmentCodeOperationCompleted, userState);
        }

        private void OnfindConsignmentsByCarrierConsignmentCodeOperationCompleted(object arg)
        {
            if ((this.findConsignmentsByCarrierConsignmentCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentsByCarrierConsignmentCodeCompleted(this, new findConsignmentsByCarrierConsignmentCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentsByCartonIdReturn")]
        public Consignment[] findConsignmentsByCartonId(string cartonId)
        {
            object[] results = this.Invoke("findConsignmentsByCartonId", new object[] {
                    cartonId});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentsByCartonId(string cartonId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentsByCartonId", new object[] {
                    cartonId}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndfindConsignmentsByCartonId(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentsByCartonIdAsync(string cartonId)
        {
            this.findConsignmentsByCartonIdAsync(cartonId, null);
        }

        /// <remarks/>
        public void findConsignmentsByCartonIdAsync(string cartonId, object userState)
        {
            if ((this.findConsignmentsByCartonIdOperationCompleted == null))
            {
                this.findConsignmentsByCartonIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentsByCartonIdOperationCompleted);
            }
            this.InvokeAsync("findConsignmentsByCartonId", new object[] {
                    cartonId}, this.findConsignmentsByCartonIdOperationCompleted, userState);
        }

        private void OnfindConsignmentsByCartonIdOperationCompleted(object arg)
        {
            if ((this.findConsignmentsByCartonIdCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentsByCartonIdCompleted(this, new findConsignmentsByCartonIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentsByParcelCodeReturn")]
        public Consignment[] findConsignmentsByParcelCode(string parcelCode)
        {
            object[] results = this.Invoke("findConsignmentsByParcelCode", new object[] {
                    parcelCode});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentsByParcelCode(string parcelCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentsByParcelCode", new object[] {
                    parcelCode}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndfindConsignmentsByParcelCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentsByParcelCodeAsync(string parcelCode)
        {
            this.findConsignmentsByParcelCodeAsync(parcelCode, null);
        }

        /// <remarks/>
        public void findConsignmentsByParcelCodeAsync(string parcelCode, object userState)
        {
            if ((this.findConsignmentsByParcelCodeOperationCompleted == null))
            {
                this.findConsignmentsByParcelCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentsByParcelCodeOperationCompleted);
            }
            this.InvokeAsync("findConsignmentsByParcelCode", new object[] {
                    parcelCode}, this.findConsignmentsByParcelCodeOperationCompleted, userState);
        }

        private void OnfindConsignmentsByParcelCodeOperationCompleted(object arg)
        {
            if ((this.findConsignmentsByParcelCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentsByParcelCodeCompleted(this, new findConsignmentsByParcelCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("searchForConsignmentsReturn")]
        public ConsignmentSearchResult searchForConsignments(ConsignmentSearchParams parameters)
        {
            object[] results = this.Invoke("searchForConsignments", new object[] {
                    parameters});
            return ((ConsignmentSearchResult)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginsearchForConsignments(ConsignmentSearchParams parameters, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("searchForConsignments", new object[] {
                    parameters}, callback, asyncState);
        }

        /// <remarks/>
        public ConsignmentSearchResult EndsearchForConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ConsignmentSearchResult)(results[0]));
        }

        /// <remarks/>
        public void searchForConsignmentsAsync(ConsignmentSearchParams parameters)
        {
            this.searchForConsignmentsAsync(parameters, null);
        }

        /// <remarks/>
        public void searchForConsignmentsAsync(ConsignmentSearchParams parameters, object userState)
        {
            if ((this.searchForConsignmentsOperationCompleted == null))
            {
                this.searchForConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsearchForConsignmentsOperationCompleted);
            }
            this.InvokeAsync("searchForConsignments", new object[] {
                    parameters}, this.searchForConsignmentsOperationCompleted, userState);
        }

        private void OnsearchForConsignmentsOperationCompleted(object arg)
        {
            if ((this.searchForConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.searchForConsignmentsCompleted(this, new searchForConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class SearchParams
    {

        private SearchCondition[] conditionsField;

        private bool distinctField;

        private SearchOrderBy[] orderByFieldsField;

        private int pageNumberField;

        private int pageSizeField;

        private string[] selectFieldsField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public SearchCondition[] conditions
        {
            get
            {
                return this.conditionsField;
            }
            set
            {
                this.conditionsField = value;
            }
        }

        /// <remarks/>
        public bool distinct
        {
            get
            {
                return this.distinctField;
            }
            set
            {
                this.distinctField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public SearchOrderBy[] orderByFields
        {
            get
            {
                return this.orderByFieldsField;
            }
            set
            {
                this.orderByFieldsField = value;
            }
        }

        /// <remarks/>
        public int pageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        public int pageSize
        {
            get
            {
                return this.pageSizeField;
            }
            set
            {
                this.pageSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] selectFields
        {
            get
            {
                return this.selectFieldsField;
            }
            set
            {
                this.selectFieldsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class SearchCondition
    {

        private string fieldField;

        private string operandField;

        private string operatorField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string operand
        {
            get
            {
                return this.operandField;
            }
            set
            {
                this.operandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string @operator
        {
            get
            {
                return this.operatorField;
            }
            set
            {
                this.operatorField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ConsignmentSearchResult
    {

        private Consignment[] consignmentsField;

        private int pageCountField;

        private int totalCountField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Consignment[] consignments
        {
            get
            {
                return this.consignmentsField;
            }
            set
            {
                this.consignmentsField = value;
            }
        }

        /// <remarks/>
        public int pageCount
        {
            get
            {
                return this.pageCountField;
            }
            set
            {
                this.pageCountField = value;
            }
        }

        /// <remarks/>
        public int totalCount
        {
            get
            {
                return this.totalCountField;
            }
            set
            {
                this.totalCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ConsignmentSearchParams
    {

        private string[] carrierCodesField;

        private string[] consignmentStatusesField;

        private System.Nullable<System.DateTime> dateAllocatedAfterField;

        private System.Nullable<System.DateTime> dateAllocatedBeforeField;

        private System.Nullable<System.DateTime> dateCompletedAfterField;

        private System.Nullable<System.DateTime> dateCompletedBeforeField;

        private System.Nullable<System.DateTime> dateCreatedAfterField;

        private System.Nullable<System.DateTime> dateCreatedBeforeField;

        private System.Nullable<System.DateTime> dateDeliveryAfterField;

        private System.Nullable<System.DateTime> dateDeliveryBeforeField;

        private System.Nullable<System.DateTime> dateDespatchAfterField;

        private System.Nullable<System.DateTime> dateDespatchBeforeField;

        private System.Nullable<System.DateTime> dateModifiedAfterField;

        private System.Nullable<System.DateTime> dateModifiedBeforeField;

        private int pageNumberField;

        private int pageSizeField;

        private string transactionTypeField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] carrierCodes
        {
            get
            {
                return this.carrierCodesField;
            }
            set
            {
                this.carrierCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] consignmentStatuses
        {
            get
            {
                return this.consignmentStatusesField;
            }
            set
            {
                this.consignmentStatusesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateAllocatedAfter
        {
            get
            {
                return this.dateAllocatedAfterField;
            }
            set
            {
                this.dateAllocatedAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateAllocatedBefore
        {
            get
            {
                return this.dateAllocatedBeforeField;
            }
            set
            {
                this.dateAllocatedBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateCompletedAfter
        {
            get
            {
                return this.dateCompletedAfterField;
            }
            set
            {
                this.dateCompletedAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateCompletedBefore
        {
            get
            {
                return this.dateCompletedBeforeField;
            }
            set
            {
                this.dateCompletedBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateCreatedAfter
        {
            get
            {
                return this.dateCreatedAfterField;
            }
            set
            {
                this.dateCreatedAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateCreatedBefore
        {
            get
            {
                return this.dateCreatedBeforeField;
            }
            set
            {
                this.dateCreatedBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateDeliveryAfter
        {
            get
            {
                return this.dateDeliveryAfterField;
            }
            set
            {
                this.dateDeliveryAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateDeliveryBefore
        {
            get
            {
                return this.dateDeliveryBeforeField;
            }
            set
            {
                this.dateDeliveryBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateDespatchAfter
        {
            get
            {
                return this.dateDespatchAfterField;
            }
            set
            {
                this.dateDespatchAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateDespatchBefore
        {
            get
            {
                return this.dateDespatchBeforeField;
            }
            set
            {
                this.dateDespatchBeforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateModifiedAfter
        {
            get
            {
                return this.dateModifiedAfterField;
            }
            set
            {
                this.dateModifiedAfterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> dateModifiedBefore
        {
            get
            {
                return this.dateModifiedBeforeField;
            }
            set
            {
                this.dateModifiedBeforeField = value;
            }
        }

        /// <remarks/>
        public int pageNumber
        {
            get
            {
                return this.pageNumberField;
            }
            set
            {
                this.pageNumberField = value;
            }
        }

        /// <remarks/>
        public int pageSize
        {
            get
            {
                return this.pageSizeField;
            }
            set
            {
                this.pageSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string transactionType
        {
            get
            {
                return this.transactionTypeField;
            }
            set
            {
                this.transactionTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class SearchRecord
    {

        private string[] resultField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class SearchResult
    {

        private int pageCountField;

        private SearchRecord[] recordsField;

        private int totalCountField;

        /// <remarks/>
        public int pageCount
        {
            get
            {
                return this.pageCountField;
            }
            set
            {
                this.pageCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public SearchRecord[] records
        {
            get
            {
                return this.recordsField;
            }
            set
            {
                this.recordsField = value;
            }
        }

        /// <remarks/>
        public int totalCount
        {
            get
            {
                return this.totalCountField;
            }
            set
            {
                this.totalCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class SearchOrderBy
    {

        private bool descendingField;

        private string fieldField;

        /// <remarks/>
        public bool descending
        {
            get
            {
                return this.descendingField;
            }
            set
            {
                this.descendingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void searchCompletedEventHandler(object sender, searchCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class searchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal searchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public SearchResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((SearchResult)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentByConsignmentCodeCompletedEventHandler(object sender, findConsignmentByConsignmentCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentByConsignmentCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentByConsignmentCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentsByOrderReferenceCompletedEventHandler(object sender, findConsignmentsByOrderReferenceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentsByOrderReferenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentsByOrderReferenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentsByCarrierConsignmentCodeCompletedEventHandler(object sender, findConsignmentsByCarrierConsignmentCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentsByCarrierConsignmentCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentsByCarrierConsignmentCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentsByCartonIdCompletedEventHandler(object sender, findConsignmentsByCartonIdCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentsByCartonIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentsByCartonIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentsByParcelCodeCompletedEventHandler(object sender, findConsignmentsByParcelCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentsByParcelCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentsByParcelCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void searchForConsignmentsCompletedEventHandler(object sender, searchForConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class searchForConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal searchForConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ConsignmentSearchResult Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ConsignmentSearchResult)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ConsignmentServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(AuditRecord))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ConsignmentActionResult))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(UpdateField))]
    public partial class ConsignmentServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback updateOperationCompleted;

        private System.Threading.SendOrPostCallback validateConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback createConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback updateConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback deleteConsignmentOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelsAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createBulkLabelsAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelForCartonIdAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createDocumentationAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createDocumentationForCartonIdAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createBulkDocumentationAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createDocumentationPdfOperationCompleted;

        private System.Threading.SendOrPostCallback createPdfsForConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback markConsignmentsAsReadyToManifestOperationCompleted;

        private System.Threading.SendOrPostCallback markConsignmentsAsPrintedOperationCompleted;

        private System.Threading.SendOrPostCallback appendParcelsToConsignmentOperationCompleted;

        private System.Threading.SendOrPostCallback packProductsToParcelOperationCompleted;

        private System.Threading.SendOrPostCallback unpackProductsFromParcelOperationCompleted;

        private System.Threading.SendOrPostCallback addInnerToOuterOperationCompleted;

        private System.Threading.SendOrPostCallback removeInnerFromOuterOperationCompleted;

        private System.Threading.SendOrPostCallback addConsignmentsToGroupOperationCompleted;

        private System.Threading.SendOrPostCallback removeConsignmentsFromGroupOperationCompleted;

        private System.Threading.SendOrPostCallback createNextLabelsAsPdfOperationCompleted;

        private System.Threading.SendOrPostCallback voidConsignmentsOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentAuditRecordsOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelAsZPLOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelsAsZPLOperationCompleted;

        private System.Threading.SendOrPostCallback createLabelForCartonIdAsZPLOperationCompleted;

        private System.Threading.SendOrPostCallback createBulkLabelsAsZPLOperationCompleted;

        private System.Threading.SendOrPostCallback deleteParcelFromConsignmentOperationCompleted;

        private System.Threading.SendOrPostCallback deleteParcelFromConsignmentWithCartonIdOperationCompleted;

        private System.Threading.SendOrPostCallback calculateTaxAndDutyOperationCompleted;

        /// <remarks/>
        public ConsignmentServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/ConsignmentService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event updateCompletedEventHandler updateCompleted;

        /// <remarks/>
        public event validateConsignmentsCompletedEventHandler validateConsignmentsCompleted;

        /// <remarks/>
        public event createConsignmentsCompletedEventHandler createConsignmentsCompleted;

        /// <remarks/>
        public event updateConsignmentsCompletedEventHandler updateConsignmentsCompleted;

        /// <remarks/>
        public event deleteConsignmentCompletedEventHandler deleteConsignmentCompleted;

        /// <remarks/>
        public event createLabelsAsPdfCompletedEventHandler createLabelsAsPdfCompleted;

        /// <remarks/>
        public event createBulkLabelsAsPdfCompletedEventHandler createBulkLabelsAsPdfCompleted;

        /// <remarks/>
        public event createLabelAsPdfCompletedEventHandler createLabelAsPdfCompleted;

        /// <remarks/>
        public event createLabelForCartonIdAsPdfCompletedEventHandler createLabelForCartonIdAsPdfCompleted;

        /// <remarks/>
        public event createDocumentationAsPdfCompletedEventHandler createDocumentationAsPdfCompleted;

        /// <remarks/>
        public event createDocumentationForCartonIdAsPdfCompletedEventHandler createDocumentationForCartonIdAsPdfCompleted;

        /// <remarks/>
        public event createBulkDocumentationAsPdfCompletedEventHandler createBulkDocumentationAsPdfCompleted;

        /// <remarks/>
        public event createDocumentationPdfCompletedEventHandler createDocumentationPdfCompleted;

        /// <remarks/>
        public event createPdfsForConsignmentsCompletedEventHandler createPdfsForConsignmentsCompleted;

        /// <remarks/>
        public event markConsignmentsAsReadyToManifestCompletedEventHandler markConsignmentsAsReadyToManifestCompleted;

        /// <remarks/>
        public event markConsignmentsAsPrintedCompletedEventHandler markConsignmentsAsPrintedCompleted;

        /// <remarks/>
        public event appendParcelsToConsignmentCompletedEventHandler appendParcelsToConsignmentCompleted;

        /// <remarks/>
        public event packProductsToParcelCompletedEventHandler packProductsToParcelCompleted;

        /// <remarks/>
        public event unpackProductsFromParcelCompletedEventHandler unpackProductsFromParcelCompleted;

        /// <remarks/>
        public event addInnerToOuterCompletedEventHandler addInnerToOuterCompleted;

        /// <remarks/>
        public event removeInnerFromOuterCompletedEventHandler removeInnerFromOuterCompleted;

        /// <remarks/>
        public event addConsignmentsToGroupCompletedEventHandler addConsignmentsToGroupCompleted;

        /// <remarks/>
        public event removeConsignmentsFromGroupCompletedEventHandler removeConsignmentsFromGroupCompleted;

        /// <remarks/>
        public event createNextLabelsAsPdfCompletedEventHandler createNextLabelsAsPdfCompleted;

        /// <remarks/>
        public event voidConsignmentsCompletedEventHandler voidConsignmentsCompleted;

        /// <remarks/>
        public event findConsignmentAuditRecordsCompletedEventHandler findConsignmentAuditRecordsCompleted;

        /// <remarks/>
        public event createLabelAsZPLCompletedEventHandler createLabelAsZPLCompleted;

        /// <remarks/>
        public event createLabelsAsZPLCompletedEventHandler createLabelsAsZPLCompleted;

        /// <remarks/>
        public event createLabelForCartonIdAsZPLCompletedEventHandler createLabelForCartonIdAsZPLCompleted;

        /// <remarks/>
        public event createBulkLabelsAsZPLCompletedEventHandler createBulkLabelsAsZPLCompleted;

        /// <remarks/>
        public event deleteParcelFromConsignmentCompletedEventHandler deleteParcelFromConsignmentCompleted;

        /// <remarks/>
        public event deleteParcelFromConsignmentWithCartonIdCompletedEventHandler deleteParcelFromConsignmentWithCartonIdCompleted;

        /// <remarks/>
        public event calculateTaxAndDutyCompletedEventHandler calculateTaxAndDutyCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("updateReturn")]
        public Consignment update(string consignmentCode, UpdateField[] updateFields)
        {
            object[] results = this.Invoke("update", new object[] {
                    consignmentCode,
                    updateFields});
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginupdate(string consignmentCode, UpdateField[] updateFields, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("update", new object[] {
                    consignmentCode,
                    updateFields}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment Endupdate(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment)(results[0]));
        }

        /// <remarks/>
        public void updateAsync(string consignmentCode, UpdateField[] updateFields)
        {
            this.updateAsync(consignmentCode, updateFields, null);
        }

        /// <remarks/>
        public void updateAsync(string consignmentCode, UpdateField[] updateFields, object userState)
        {
            if ((this.updateOperationCompleted == null))
            {
                this.updateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateOperationCompleted);
            }
            this.InvokeAsync("update", new object[] {
                    consignmentCode,
                    updateFields}, this.updateOperationCompleted, userState);
        }

        private void OnupdateOperationCompleted(object arg)
        {
            if ((this.updateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateCompleted(this, new updateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("validateConsignmentsReturn")]
        public bool validateConsignments(Consignment[] consignments)
        {
            object[] results = this.Invoke("validateConsignments", new object[] {
                    consignments});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginvalidateConsignments(Consignment[] consignments, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("validateConsignments", new object[] {
                    consignments}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndvalidateConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void validateConsignmentsAsync(Consignment[] consignments)
        {
            this.validateConsignmentsAsync(consignments, null);
        }

        /// <remarks/>
        public void validateConsignmentsAsync(Consignment[] consignments, object userState)
        {
            if ((this.validateConsignmentsOperationCompleted == null))
            {
                this.validateConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnvalidateConsignmentsOperationCompleted);
            }
            this.InvokeAsync("validateConsignments", new object[] {
                    consignments}, this.validateConsignmentsOperationCompleted, userState);
        }

        private void OnvalidateConsignmentsOperationCompleted(object arg)
        {
            if ((this.validateConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.validateConsignmentsCompleted(this, new validateConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createConsignmentsReturn")]
        public Consignment[] createConsignments(Consignment[] consignments)
        {
            object[] results = this.Invoke("createConsignments", new object[] {
                    consignments});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateConsignments(Consignment[] consignments, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createConsignments", new object[] {
                    consignments}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndcreateConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void createConsignmentsAsync(Consignment[] consignments)
        {
            this.createConsignmentsAsync(consignments, null);
        }

        /// <remarks/>
        public void createConsignmentsAsync(Consignment[] consignments, object userState)
        {
            if ((this.createConsignmentsOperationCompleted == null))
            {
                this.createConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateConsignmentsOperationCompleted);
            }
            this.InvokeAsync("createConsignments", new object[] {
                    consignments}, this.createConsignmentsOperationCompleted, userState);
        }

        private void OncreateConsignmentsOperationCompleted(object arg)
        {
            if ((this.createConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createConsignmentsCompleted(this, new createConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("updateConsignmentsReturn")]
        public Consignment[] updateConsignments(Consignment[] consignments)
        {
            object[] results = this.Invoke("updateConsignments", new object[] {
                    consignments});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginupdateConsignments(Consignment[] consignments, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("updateConsignments", new object[] {
                    consignments}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndupdateConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void updateConsignmentsAsync(Consignment[] consignments)
        {
            this.updateConsignmentsAsync(consignments, null);
        }

        /// <remarks/>
        public void updateConsignmentsAsync(Consignment[] consignments, object userState)
        {
            if ((this.updateConsignmentsOperationCompleted == null))
            {
                this.updateConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateConsignmentsOperationCompleted);
            }
            this.InvokeAsync("updateConsignments", new object[] {
                    consignments}, this.updateConsignmentsOperationCompleted, userState);
        }

        private void OnupdateConsignmentsOperationCompleted(object arg)
        {
            if ((this.updateConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateConsignmentsCompleted(this, new updateConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("deleteConsignmentReturn")]
        public bool deleteConsignment(string consignmentCode)
        {
            object[] results = this.Invoke("deleteConsignment", new object[] {
                    consignmentCode});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindeleteConsignment(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("deleteConsignment", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public bool EnddeleteConsignment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void deleteConsignmentAsync(string consignmentCode)
        {
            this.deleteConsignmentAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void deleteConsignmentAsync(string consignmentCode, object userState)
        {
            if ((this.deleteConsignmentOperationCompleted == null))
            {
                this.deleteConsignmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteConsignmentOperationCompleted);
            }
            this.InvokeAsync("deleteConsignment", new object[] {
                    consignmentCode}, this.deleteConsignmentOperationCompleted, userState);
        }

        private void OndeleteConsignmentOperationCompleted(object arg)
        {
            if ((this.deleteConsignmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteConsignmentCompleted(this, new deleteConsignmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelsAsPdfReturn")]
        public string createLabelsAsPdf(string consignmentCode)
        {
            object[] results = this.Invoke("createLabelsAsPdf", new object[] {
                    consignmentCode});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelsAsPdf(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelsAsPdf", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateLabelsAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createLabelsAsPdfAsync(string consignmentCode)
        {
            this.createLabelsAsPdfAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void createLabelsAsPdfAsync(string consignmentCode, object userState)
        {
            if ((this.createLabelsAsPdfOperationCompleted == null))
            {
                this.createLabelsAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelsAsPdfOperationCompleted);
            }
            this.InvokeAsync("createLabelsAsPdf", new object[] {
                    consignmentCode}, this.createLabelsAsPdfOperationCompleted, userState);
        }

        private void OncreateLabelsAsPdfOperationCompleted(object arg)
        {
            if ((this.createLabelsAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelsAsPdfCompleted(this, new createLabelsAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createBulkLabelsAsPdfReturn")]
        public string createBulkLabelsAsPdf(string[] consignmentCodes)
        {
            object[] results = this.Invoke("createBulkLabelsAsPdf", new object[] {
                    consignmentCodes});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateBulkLabelsAsPdf(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createBulkLabelsAsPdf", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateBulkLabelsAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createBulkLabelsAsPdfAsync(string[] consignmentCodes)
        {
            this.createBulkLabelsAsPdfAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void createBulkLabelsAsPdfAsync(string[] consignmentCodes, object userState)
        {
            if ((this.createBulkLabelsAsPdfOperationCompleted == null))
            {
                this.createBulkLabelsAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateBulkLabelsAsPdfOperationCompleted);
            }
            this.InvokeAsync("createBulkLabelsAsPdf", new object[] {
                    consignmentCodes}, this.createBulkLabelsAsPdfOperationCompleted, userState);
        }

        private void OncreateBulkLabelsAsPdfOperationCompleted(object arg)
        {
            if ((this.createBulkLabelsAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createBulkLabelsAsPdfCompleted(this, new createBulkLabelsAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelAsPdfReturn")]
        public string createLabelAsPdf(string consignmentCode, int parcelNumber)
        {
            object[] results = this.Invoke("createLabelAsPdf", new object[] {
                    consignmentCode,
                    parcelNumber});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelAsPdf(string consignmentCode, int parcelNumber, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelAsPdf", new object[] {
                    consignmentCode,
                    parcelNumber}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateLabelAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createLabelAsPdfAsync(string consignmentCode, int parcelNumber)
        {
            this.createLabelAsPdfAsync(consignmentCode, parcelNumber, null);
        }

        /// <remarks/>
        public void createLabelAsPdfAsync(string consignmentCode, int parcelNumber, object userState)
        {
            if ((this.createLabelAsPdfOperationCompleted == null))
            {
                this.createLabelAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelAsPdfOperationCompleted);
            }
            this.InvokeAsync("createLabelAsPdf", new object[] {
                    consignmentCode,
                    parcelNumber}, this.createLabelAsPdfOperationCompleted, userState);
        }

        private void OncreateLabelAsPdfOperationCompleted(object arg)
        {
            if ((this.createLabelAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelAsPdfCompleted(this, new createLabelAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelForCartonIdAsPdfReturn")]
        public string createLabelForCartonIdAsPdf(string consignmentCode, string cartonId)
        {
            object[] results = this.Invoke("createLabelForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelForCartonIdAsPdf(string consignmentCode, string cartonId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateLabelForCartonIdAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createLabelForCartonIdAsPdfAsync(string consignmentCode, string cartonId)
        {
            this.createLabelForCartonIdAsPdfAsync(consignmentCode, cartonId, null);
        }

        /// <remarks/>
        public void createLabelForCartonIdAsPdfAsync(string consignmentCode, string cartonId, object userState)
        {
            if ((this.createLabelForCartonIdAsPdfOperationCompleted == null))
            {
                this.createLabelForCartonIdAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelForCartonIdAsPdfOperationCompleted);
            }
            this.InvokeAsync("createLabelForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId}, this.createLabelForCartonIdAsPdfOperationCompleted, userState);
        }

        private void OncreateLabelForCartonIdAsPdfOperationCompleted(object arg)
        {
            if ((this.createLabelForCartonIdAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelForCartonIdAsPdfCompleted(this, new createLabelForCartonIdAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createDocumentationAsPdfReturn")]
        public string createDocumentationAsPdf(string consignmentCode)
        {
            object[] results = this.Invoke("createDocumentationAsPdf", new object[] {
                    consignmentCode});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateDocumentationAsPdf(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createDocumentationAsPdf", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateDocumentationAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createDocumentationAsPdfAsync(string consignmentCode)
        {
            this.createDocumentationAsPdfAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void createDocumentationAsPdfAsync(string consignmentCode, object userState)
        {
            if ((this.createDocumentationAsPdfOperationCompleted == null))
            {
                this.createDocumentationAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateDocumentationAsPdfOperationCompleted);
            }
            this.InvokeAsync("createDocumentationAsPdf", new object[] {
                    consignmentCode}, this.createDocumentationAsPdfOperationCompleted, userState);
        }

        private void OncreateDocumentationAsPdfOperationCompleted(object arg)
        {
            if ((this.createDocumentationAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createDocumentationAsPdfCompleted(this, new createDocumentationAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createDocumentationForCartonIdAsPdfReturn")]
        public string createDocumentationForCartonIdAsPdf(string consignmentCode, string cartonId)
        {
            object[] results = this.Invoke("createDocumentationForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateDocumentationForCartonIdAsPdf(string consignmentCode, string cartonId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createDocumentationForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateDocumentationForCartonIdAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createDocumentationForCartonIdAsPdfAsync(string consignmentCode, string cartonId)
        {
            this.createDocumentationForCartonIdAsPdfAsync(consignmentCode, cartonId, null);
        }

        /// <remarks/>
        public void createDocumentationForCartonIdAsPdfAsync(string consignmentCode, string cartonId, object userState)
        {
            if ((this.createDocumentationForCartonIdAsPdfOperationCompleted == null))
            {
                this.createDocumentationForCartonIdAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateDocumentationForCartonIdAsPdfOperationCompleted);
            }
            this.InvokeAsync("createDocumentationForCartonIdAsPdf", new object[] {
                    consignmentCode,
                    cartonId}, this.createDocumentationForCartonIdAsPdfOperationCompleted, userState);
        }

        private void OncreateDocumentationForCartonIdAsPdfOperationCompleted(object arg)
        {
            if ((this.createDocumentationForCartonIdAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createDocumentationForCartonIdAsPdfCompleted(this, new createDocumentationForCartonIdAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createBulkDocumentationAsPdfReturn")]
        public string createBulkDocumentationAsPdf(string[] consignmentCodes)
        {
            object[] results = this.Invoke("createBulkDocumentationAsPdf", new object[] {
                    consignmentCodes});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateBulkDocumentationAsPdf(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createBulkDocumentationAsPdf", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateBulkDocumentationAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createBulkDocumentationAsPdfAsync(string[] consignmentCodes)
        {
            this.createBulkDocumentationAsPdfAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void createBulkDocumentationAsPdfAsync(string[] consignmentCodes, object userState)
        {
            if ((this.createBulkDocumentationAsPdfOperationCompleted == null))
            {
                this.createBulkDocumentationAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateBulkDocumentationAsPdfOperationCompleted);
            }
            this.InvokeAsync("createBulkDocumentationAsPdf", new object[] {
                    consignmentCodes}, this.createBulkDocumentationAsPdfOperationCompleted, userState);
        }

        private void OncreateBulkDocumentationAsPdfOperationCompleted(object arg)
        {
            if ((this.createBulkDocumentationAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createBulkDocumentationAsPdfCompleted(this, new createBulkDocumentationAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createDocumentationPdfReturn")]
        public string createDocumentationPdf(string[] consignmentCodes, int parcelNumber, string printerType, string documentType)
        {
            object[] results = this.Invoke("createDocumentationPdf", new object[] {
                    consignmentCodes,
                    parcelNumber,
                    printerType,
                    documentType});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateDocumentationPdf(string[] consignmentCodes, int parcelNumber, string printerType, string documentType, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createDocumentationPdf", new object[] {
                    consignmentCodes,
                    parcelNumber,
                    printerType,
                    documentType}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateDocumentationPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createDocumentationPdfAsync(string[] consignmentCodes, int parcelNumber, string printerType, string documentType)
        {
            this.createDocumentationPdfAsync(consignmentCodes, parcelNumber, printerType, documentType, null);
        }

        /// <remarks/>
        public void createDocumentationPdfAsync(string[] consignmentCodes, int parcelNumber, string printerType, string documentType, object userState)
        {
            if ((this.createDocumentationPdfOperationCompleted == null))
            {
                this.createDocumentationPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateDocumentationPdfOperationCompleted);
            }
            this.InvokeAsync("createDocumentationPdf", new object[] {
                    consignmentCodes,
                    parcelNumber,
                    printerType,
                    documentType}, this.createDocumentationPdfOperationCompleted, userState);
        }

        private void OncreateDocumentationPdfOperationCompleted(object arg)
        {
            if ((this.createDocumentationPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createDocumentationPdfCompleted(this, new createDocumentationPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createPdfsForConsignmentsReturn")]
        public Labels createPdfsForConsignments(string[] consignmentCodes)
        {
            object[] results = this.Invoke("createPdfsForConsignments", new object[] {
                    consignmentCodes});
            return ((Labels)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreatePdfsForConsignments(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createPdfsForConsignments", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public Labels EndcreatePdfsForConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Labels)(results[0]));
        }

        /// <remarks/>
        public void createPdfsForConsignmentsAsync(string[] consignmentCodes)
        {
            this.createPdfsForConsignmentsAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void createPdfsForConsignmentsAsync(string[] consignmentCodes, object userState)
        {
            if ((this.createPdfsForConsignmentsOperationCompleted == null))
            {
                this.createPdfsForConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreatePdfsForConsignmentsOperationCompleted);
            }
            this.InvokeAsync("createPdfsForConsignments", new object[] {
                    consignmentCodes}, this.createPdfsForConsignmentsOperationCompleted, userState);
        }

        private void OncreatePdfsForConsignmentsOperationCompleted(object arg)
        {
            if ((this.createPdfsForConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createPdfsForConsignmentsCompleted(this, new createPdfsForConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("markConsignmentsAsReadyToManifestReturn")]
        public bool markConsignmentsAsReadyToManifest(string[] consignmentCodes)
        {
            object[] results = this.Invoke("markConsignmentsAsReadyToManifest", new object[] {
                    consignmentCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginmarkConsignmentsAsReadyToManifest(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("markConsignmentsAsReadyToManifest", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndmarkConsignmentsAsReadyToManifest(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void markConsignmentsAsReadyToManifestAsync(string[] consignmentCodes)
        {
            this.markConsignmentsAsReadyToManifestAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void markConsignmentsAsReadyToManifestAsync(string[] consignmentCodes, object userState)
        {
            if ((this.markConsignmentsAsReadyToManifestOperationCompleted == null))
            {
                this.markConsignmentsAsReadyToManifestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnmarkConsignmentsAsReadyToManifestOperationCompleted);
            }
            this.InvokeAsync("markConsignmentsAsReadyToManifest", new object[] {
                    consignmentCodes}, this.markConsignmentsAsReadyToManifestOperationCompleted, userState);
        }

        private void OnmarkConsignmentsAsReadyToManifestOperationCompleted(object arg)
        {
            if ((this.markConsignmentsAsReadyToManifestCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.markConsignmentsAsReadyToManifestCompleted(this, new markConsignmentsAsReadyToManifestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("markConsignmentsAsPrintedReturn")]
        public bool markConsignmentsAsPrinted(string[] consignmentCodes)
        {
            object[] results = this.Invoke("markConsignmentsAsPrinted", new object[] {
                    consignmentCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginmarkConsignmentsAsPrinted(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("markConsignmentsAsPrinted", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndmarkConsignmentsAsPrinted(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void markConsignmentsAsPrintedAsync(string[] consignmentCodes)
        {
            this.markConsignmentsAsPrintedAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void markConsignmentsAsPrintedAsync(string[] consignmentCodes, object userState)
        {
            if ((this.markConsignmentsAsPrintedOperationCompleted == null))
            {
                this.markConsignmentsAsPrintedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnmarkConsignmentsAsPrintedOperationCompleted);
            }
            this.InvokeAsync("markConsignmentsAsPrinted", new object[] {
                    consignmentCodes}, this.markConsignmentsAsPrintedOperationCompleted, userState);
        }

        private void OnmarkConsignmentsAsPrintedOperationCompleted(object arg)
        {
            if ((this.markConsignmentsAsPrintedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.markConsignmentsAsPrintedCompleted(this, new markConsignmentsAsPrintedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("appendParcelsToConsignmentReturn")]
        public Parcel[] appendParcelsToConsignment(string consignmentCode, Parcel[] parcels, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("appendParcelsToConsignment", new object[] {
                    consignmentCode,
                    parcels,
                    recalculateTaxAndDuty});
            return ((Parcel[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginappendParcelsToConsignment(string consignmentCode, Parcel[] parcels, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("appendParcelsToConsignment", new object[] {
                    consignmentCode,
                    parcels,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public Parcel[] EndappendParcelsToConsignment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Parcel[])(results[0]));
        }

        /// <remarks/>
        public void appendParcelsToConsignmentAsync(string consignmentCode, Parcel[] parcels, bool recalculateTaxAndDuty)
        {
            this.appendParcelsToConsignmentAsync(consignmentCode, parcels, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void appendParcelsToConsignmentAsync(string consignmentCode, Parcel[] parcels, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.appendParcelsToConsignmentOperationCompleted == null))
            {
                this.appendParcelsToConsignmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnappendParcelsToConsignmentOperationCompleted);
            }
            this.InvokeAsync("appendParcelsToConsignment", new object[] {
                    consignmentCode,
                    parcels,
                    recalculateTaxAndDuty}, this.appendParcelsToConsignmentOperationCompleted, userState);
        }

        private void OnappendParcelsToConsignmentOperationCompleted(object arg)
        {
            if ((this.appendParcelsToConsignmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.appendParcelsToConsignmentCompleted(this, new appendParcelsToConsignmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("packProductsToParcelReturn")]
        public bool packProductsToParcel(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("packProductsToParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginpackProductsToParcel(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("packProductsToParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndpackProductsToParcel(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void packProductsToParcelAsync(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty)
        {
            this.packProductsToParcelAsync(consignmentCode, parcelNumber, products, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void packProductsToParcelAsync(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.packProductsToParcelOperationCompleted == null))
            {
                this.packProductsToParcelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnpackProductsToParcelOperationCompleted);
            }
            this.InvokeAsync("packProductsToParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty}, this.packProductsToParcelOperationCompleted, userState);
        }

        private void OnpackProductsToParcelOperationCompleted(object arg)
        {
            if ((this.packProductsToParcelCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.packProductsToParcelCompleted(this, new packProductsToParcelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("unpackProductsFromParcelReturn")]
        public bool unpackProductsFromParcel(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("unpackProductsFromParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginunpackProductsFromParcel(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("unpackProductsFromParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndunpackProductsFromParcel(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void unpackProductsFromParcelAsync(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty)
        {
            this.unpackProductsFromParcelAsync(consignmentCode, parcelNumber, products, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void unpackProductsFromParcelAsync(string consignmentCode, int parcelNumber, Product[] products, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.unpackProductsFromParcelOperationCompleted == null))
            {
                this.unpackProductsFromParcelOperationCompleted = new System.Threading.SendOrPostCallback(this.OnunpackProductsFromParcelOperationCompleted);
            }
            this.InvokeAsync("unpackProductsFromParcel", new object[] {
                    consignmentCode,
                    parcelNumber,
                    products,
                    recalculateTaxAndDuty}, this.unpackProductsFromParcelOperationCompleted, userState);
        }

        private void OnunpackProductsFromParcelOperationCompleted(object arg)
        {
            if ((this.unpackProductsFromParcelCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.unpackProductsFromParcelCompleted(this, new unpackProductsFromParcelCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("addInnerToOuterReturn")]
        public bool addInnerToOuter(string innerConsignmentCode, int innerParcelNumber, string outerConsignmentCode, int outerParcelNumber)
        {
            object[] results = this.Invoke("addInnerToOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber,
                    outerConsignmentCode,
                    outerParcelNumber});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginaddInnerToOuter(string innerConsignmentCode, int innerParcelNumber, string outerConsignmentCode, int outerParcelNumber, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("addInnerToOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber,
                    outerConsignmentCode,
                    outerParcelNumber}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndaddInnerToOuter(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void addInnerToOuterAsync(string innerConsignmentCode, int innerParcelNumber, string outerConsignmentCode, int outerParcelNumber)
        {
            this.addInnerToOuterAsync(innerConsignmentCode, innerParcelNumber, outerConsignmentCode, outerParcelNumber, null);
        }

        /// <remarks/>
        public void addInnerToOuterAsync(string innerConsignmentCode, int innerParcelNumber, string outerConsignmentCode, int outerParcelNumber, object userState)
        {
            if ((this.addInnerToOuterOperationCompleted == null))
            {
                this.addInnerToOuterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnaddInnerToOuterOperationCompleted);
            }
            this.InvokeAsync("addInnerToOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber,
                    outerConsignmentCode,
                    outerParcelNumber}, this.addInnerToOuterOperationCompleted, userState);
        }

        private void OnaddInnerToOuterOperationCompleted(object arg)
        {
            if ((this.addInnerToOuterCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.addInnerToOuterCompleted(this, new addInnerToOuterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("removeInnerFromOuterReturn")]
        public bool removeInnerFromOuter(string innerConsignmentCode, int innerParcelNumber)
        {
            object[] results = this.Invoke("removeInnerFromOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginremoveInnerFromOuter(string innerConsignmentCode, int innerParcelNumber, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("removeInnerFromOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndremoveInnerFromOuter(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void removeInnerFromOuterAsync(string innerConsignmentCode, int innerParcelNumber)
        {
            this.removeInnerFromOuterAsync(innerConsignmentCode, innerParcelNumber, null);
        }

        /// <remarks/>
        public void removeInnerFromOuterAsync(string innerConsignmentCode, int innerParcelNumber, object userState)
        {
            if ((this.removeInnerFromOuterOperationCompleted == null))
            {
                this.removeInnerFromOuterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnremoveInnerFromOuterOperationCompleted);
            }
            this.InvokeAsync("removeInnerFromOuter", new object[] {
                    innerConsignmentCode,
                    innerParcelNumber}, this.removeInnerFromOuterOperationCompleted, userState);
        }

        private void OnremoveInnerFromOuterOperationCompleted(object arg)
        {
            if ((this.removeInnerFromOuterCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.removeInnerFromOuterCompleted(this, new removeInnerFromOuterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("addConsignmentsToGroupReturn")]
        public bool addConsignmentsToGroup(string[] consignmentCodes, string manifestGroupCode)
        {
            object[] results = this.Invoke("addConsignmentsToGroup", new object[] {
                    consignmentCodes,
                    manifestGroupCode});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginaddConsignmentsToGroup(string[] consignmentCodes, string manifestGroupCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("addConsignmentsToGroup", new object[] {
                    consignmentCodes,
                    manifestGroupCode}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndaddConsignmentsToGroup(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void addConsignmentsToGroupAsync(string[] consignmentCodes, string manifestGroupCode)
        {
            this.addConsignmentsToGroupAsync(consignmentCodes, manifestGroupCode, null);
        }

        /// <remarks/>
        public void addConsignmentsToGroupAsync(string[] consignmentCodes, string manifestGroupCode, object userState)
        {
            if ((this.addConsignmentsToGroupOperationCompleted == null))
            {
                this.addConsignmentsToGroupOperationCompleted = new System.Threading.SendOrPostCallback(this.OnaddConsignmentsToGroupOperationCompleted);
            }
            this.InvokeAsync("addConsignmentsToGroup", new object[] {
                    consignmentCodes,
                    manifestGroupCode}, this.addConsignmentsToGroupOperationCompleted, userState);
        }

        private void OnaddConsignmentsToGroupOperationCompleted(object arg)
        {
            if ((this.addConsignmentsToGroupCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.addConsignmentsToGroupCompleted(this, new addConsignmentsToGroupCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("removeConsignmentsFromGroupReturn")]
        public bool removeConsignmentsFromGroup(string[] consignmentCodes)
        {
            object[] results = this.Invoke("removeConsignmentsFromGroup", new object[] {
                    consignmentCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginremoveConsignmentsFromGroup(string[] consignmentCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("removeConsignmentsFromGroup", new object[] {
                    consignmentCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndremoveConsignmentsFromGroup(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void removeConsignmentsFromGroupAsync(string[] consignmentCodes)
        {
            this.removeConsignmentsFromGroupAsync(consignmentCodes, null);
        }

        /// <remarks/>
        public void removeConsignmentsFromGroupAsync(string[] consignmentCodes, object userState)
        {
            if ((this.removeConsignmentsFromGroupOperationCompleted == null))
            {
                this.removeConsignmentsFromGroupOperationCompleted = new System.Threading.SendOrPostCallback(this.OnremoveConsignmentsFromGroupOperationCompleted);
            }
            this.InvokeAsync("removeConsignmentsFromGroup", new object[] {
                    consignmentCodes}, this.removeConsignmentsFromGroupOperationCompleted, userState);
        }

        private void OnremoveConsignmentsFromGroupOperationCompleted(object arg)
        {
            if ((this.removeConsignmentsFromGroupCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.removeConsignmentsFromGroupCompleted(this, new removeConsignmentsFromGroupCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createNextLabelsAsPdfReturn")]
        public string createNextLabelsAsPdf(string consignmentCode, int numberOfLabels)
        {
            object[] results = this.Invoke("createNextLabelsAsPdf", new object[] {
                    consignmentCode,
                    numberOfLabels});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateNextLabelsAsPdf(string consignmentCode, int numberOfLabels, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createNextLabelsAsPdf", new object[] {
                    consignmentCode,
                    numberOfLabels}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateNextLabelsAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createNextLabelsAsPdfAsync(string consignmentCode, int numberOfLabels)
        {
            this.createNextLabelsAsPdfAsync(consignmentCode, numberOfLabels, null);
        }

        /// <remarks/>
        public void createNextLabelsAsPdfAsync(string consignmentCode, int numberOfLabels, object userState)
        {
            if ((this.createNextLabelsAsPdfOperationCompleted == null))
            {
                this.createNextLabelsAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateNextLabelsAsPdfOperationCompleted);
            }
            this.InvokeAsync("createNextLabelsAsPdf", new object[] {
                    consignmentCode,
                    numberOfLabels}, this.createNextLabelsAsPdfOperationCompleted, userState);
        }

        private void OncreateNextLabelsAsPdfOperationCompleted(object arg)
        {
            if ((this.createNextLabelsAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createNextLabelsAsPdfCompleted(this, new createNextLabelsAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("voidConsignmentsReturn")]
        public ConsignmentActionResult[] voidConsignments(string[] consignmentCodes, string reasonCode, string reason)
        {
            object[] results = this.Invoke("voidConsignments", new object[] {
                    consignmentCodes,
                    reasonCode,
                    reason});
            return ((ConsignmentActionResult[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginvoidConsignments(string[] consignmentCodes, string reasonCode, string reason, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("voidConsignments", new object[] {
                    consignmentCodes,
                    reasonCode,
                    reason}, callback, asyncState);
        }

        /// <remarks/>
        public ConsignmentActionResult[] EndvoidConsignments(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ConsignmentActionResult[])(results[0]));
        }

        /// <remarks/>
        public void voidConsignmentsAsync(string[] consignmentCodes, string reasonCode, string reason)
        {
            this.voidConsignmentsAsync(consignmentCodes, reasonCode, reason, null);
        }

        /// <remarks/>
        public void voidConsignmentsAsync(string[] consignmentCodes, string reasonCode, string reason, object userState)
        {
            if ((this.voidConsignmentsOperationCompleted == null))
            {
                this.voidConsignmentsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnvoidConsignmentsOperationCompleted);
            }
            this.InvokeAsync("voidConsignments", new object[] {
                    consignmentCodes,
                    reasonCode,
                    reason}, this.voidConsignmentsOperationCompleted, userState);
        }

        private void OnvoidConsignmentsOperationCompleted(object arg)
        {
            if ((this.voidConsignmentsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.voidConsignmentsCompleted(this, new voidConsignmentsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentAuditRecordsReturn")]
        public AuditRecord[] findConsignmentAuditRecords(string consignmentCode)
        {
            object[] results = this.Invoke("findConsignmentAuditRecords", new object[] {
                    consignmentCode});
            return ((AuditRecord[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentAuditRecords(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentAuditRecords", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public AuditRecord[] EndfindConsignmentAuditRecords(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((AuditRecord[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentAuditRecordsAsync(string consignmentCode)
        {
            this.findConsignmentAuditRecordsAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void findConsignmentAuditRecordsAsync(string consignmentCode, object userState)
        {
            if ((this.findConsignmentAuditRecordsOperationCompleted == null))
            {
                this.findConsignmentAuditRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentAuditRecordsOperationCompleted);
            }
            this.InvokeAsync("findConsignmentAuditRecords", new object[] {
                    consignmentCode}, this.findConsignmentAuditRecordsOperationCompleted, userState);
        }

        private void OnfindConsignmentAuditRecordsOperationCompleted(object arg)
        {
            if ((this.findConsignmentAuditRecordsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentAuditRecordsCompleted(this, new findConsignmentAuditRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelAsZPLReturn")]
        public string createLabelAsZPL(string consignmentCode, int parcelNumber, double dpi)
        {
            object[] results = this.Invoke("createLabelAsZPL", new object[] {
                    consignmentCode,
                    parcelNumber,
                    dpi});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelAsZPL(string consignmentCode, int parcelNumber, double dpi, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelAsZPL", new object[] {
                    consignmentCode,
                    parcelNumber,
                    dpi}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateLabelAsZPL(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createLabelAsZPLAsync(string consignmentCode, int parcelNumber, double dpi)
        {
            this.createLabelAsZPLAsync(consignmentCode, parcelNumber, dpi, null);
        }

        /// <remarks/>
        public void createLabelAsZPLAsync(string consignmentCode, int parcelNumber, double dpi, object userState)
        {
            if ((this.createLabelAsZPLOperationCompleted == null))
            {
                this.createLabelAsZPLOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelAsZPLOperationCompleted);
            }
            this.InvokeAsync("createLabelAsZPL", new object[] {
                    consignmentCode,
                    parcelNumber,
                    dpi}, this.createLabelAsZPLOperationCompleted, userState);
        }

        private void OncreateLabelAsZPLOperationCompleted(object arg)
        {
            if ((this.createLabelAsZPLCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelAsZPLCompleted(this, new createLabelAsZPLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelsAsZPLReturn")]
        public string[] createLabelsAsZPL(string consignmentCode, double dpi)
        {
            object[] results = this.Invoke("createLabelsAsZPL", new object[] {
                    consignmentCode,
                    dpi});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelsAsZPL(string consignmentCode, double dpi, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelsAsZPL", new object[] {
                    consignmentCode,
                    dpi}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndcreateLabelsAsZPL(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void createLabelsAsZPLAsync(string consignmentCode, double dpi)
        {
            this.createLabelsAsZPLAsync(consignmentCode, dpi, null);
        }

        /// <remarks/>
        public void createLabelsAsZPLAsync(string consignmentCode, double dpi, object userState)
        {
            if ((this.createLabelsAsZPLOperationCompleted == null))
            {
                this.createLabelsAsZPLOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelsAsZPLOperationCompleted);
            }
            this.InvokeAsync("createLabelsAsZPL", new object[] {
                    consignmentCode,
                    dpi}, this.createLabelsAsZPLOperationCompleted, userState);
        }

        private void OncreateLabelsAsZPLOperationCompleted(object arg)
        {
            if ((this.createLabelsAsZPLCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelsAsZPLCompleted(this, new createLabelsAsZPLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createLabelForCartonIdAsZPLReturn")]
        public string createLabelForCartonIdAsZPL(string consignmentCode, string cartonId, double dpi)
        {
            object[] results = this.Invoke("createLabelForCartonIdAsZPL", new object[] {
                    consignmentCode,
                    cartonId,
                    dpi});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateLabelForCartonIdAsZPL(string consignmentCode, string cartonId, double dpi, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createLabelForCartonIdAsZPL", new object[] {
                    consignmentCode,
                    cartonId,
                    dpi}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateLabelForCartonIdAsZPL(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createLabelForCartonIdAsZPLAsync(string consignmentCode, string cartonId, double dpi)
        {
            this.createLabelForCartonIdAsZPLAsync(consignmentCode, cartonId, dpi, null);
        }

        /// <remarks/>
        public void createLabelForCartonIdAsZPLAsync(string consignmentCode, string cartonId, double dpi, object userState)
        {
            if ((this.createLabelForCartonIdAsZPLOperationCompleted == null))
            {
                this.createLabelForCartonIdAsZPLOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateLabelForCartonIdAsZPLOperationCompleted);
            }
            this.InvokeAsync("createLabelForCartonIdAsZPL", new object[] {
                    consignmentCode,
                    cartonId,
                    dpi}, this.createLabelForCartonIdAsZPLOperationCompleted, userState);
        }

        private void OncreateLabelForCartonIdAsZPLOperationCompleted(object arg)
        {
            if ((this.createLabelForCartonIdAsZPLCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createLabelForCartonIdAsZPLCompleted(this, new createLabelForCartonIdAsZPLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createBulkLabelsAsZPLReturn")]
        public string[] createBulkLabelsAsZPL(string[] consignmentCodes, double dpi)
        {
            object[] results = this.Invoke("createBulkLabelsAsZPL", new object[] {
                    consignmentCodes,
                    dpi});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateBulkLabelsAsZPL(string[] consignmentCodes, double dpi, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createBulkLabelsAsZPL", new object[] {
                    consignmentCodes,
                    dpi}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndcreateBulkLabelsAsZPL(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void createBulkLabelsAsZPLAsync(string[] consignmentCodes, double dpi)
        {
            this.createBulkLabelsAsZPLAsync(consignmentCodes, dpi, null);
        }

        /// <remarks/>
        public void createBulkLabelsAsZPLAsync(string[] consignmentCodes, double dpi, object userState)
        {
            if ((this.createBulkLabelsAsZPLOperationCompleted == null))
            {
                this.createBulkLabelsAsZPLOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateBulkLabelsAsZPLOperationCompleted);
            }
            this.InvokeAsync("createBulkLabelsAsZPL", new object[] {
                    consignmentCodes,
                    dpi}, this.createBulkLabelsAsZPLOperationCompleted, userState);
        }

        private void OncreateBulkLabelsAsZPLOperationCompleted(object arg)
        {
            if ((this.createBulkLabelsAsZPLCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createBulkLabelsAsZPLCompleted(this, new createBulkLabelsAsZPLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("deleteParcelFromConsignmentReturn")]
        public bool deleteParcelFromConsignment(string consignmentCode, int parcelNo, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("deleteParcelFromConsignment", new object[] {
                    consignmentCode,
                    parcelNo,
                    recalculateTaxAndDuty});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindeleteParcelFromConsignment(string consignmentCode, int parcelNo, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("deleteParcelFromConsignment", new object[] {
                    consignmentCode,
                    parcelNo,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public bool EnddeleteParcelFromConsignment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void deleteParcelFromConsignmentAsync(string consignmentCode, int parcelNo, bool recalculateTaxAndDuty)
        {
            this.deleteParcelFromConsignmentAsync(consignmentCode, parcelNo, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void deleteParcelFromConsignmentAsync(string consignmentCode, int parcelNo, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.deleteParcelFromConsignmentOperationCompleted == null))
            {
                this.deleteParcelFromConsignmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteParcelFromConsignmentOperationCompleted);
            }
            this.InvokeAsync("deleteParcelFromConsignment", new object[] {
                    consignmentCode,
                    parcelNo,
                    recalculateTaxAndDuty}, this.deleteParcelFromConsignmentOperationCompleted, userState);
        }

        private void OndeleteParcelFromConsignmentOperationCompleted(object arg)
        {
            if ((this.deleteParcelFromConsignmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteParcelFromConsignmentCompleted(this, new deleteParcelFromConsignmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("deleteParcelFromConsignmentWithCartonIdReturn")]
        public bool deleteParcelFromConsignmentWithCartonId(string consignmentCode, string cartonId, bool recalculateTaxAndDuty)
        {
            object[] results = this.Invoke("deleteParcelFromConsignmentWithCartonId", new object[] {
                    consignmentCode,
                    cartonId,
                    recalculateTaxAndDuty});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindeleteParcelFromConsignmentWithCartonId(string consignmentCode, string cartonId, bool recalculateTaxAndDuty, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("deleteParcelFromConsignmentWithCartonId", new object[] {
                    consignmentCode,
                    cartonId,
                    recalculateTaxAndDuty}, callback, asyncState);
        }

        /// <remarks/>
        public bool EnddeleteParcelFromConsignmentWithCartonId(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void deleteParcelFromConsignmentWithCartonIdAsync(string consignmentCode, string cartonId, bool recalculateTaxAndDuty)
        {
            this.deleteParcelFromConsignmentWithCartonIdAsync(consignmentCode, cartonId, recalculateTaxAndDuty, null);
        }

        /// <remarks/>
        public void deleteParcelFromConsignmentWithCartonIdAsync(string consignmentCode, string cartonId, bool recalculateTaxAndDuty, object userState)
        {
            if ((this.deleteParcelFromConsignmentWithCartonIdOperationCompleted == null))
            {
                this.deleteParcelFromConsignmentWithCartonIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OndeleteParcelFromConsignmentWithCartonIdOperationCompleted);
            }
            this.InvokeAsync("deleteParcelFromConsignmentWithCartonId", new object[] {
                    consignmentCode,
                    cartonId,
                    recalculateTaxAndDuty}, this.deleteParcelFromConsignmentWithCartonIdOperationCompleted, userState);
        }

        private void OndeleteParcelFromConsignmentWithCartonIdOperationCompleted(object arg)
        {
            if ((this.deleteParcelFromConsignmentWithCartonIdCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.deleteParcelFromConsignmentWithCartonIdCompleted(this, new deleteParcelFromConsignmentWithCartonIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("calculateTaxAndDutyReturn")]
        public TaxAndDuty calculateTaxAndDuty(string consignmentCode)
        {
            object[] results = this.Invoke("calculateTaxAndDuty", new object[] {
                    consignmentCode});
            return ((TaxAndDuty)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincalculateTaxAndDuty(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("calculateTaxAndDuty", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public TaxAndDuty EndcalculateTaxAndDuty(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxAndDuty)(results[0]));
        }

        /// <remarks/>
        public void calculateTaxAndDutyAsync(string consignmentCode)
        {
            this.calculateTaxAndDutyAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void calculateTaxAndDutyAsync(string consignmentCode, object userState)
        {
            if ((this.calculateTaxAndDutyOperationCompleted == null))
            {
                this.calculateTaxAndDutyOperationCompleted = new System.Threading.SendOrPostCallback(this.OncalculateTaxAndDutyOperationCompleted);
            }
            this.InvokeAsync("calculateTaxAndDuty", new object[] {
                    consignmentCode}, this.calculateTaxAndDutyOperationCompleted, userState);
        }

        private void OncalculateTaxAndDutyOperationCompleted(object arg)
        {
            if ((this.calculateTaxAndDutyCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.calculateTaxAndDutyCompleted(this, new calculateTaxAndDutyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class UpdateField
    {

        private string fieldField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class TaxAndDuty
    {

        private Property[] propertiesField;

        private double taxAndDutyField;

        private string taxAndDutyCurrencyCodeField;

        private double taxAndDutyCurrencyRateField;

        private string taxAndDutyStatusTextField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public Property[] properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        /// <remarks/>
        public double taxAndDuty
        {
            get
            {
                return this.taxAndDutyField;
            }
            set
            {
                this.taxAndDutyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxAndDutyCurrencyCode
        {
            get
            {
                return this.taxAndDutyCurrencyCodeField;
            }
            set
            {
                this.taxAndDutyCurrencyCodeField = value;
            }
        }

        /// <remarks/>
        public double taxAndDutyCurrencyRate
        {
            get
            {
                return this.taxAndDutyCurrencyRateField;
            }
            set
            {
                this.taxAndDutyCurrencyRateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string taxAndDutyStatusText
        {
            get
            {
                return this.taxAndDutyStatusTextField;
            }
            set
            {
                this.taxAndDutyStatusTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class AuditRecord
    {

        private System.Nullable<System.DateTime> dateField;

        private string textField;

        private string userIdField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string userId
        {
            get
            {
                return this.userIdField;
            }
            set
            {
                this.userIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ConsignmentActionResult
    {

        private string consignmentCodeField;

        private string errorCodeField;

        private string referenceField;

        private string textField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consignmentCode
        {
            get
            {
                return this.consignmentCodeField;
            }
            set
            {
                this.consignmentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string errorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void updateCompletedEventHandler(object sender, updateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal updateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void validateConsignmentsCompletedEventHandler(object sender, validateConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class validateConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal validateConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createConsignmentsCompletedEventHandler(object sender, createConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void updateConsignmentsCompletedEventHandler(object sender, updateConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal updateConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void deleteConsignmentCompletedEventHandler(object sender, deleteConsignmentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deleteConsignmentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal deleteConsignmentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelsAsPdfCompletedEventHandler(object sender, createLabelsAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelsAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelsAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createBulkLabelsAsPdfCompletedEventHandler(object sender, createBulkLabelsAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createBulkLabelsAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createBulkLabelsAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelAsPdfCompletedEventHandler(object sender, createLabelAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelForCartonIdAsPdfCompletedEventHandler(object sender, createLabelForCartonIdAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelForCartonIdAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelForCartonIdAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createDocumentationAsPdfCompletedEventHandler(object sender, createDocumentationAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createDocumentationAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createDocumentationAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createDocumentationForCartonIdAsPdfCompletedEventHandler(object sender, createDocumentationForCartonIdAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createDocumentationForCartonIdAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createDocumentationForCartonIdAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createBulkDocumentationAsPdfCompletedEventHandler(object sender, createBulkDocumentationAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createBulkDocumentationAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createBulkDocumentationAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createDocumentationPdfCompletedEventHandler(object sender, createDocumentationPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createDocumentationPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createDocumentationPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createPdfsForConsignmentsCompletedEventHandler(object sender, createPdfsForConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createPdfsForConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createPdfsForConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Labels Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Labels)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void markConsignmentsAsReadyToManifestCompletedEventHandler(object sender, markConsignmentsAsReadyToManifestCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class markConsignmentsAsReadyToManifestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal markConsignmentsAsReadyToManifestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void markConsignmentsAsPrintedCompletedEventHandler(object sender, markConsignmentsAsPrintedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class markConsignmentsAsPrintedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal markConsignmentsAsPrintedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void appendParcelsToConsignmentCompletedEventHandler(object sender, appendParcelsToConsignmentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class appendParcelsToConsignmentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal appendParcelsToConsignmentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Parcel[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Parcel[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void packProductsToParcelCompletedEventHandler(object sender, packProductsToParcelCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class packProductsToParcelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal packProductsToParcelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void unpackProductsFromParcelCompletedEventHandler(object sender, unpackProductsFromParcelCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class unpackProductsFromParcelCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal unpackProductsFromParcelCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void addInnerToOuterCompletedEventHandler(object sender, addInnerToOuterCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class addInnerToOuterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal addInnerToOuterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void removeInnerFromOuterCompletedEventHandler(object sender, removeInnerFromOuterCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class removeInnerFromOuterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal removeInnerFromOuterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void addConsignmentsToGroupCompletedEventHandler(object sender, addConsignmentsToGroupCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class addConsignmentsToGroupCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal addConsignmentsToGroupCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void removeConsignmentsFromGroupCompletedEventHandler(object sender, removeConsignmentsFromGroupCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class removeConsignmentsFromGroupCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal removeConsignmentsFromGroupCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createNextLabelsAsPdfCompletedEventHandler(object sender, createNextLabelsAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createNextLabelsAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createNextLabelsAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void voidConsignmentsCompletedEventHandler(object sender, voidConsignmentsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class voidConsignmentsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal voidConsignmentsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ConsignmentActionResult[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ConsignmentActionResult[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentAuditRecordsCompletedEventHandler(object sender, findConsignmentAuditRecordsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentAuditRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentAuditRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public AuditRecord[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((AuditRecord[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelAsZPLCompletedEventHandler(object sender, createLabelAsZPLCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelAsZPLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelAsZPLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelsAsZPLCompletedEventHandler(object sender, createLabelsAsZPLCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelsAsZPLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelsAsZPLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createLabelForCartonIdAsZPLCompletedEventHandler(object sender, createLabelForCartonIdAsZPLCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createLabelForCartonIdAsZPLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createLabelForCartonIdAsZPLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createBulkLabelsAsZPLCompletedEventHandler(object sender, createBulkLabelsAsZPLCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createBulkLabelsAsZPLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createBulkLabelsAsZPLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void deleteParcelFromConsignmentCompletedEventHandler(object sender, deleteParcelFromConsignmentCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deleteParcelFromConsignmentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal deleteParcelFromConsignmentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void deleteParcelFromConsignmentWithCartonIdCompletedEventHandler(object sender, deleteParcelFromConsignmentWithCartonIdCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class deleteParcelFromConsignmentWithCartonIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal deleteParcelFromConsignmentWithCartonIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void calculateTaxAndDutyCompletedEventHandler(object sender, calculateTaxAndDutyCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class calculateTaxAndDutyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal calculateTaxAndDutyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public TaxAndDuty Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((TaxAndDuty)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ConsignmentTrackingServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ParcelStatusHistory))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ParcelTrackingItem))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ParcelTrackingInfo))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ConsignmentTrackingInfo))]
    public partial class ConsignmentTrackingServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback findParcelTrackingByOrderReferenceOperationCompleted;

        private System.Threading.SendOrPostCallback findParcelTrackingByConsignmentCodeOperationCompleted;

        private System.Threading.SendOrPostCallback findAllParcelStatusesBetweenDatesOperationCompleted;

        /// <remarks/>
        public ConsignmentTrackingServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/ConsignmentTrackingService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event findParcelTrackingByOrderReferenceCompletedEventHandler findParcelTrackingByOrderReferenceCompleted;

        /// <remarks/>
        public event findParcelTrackingByConsignmentCodeCompletedEventHandler findParcelTrackingByConsignmentCodeCompleted;

        /// <remarks/>
        public event findAllParcelStatusesBetweenDatesCompletedEventHandler findAllParcelStatusesBetweenDatesCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findParcelTrackingByOrderReferenceReturn")]
        public ConsignmentTrackingInfo[] findParcelTrackingByOrderReference(string orderReference)
        {
            object[] results = this.Invoke("findParcelTrackingByOrderReference", new object[] {
                    orderReference});
            return ((ConsignmentTrackingInfo[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindParcelTrackingByOrderReference(string orderReference, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findParcelTrackingByOrderReference", new object[] {
                    orderReference}, callback, asyncState);
        }

        /// <remarks/>
        public ConsignmentTrackingInfo[] EndfindParcelTrackingByOrderReference(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ConsignmentTrackingInfo[])(results[0]));
        }

        /// <remarks/>
        public void findParcelTrackingByOrderReferenceAsync(string orderReference)
        {
            this.findParcelTrackingByOrderReferenceAsync(orderReference, null);
        }

        /// <remarks/>
        public void findParcelTrackingByOrderReferenceAsync(string orderReference, object userState)
        {
            if ((this.findParcelTrackingByOrderReferenceOperationCompleted == null))
            {
                this.findParcelTrackingByOrderReferenceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindParcelTrackingByOrderReferenceOperationCompleted);
            }
            this.InvokeAsync("findParcelTrackingByOrderReference", new object[] {
                    orderReference}, this.findParcelTrackingByOrderReferenceOperationCompleted, userState);
        }

        private void OnfindParcelTrackingByOrderReferenceOperationCompleted(object arg)
        {
            if ((this.findParcelTrackingByOrderReferenceCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findParcelTrackingByOrderReferenceCompleted(this, new findParcelTrackingByOrderReferenceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findParcelTrackingByConsignmentCodeReturn")]
        public ConsignmentTrackingInfo[] findParcelTrackingByConsignmentCode(string consignmentCode)
        {
            object[] results = this.Invoke("findParcelTrackingByConsignmentCode", new object[] {
                    consignmentCode});
            return ((ConsignmentTrackingInfo[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindParcelTrackingByConsignmentCode(string consignmentCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findParcelTrackingByConsignmentCode", new object[] {
                    consignmentCode}, callback, asyncState);
        }

        /// <remarks/>
        public ConsignmentTrackingInfo[] EndfindParcelTrackingByConsignmentCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ConsignmentTrackingInfo[])(results[0]));
        }

        /// <remarks/>
        public void findParcelTrackingByConsignmentCodeAsync(string consignmentCode)
        {
            this.findParcelTrackingByConsignmentCodeAsync(consignmentCode, null);
        }

        /// <remarks/>
        public void findParcelTrackingByConsignmentCodeAsync(string consignmentCode, object userState)
        {
            if ((this.findParcelTrackingByConsignmentCodeOperationCompleted == null))
            {
                this.findParcelTrackingByConsignmentCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindParcelTrackingByConsignmentCodeOperationCompleted);
            }
            this.InvokeAsync("findParcelTrackingByConsignmentCode", new object[] {
                    consignmentCode}, this.findParcelTrackingByConsignmentCodeOperationCompleted, userState);
        }

        private void OnfindParcelTrackingByConsignmentCodeOperationCompleted(object arg)
        {
            if ((this.findParcelTrackingByConsignmentCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findParcelTrackingByConsignmentCodeCompleted(this, new findParcelTrackingByConsignmentCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findAllParcelStatusesBetweenDatesReturn")]
        public ParcelStatusHistory[] findAllParcelStatusesBetweenDates(System.DateTime fromDate, System.DateTime toDate)
        {
            object[] results = this.Invoke("findAllParcelStatusesBetweenDates", new object[] {
                    fromDate,
                    toDate});
            return ((ParcelStatusHistory[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindAllParcelStatusesBetweenDates(System.DateTime fromDate, System.DateTime toDate, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findAllParcelStatusesBetweenDates", new object[] {
                    fromDate,
                    toDate}, callback, asyncState);
        }

        /// <remarks/>
        public ParcelStatusHistory[] EndfindAllParcelStatusesBetweenDates(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ParcelStatusHistory[])(results[0]));
        }

        /// <remarks/>
        public void findAllParcelStatusesBetweenDatesAsync(System.DateTime fromDate, System.DateTime toDate)
        {
            this.findAllParcelStatusesBetweenDatesAsync(fromDate, toDate, null);
        }

        /// <remarks/>
        public void findAllParcelStatusesBetweenDatesAsync(System.DateTime fromDate, System.DateTime toDate, object userState)
        {
            if ((this.findAllParcelStatusesBetweenDatesOperationCompleted == null))
            {
                this.findAllParcelStatusesBetweenDatesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindAllParcelStatusesBetweenDatesOperationCompleted);
            }
            this.InvokeAsync("findAllParcelStatusesBetweenDates", new object[] {
                    fromDate,
                    toDate}, this.findAllParcelStatusesBetweenDatesOperationCompleted, userState);
        }

        private void OnfindAllParcelStatusesBetweenDatesOperationCompleted(object arg)
        {
            if ((this.findAllParcelStatusesBetweenDatesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findAllParcelStatusesBetweenDatesCompleted(this, new findAllParcelStatusesBetweenDatesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ConsignmentTrackingInfo
    {

        private string carrierConsignmentCodeField;

        private string consignmentCodeField;

        private ParcelTrackingInfo[] parcelsField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierConsignmentCode
        {
            get
            {
                return this.carrierConsignmentCodeField;
            }
            set
            {
                this.carrierConsignmentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consignmentCode
        {
            get
            {
                return this.consignmentCodeField;
            }
            set
            {
                this.consignmentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public ParcelTrackingInfo[] parcels
        {
            get
            {
                return this.parcelsField;
            }
            set
            {
                this.parcelsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ParcelTrackingInfo
    {

        private string codeField;

        private ParcelTrackingItem[] itemsField;

        private int numberField;

        private string parcelStatusNameField;

        private string statusTextField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public ParcelTrackingItem[] items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        public int number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string parcelStatusName
        {
            get
            {
                return this.parcelStatusNameField;
            }
            set
            {
                this.parcelStatusNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string statusText
        {
            get
            {
                return this.statusTextField;
            }
            set
            {
                this.statusTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ParcelTrackingItem
    {

        private System.Nullable<System.DateTime> acheivedDateTimeField;

        private string parcelStatusNameField;

        private string statusTextField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> acheivedDateTime
        {
            get
            {
                return this.acheivedDateTimeField;
            }
            set
            {
                this.acheivedDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string parcelStatusName
        {
            get
            {
                return this.parcelStatusNameField;
            }
            set
            {
                this.parcelStatusNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string statusText
        {
            get
            {
                return this.statusTextField;
            }
            set
            {
                this.statusTextField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ParcelStatusHistory
    {

        private System.Nullable<System.DateTime> achievedDateTimeField;

        private string carrierReasonCodeField;

        private string carrierStatusCodeField;

        private string consignmentCodeField;

        private string depotAchievingStatusField;

        private string parcelStatusDescField;

        private string parcelStatusTextField;

        private System.Nullable<System.DateTime> timeAppliedField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> achievedDateTime
        {
            get
            {
                return this.achievedDateTimeField;
            }
            set
            {
                this.achievedDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierReasonCode
        {
            get
            {
                return this.carrierReasonCodeField;
            }
            set
            {
                this.carrierReasonCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierStatusCode
        {
            get
            {
                return this.carrierStatusCodeField;
            }
            set
            {
                this.carrierStatusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string consignmentCode
        {
            get
            {
                return this.consignmentCodeField;
            }
            set
            {
                this.consignmentCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string depotAchievingStatus
        {
            get
            {
                return this.depotAchievingStatusField;
            }
            set
            {
                this.depotAchievingStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string parcelStatusDesc
        {
            get
            {
                return this.parcelStatusDescField;
            }
            set
            {
                this.parcelStatusDescField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string parcelStatusText
        {
            get
            {
                return this.parcelStatusTextField;
            }
            set
            {
                this.parcelStatusTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> timeApplied
        {
            get
            {
                return this.timeAppliedField;
            }
            set
            {
                this.timeAppliedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findParcelTrackingByOrderReferenceCompletedEventHandler(object sender, findParcelTrackingByOrderReferenceCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findParcelTrackingByOrderReferenceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findParcelTrackingByOrderReferenceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ConsignmentTrackingInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ConsignmentTrackingInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findParcelTrackingByConsignmentCodeCompletedEventHandler(object sender, findParcelTrackingByConsignmentCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findParcelTrackingByConsignmentCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findParcelTrackingByConsignmentCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ConsignmentTrackingInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ConsignmentTrackingInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findAllParcelStatusesBetweenDatesCompletedEventHandler(object sender, findAllParcelStatusesBetweenDatesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findAllParcelStatusesBetweenDatesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findAllParcelStatusesBetweenDatesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ParcelStatusHistory[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ParcelStatusHistory[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "DebugServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    public partial class DebugServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback debugConsignmentWhyNotOperationCompleted;

        private System.Threading.SendOrPostCallback debugConsignmentWhyNotWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback debugWhyNotWithBookingCodeOperationCompleted;

        private System.Threading.SendOrPostCallback debugWhyNotOperationCompleted;

        /// <remarks/>
        public DebugServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/DebugService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event debugConsignmentWhyNotCompletedEventHandler debugConsignmentWhyNotCompleted;

        /// <remarks/>
        public event debugConsignmentWhyNotWithBookingCodeCompletedEventHandler debugConsignmentWhyNotWithBookingCodeCompleted;

        /// <remarks/>
        public event debugWhyNotWithBookingCodeCompletedEventHandler debugWhyNotWithBookingCodeCompleted;

        /// <remarks/>
        public event debugWhyNotCompletedEventHandler debugWhyNotCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("debugConsignmentWhyNotReturn")]
        public string[] debugConsignmentWhyNot(string consignmentCode, AllocationFilter filter)
        {
            object[] results = this.Invoke("debugConsignmentWhyNot", new object[] {
                    consignmentCode,
                    filter});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindebugConsignmentWhyNot(string consignmentCode, AllocationFilter filter, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("debugConsignmentWhyNot", new object[] {
                    consignmentCode,
                    filter}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EnddebugConsignmentWhyNot(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void debugConsignmentWhyNotAsync(string consignmentCode, AllocationFilter filter)
        {
            this.debugConsignmentWhyNotAsync(consignmentCode, filter, null);
        }

        /// <remarks/>
        public void debugConsignmentWhyNotAsync(string consignmentCode, AllocationFilter filter, object userState)
        {
            if ((this.debugConsignmentWhyNotOperationCompleted == null))
            {
                this.debugConsignmentWhyNotOperationCompleted = new System.Threading.SendOrPostCallback(this.OndebugConsignmentWhyNotOperationCompleted);
            }
            this.InvokeAsync("debugConsignmentWhyNot", new object[] {
                    consignmentCode,
                    filter}, this.debugConsignmentWhyNotOperationCompleted, userState);
        }

        private void OndebugConsignmentWhyNotOperationCompleted(object arg)
        {
            if ((this.debugConsignmentWhyNotCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.debugConsignmentWhyNotCompleted(this, new debugConsignmentWhyNotCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("debugConsignmentWhyNotWithBookingCodeReturn")]
        public string[] debugConsignmentWhyNotWithBookingCode(string consignmentCode, string bookingCode)
        {
            object[] results = this.Invoke("debugConsignmentWhyNotWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindebugConsignmentWhyNotWithBookingCode(string consignmentCode, string bookingCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("debugConsignmentWhyNotWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EnddebugConsignmentWhyNotWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void debugConsignmentWhyNotWithBookingCodeAsync(string consignmentCode, string bookingCode)
        {
            this.debugConsignmentWhyNotWithBookingCodeAsync(consignmentCode, bookingCode, null);
        }

        /// <remarks/>
        public void debugConsignmentWhyNotWithBookingCodeAsync(string consignmentCode, string bookingCode, object userState)
        {
            if ((this.debugConsignmentWhyNotWithBookingCodeOperationCompleted == null))
            {
                this.debugConsignmentWhyNotWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndebugConsignmentWhyNotWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("debugConsignmentWhyNotWithBookingCode", new object[] {
                    consignmentCode,
                    bookingCode}, this.debugConsignmentWhyNotWithBookingCodeOperationCompleted, userState);
        }

        private void OndebugConsignmentWhyNotWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.debugConsignmentWhyNotWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.debugConsignmentWhyNotWithBookingCodeCompleted(this, new debugConsignmentWhyNotWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("debugWhyNotWithBookingCodeReturn")]
        public string[] debugWhyNotWithBookingCode(Consignment consignment, string bookingCode)
        {
            object[] results = this.Invoke("debugWhyNotWithBookingCode", new object[] {
                    consignment,
                    bookingCode});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindebugWhyNotWithBookingCode(Consignment consignment, string bookingCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("debugWhyNotWithBookingCode", new object[] {
                    consignment,
                    bookingCode}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EnddebugWhyNotWithBookingCode(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void debugWhyNotWithBookingCodeAsync(Consignment consignment, string bookingCode)
        {
            this.debugWhyNotWithBookingCodeAsync(consignment, bookingCode, null);
        }

        /// <remarks/>
        public void debugWhyNotWithBookingCodeAsync(Consignment consignment, string bookingCode, object userState)
        {
            if ((this.debugWhyNotWithBookingCodeOperationCompleted == null))
            {
                this.debugWhyNotWithBookingCodeOperationCompleted = new System.Threading.SendOrPostCallback(this.OndebugWhyNotWithBookingCodeOperationCompleted);
            }
            this.InvokeAsync("debugWhyNotWithBookingCode", new object[] {
                    consignment,
                    bookingCode}, this.debugWhyNotWithBookingCodeOperationCompleted, userState);
        }

        private void OndebugWhyNotWithBookingCodeOperationCompleted(object arg)
        {
            if ((this.debugWhyNotWithBookingCodeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.debugWhyNotWithBookingCodeCompleted(this, new debugWhyNotWithBookingCodeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("debugWhyNotReturn")]
        public string[] debugWhyNot(Consignment consignment, AllocationFilter filter)
        {
            object[] results = this.Invoke("debugWhyNot", new object[] {
                    consignment,
                    filter});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindebugWhyNot(Consignment consignment, AllocationFilter filter, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("debugWhyNot", new object[] {
                    consignment,
                    filter}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EnddebugWhyNot(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void debugWhyNotAsync(Consignment consignment, AllocationFilter filter)
        {
            this.debugWhyNotAsync(consignment, filter, null);
        }

        /// <remarks/>
        public void debugWhyNotAsync(Consignment consignment, AllocationFilter filter, object userState)
        {
            if ((this.debugWhyNotOperationCompleted == null))
            {
                this.debugWhyNotOperationCompleted = new System.Threading.SendOrPostCallback(this.OndebugWhyNotOperationCompleted);
            }
            this.InvokeAsync("debugWhyNot", new object[] {
                    consignment,
                    filter}, this.debugWhyNotOperationCompleted, userState);
        }

        private void OndebugWhyNotOperationCompleted(object arg)
        {
            if ((this.debugWhyNotCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.debugWhyNotCompleted(this, new debugWhyNotCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void debugConsignmentWhyNotCompletedEventHandler(object sender, debugConsignmentWhyNotCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class debugConsignmentWhyNotCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal debugConsignmentWhyNotCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void debugConsignmentWhyNotWithBookingCodeCompletedEventHandler(object sender, debugConsignmentWhyNotWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class debugConsignmentWhyNotWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal debugConsignmentWhyNotWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void debugWhyNotWithBookingCodeCompletedEventHandler(object sender, debugWhyNotWithBookingCodeCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class debugWhyNotWithBookingCodeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal debugWhyNotWithBookingCodeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void debugWhyNotCompletedEventHandler(object sender, debugWhyNotCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class debugWhyNotCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal debugWhyNotCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "InformationServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(CarrierService))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(CodeName))]
    public partial class InformationServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback findTransactionTypesOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentStatusesOperationCompleted;

        private System.Threading.SendOrPostCallback findManifestStatusesOperationCompleted;

        private System.Threading.SendOrPostCallback findWarehousesOperationCompleted;

        private System.Threading.SendOrPostCallback findCarriersOperationCompleted;

        private System.Threading.SendOrPostCallback findCarrierServiceTypesOperationCompleted;

        private System.Threading.SendOrPostCallback findGroupsOperationCompleted;

        private System.Threading.SendOrPostCallback findCarrierServicesOperationCompleted;

        private System.Threading.SendOrPostCallback findPODTypesOperationCompleted;

        private System.Threading.SendOrPostCallback verifyAddressesOperationCompleted;

        private System.Threading.SendOrPostCallback findSimilarAddressesOperationCompleted;

        /// <remarks/>
        public InformationServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/InformationService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event findTransactionTypesCompletedEventHandler findTransactionTypesCompleted;

        /// <remarks/>
        public event findConsignmentStatusesCompletedEventHandler findConsignmentStatusesCompleted;

        /// <remarks/>
        public event findManifestStatusesCompletedEventHandler findManifestStatusesCompleted;

        /// <remarks/>
        public event findWarehousesCompletedEventHandler findWarehousesCompleted;

        /// <remarks/>
        public event findCarriersCompletedEventHandler findCarriersCompleted;

        /// <remarks/>
        public event findCarrierServiceTypesCompletedEventHandler findCarrierServiceTypesCompleted;

        /// <remarks/>
        public event findGroupsCompletedEventHandler findGroupsCompleted;

        /// <remarks/>
        public event findCarrierServicesCompletedEventHandler findCarrierServicesCompleted;

        /// <remarks/>
        public event findPODTypesCompletedEventHandler findPODTypesCompleted;

        /// <remarks/>
        public event verifyAddressesCompletedEventHandler verifyAddressesCompleted;

        /// <remarks/>
        public event findSimilarAddressesCompletedEventHandler findSimilarAddressesCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findTransactionTypesReturn")]
        public string[] findTransactionTypes()
        {
            object[] results = this.Invoke("findTransactionTypes", new object[0]);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindTransactionTypes(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findTransactionTypes", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string[] EndfindTransactionTypes(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void findTransactionTypesAsync()
        {
            this.findTransactionTypesAsync(null);
        }

        /// <remarks/>
        public void findTransactionTypesAsync(object userState)
        {
            if ((this.findTransactionTypesOperationCompleted == null))
            {
                this.findTransactionTypesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindTransactionTypesOperationCompleted);
            }
            this.InvokeAsync("findTransactionTypes", new object[0], this.findTransactionTypesOperationCompleted, userState);
        }

        private void OnfindTransactionTypesOperationCompleted(object arg)
        {
            if ((this.findTransactionTypesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findTransactionTypesCompleted(this, new findTransactionTypesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentStatusesReturn")]
        public string[] findConsignmentStatuses()
        {
            object[] results = this.Invoke("findConsignmentStatuses", new object[0]);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentStatuses(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentStatuses", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string[] EndfindConsignmentStatuses(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentStatusesAsync()
        {
            this.findConsignmentStatusesAsync(null);
        }

        /// <remarks/>
        public void findConsignmentStatusesAsync(object userState)
        {
            if ((this.findConsignmentStatusesOperationCompleted == null))
            {
                this.findConsignmentStatusesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentStatusesOperationCompleted);
            }
            this.InvokeAsync("findConsignmentStatuses", new object[0], this.findConsignmentStatusesOperationCompleted, userState);
        }

        private void OnfindConsignmentStatusesOperationCompleted(object arg)
        {
            if ((this.findConsignmentStatusesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentStatusesCompleted(this, new findConsignmentStatusesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findManifestStatusesReturn")]
        public string[] findManifestStatuses()
        {
            object[] results = this.Invoke("findManifestStatuses", new object[0]);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindManifestStatuses(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findManifestStatuses", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string[] EndfindManifestStatuses(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void findManifestStatusesAsync()
        {
            this.findManifestStatusesAsync(null);
        }

        /// <remarks/>
        public void findManifestStatusesAsync(object userState)
        {
            if ((this.findManifestStatusesOperationCompleted == null))
            {
                this.findManifestStatusesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindManifestStatusesOperationCompleted);
            }
            this.InvokeAsync("findManifestStatuses", new object[0], this.findManifestStatusesOperationCompleted, userState);
        }

        private void OnfindManifestStatusesOperationCompleted(object arg)
        {
            if ((this.findManifestStatusesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findManifestStatusesCompleted(this, new findManifestStatusesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findWarehousesReturn")]
        public CodeName[] findWarehouses()
        {
            object[] results = this.Invoke("findWarehouses", new object[0]);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindWarehouses(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findWarehouses", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public CodeName[] EndfindWarehouses(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public void findWarehousesAsync()
        {
            this.findWarehousesAsync(null);
        }

        /// <remarks/>
        public void findWarehousesAsync(object userState)
        {
            if ((this.findWarehousesOperationCompleted == null))
            {
                this.findWarehousesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindWarehousesOperationCompleted);
            }
            this.InvokeAsync("findWarehouses", new object[0], this.findWarehousesOperationCompleted, userState);
        }

        private void OnfindWarehousesOperationCompleted(object arg)
        {
            if ((this.findWarehousesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findWarehousesCompleted(this, new findWarehousesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findCarriersReturn")]
        public CodeName[] findCarriers()
        {
            object[] results = this.Invoke("findCarriers", new object[0]);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindCarriers(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findCarriers", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public CodeName[] EndfindCarriers(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public void findCarriersAsync()
        {
            this.findCarriersAsync(null);
        }

        /// <remarks/>
        public void findCarriersAsync(object userState)
        {
            if ((this.findCarriersOperationCompleted == null))
            {
                this.findCarriersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindCarriersOperationCompleted);
            }
            this.InvokeAsync("findCarriers", new object[0], this.findCarriersOperationCompleted, userState);
        }

        private void OnfindCarriersOperationCompleted(object arg)
        {
            if ((this.findCarriersCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findCarriersCompleted(this, new findCarriersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findCarrierServiceTypesReturn")]
        public CodeName[] findCarrierServiceTypes()
        {
            object[] results = this.Invoke("findCarrierServiceTypes", new object[0]);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindCarrierServiceTypes(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findCarrierServiceTypes", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public CodeName[] EndfindCarrierServiceTypes(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public void findCarrierServiceTypesAsync()
        {
            this.findCarrierServiceTypesAsync(null);
        }

        /// <remarks/>
        public void findCarrierServiceTypesAsync(object userState)
        {
            if ((this.findCarrierServiceTypesOperationCompleted == null))
            {
                this.findCarrierServiceTypesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindCarrierServiceTypesOperationCompleted);
            }
            this.InvokeAsync("findCarrierServiceTypes", new object[0], this.findCarrierServiceTypesOperationCompleted, userState);
        }

        private void OnfindCarrierServiceTypesOperationCompleted(object arg)
        {
            if ((this.findCarrierServiceTypesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findCarrierServiceTypesCompleted(this, new findCarrierServiceTypesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findGroupsReturn")]
        public CodeName[] findGroups()
        {
            object[] results = this.Invoke("findGroups", new object[0]);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindGroups(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findGroups", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public CodeName[] EndfindGroups(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CodeName[])(results[0]));
        }

        /// <remarks/>
        public void findGroupsAsync()
        {
            this.findGroupsAsync(null);
        }

        /// <remarks/>
        public void findGroupsAsync(object userState)
        {
            if ((this.findGroupsOperationCompleted == null))
            {
                this.findGroupsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindGroupsOperationCompleted);
            }
            this.InvokeAsync("findGroups", new object[0], this.findGroupsOperationCompleted, userState);
        }

        private void OnfindGroupsOperationCompleted(object arg)
        {
            if ((this.findGroupsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findGroupsCompleted(this, new findGroupsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findCarrierServicesReturn")]
        public CarrierService[] findCarrierServices(string carrierCode)
        {
            object[] results = this.Invoke("findCarrierServices", new object[] {
                    carrierCode});
            return ((CarrierService[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindCarrierServices(string carrierCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findCarrierServices", new object[] {
                    carrierCode}, callback, asyncState);
        }

        /// <remarks/>
        public CarrierService[] EndfindCarrierServices(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((CarrierService[])(results[0]));
        }

        /// <remarks/>
        public void findCarrierServicesAsync(string carrierCode)
        {
            this.findCarrierServicesAsync(carrierCode, null);
        }

        /// <remarks/>
        public void findCarrierServicesAsync(string carrierCode, object userState)
        {
            if ((this.findCarrierServicesOperationCompleted == null))
            {
                this.findCarrierServicesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindCarrierServicesOperationCompleted);
            }
            this.InvokeAsync("findCarrierServices", new object[] {
                    carrierCode}, this.findCarrierServicesOperationCompleted, userState);
        }

        private void OnfindCarrierServicesOperationCompleted(object arg)
        {
            if ((this.findCarrierServicesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findCarrierServicesCompleted(this, new findCarrierServicesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findPODTypesReturn")]
        public string[] findPODTypes()
        {
            object[] results = this.Invoke("findPODTypes", new object[0]);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindPODTypes(System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findPODTypes", new object[0], callback, asyncState);
        }

        /// <remarks/>
        public string[] EndfindPODTypes(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void findPODTypesAsync()
        {
            this.findPODTypesAsync(null);
        }

        /// <remarks/>
        public void findPODTypesAsync(object userState)
        {
            if ((this.findPODTypesOperationCompleted == null))
            {
                this.findPODTypesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindPODTypesOperationCompleted);
            }
            this.InvokeAsync("findPODTypes", new object[0], this.findPODTypesOperationCompleted, userState);
        }

        private void OnfindPODTypesOperationCompleted(object arg)
        {
            if ((this.findPODTypesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findPODTypesCompleted(this, new findPODTypesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("verifyAddressesReturn")]
        public VerifiedAddress[] verifyAddresses(Address[] addresses)
        {
            object[] results = this.Invoke("verifyAddresses", new object[] {
                    addresses});
            return ((VerifiedAddress[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginverifyAddresses(Address[] addresses, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("verifyAddresses", new object[] {
                    addresses}, callback, asyncState);
        }

        /// <remarks/>
        public VerifiedAddress[] EndverifyAddresses(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((VerifiedAddress[])(results[0]));
        }

        /// <remarks/>
        public void verifyAddressesAsync(Address[] addresses)
        {
            this.verifyAddressesAsync(addresses, null);
        }

        /// <remarks/>
        public void verifyAddressesAsync(Address[] addresses, object userState)
        {
            if ((this.verifyAddressesOperationCompleted == null))
            {
                this.verifyAddressesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnverifyAddressesOperationCompleted);
            }
            this.InvokeAsync("verifyAddresses", new object[] {
                    addresses}, this.verifyAddressesOperationCompleted, userState);
        }

        private void OnverifyAddressesOperationCompleted(object arg)
        {
            if ((this.verifyAddressesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.verifyAddressesCompleted(this, new verifyAddressesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findSimilarAddressesReturn")]
        public Address[] findSimilarAddresses(Address address)
        {
            object[] results = this.Invoke("findSimilarAddresses", new object[] {
                    address});
            return ((Address[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindSimilarAddresses(Address address, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findSimilarAddresses", new object[] {
                    address}, callback, asyncState);
        }

        /// <remarks/>
        public Address[] EndfindSimilarAddresses(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Address[])(results[0]));
        }

        /// <remarks/>
        public void findSimilarAddressesAsync(Address address)
        {
            this.findSimilarAddressesAsync(address, null);
        }

        /// <remarks/>
        public void findSimilarAddressesAsync(Address address, object userState)
        {
            if ((this.findSimilarAddressesOperationCompleted == null))
            {
                this.findSimilarAddressesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindSimilarAddressesOperationCompleted);
            }
            this.InvokeAsync("findSimilarAddresses", new object[] {
                    address}, this.findSimilarAddressesOperationCompleted, userState);
        }

        private void OnfindSimilarAddressesOperationCompleted(object arg)
        {
            if ((this.findSimilarAddressesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findSimilarAddressesCompleted(this, new findSimilarAddressesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class CodeName
    {

        private string codeField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class CarrierService
    {

        private string codeField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findTransactionTypesCompletedEventHandler(object sender, findTransactionTypesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findTransactionTypesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findTransactionTypesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentStatusesCompletedEventHandler(object sender, findConsignmentStatusesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentStatusesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentStatusesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findManifestStatusesCompletedEventHandler(object sender, findManifestStatusesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findManifestStatusesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findManifestStatusesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findWarehousesCompletedEventHandler(object sender, findWarehousesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findWarehousesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findWarehousesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CodeName[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CodeName[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findCarriersCompletedEventHandler(object sender, findCarriersCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findCarriersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findCarriersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CodeName[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CodeName[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findCarrierServiceTypesCompletedEventHandler(object sender, findCarrierServiceTypesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findCarrierServiceTypesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findCarrierServiceTypesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CodeName[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CodeName[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findGroupsCompletedEventHandler(object sender, findGroupsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findGroupsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findGroupsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CodeName[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CodeName[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findCarrierServicesCompletedEventHandler(object sender, findCarrierServicesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findCarrierServicesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findCarrierServicesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public CarrierService[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CarrierService[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findPODTypesCompletedEventHandler(object sender, findPODTypesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findPODTypesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findPODTypesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void verifyAddressesCompletedEventHandler(object sender, verifyAddressesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class verifyAddressesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal verifyAddressesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public VerifiedAddress[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((VerifiedAddress[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findSimilarAddressesCompletedEventHandler(object sender, findSimilarAddressesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findSimilarAddressesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findSimilarAddressesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Address[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Address[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "SetupServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    public partial class SetupServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback enableCarriersOperationCompleted;

        private System.Threading.SendOrPostCallback disableCarriersOperationCompleted;

        private System.Threading.SendOrPostCallback enableCarrierServicesOperationCompleted;

        private System.Threading.SendOrPostCallback disableCarrierServicesOperationCompleted;

        /// <remarks/>
        public SetupServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/SetupService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event enableCarriersCompletedEventHandler enableCarriersCompleted;

        /// <remarks/>
        public event disableCarriersCompletedEventHandler disableCarriersCompleted;

        /// <remarks/>
        public event enableCarrierServicesCompletedEventHandler enableCarrierServicesCompleted;

        /// <remarks/>
        public event disableCarrierServicesCompletedEventHandler disableCarrierServicesCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("enableCarriersReturn")]
        public bool enableCarriers(string[] carrierCodes)
        {
            object[] results = this.Invoke("enableCarriers", new object[] {
                    carrierCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginenableCarriers(string[] carrierCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("enableCarriers", new object[] {
                    carrierCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndenableCarriers(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void enableCarriersAsync(string[] carrierCodes)
        {
            this.enableCarriersAsync(carrierCodes, null);
        }

        /// <remarks/>
        public void enableCarriersAsync(string[] carrierCodes, object userState)
        {
            if ((this.enableCarriersOperationCompleted == null))
            {
                this.enableCarriersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnenableCarriersOperationCompleted);
            }
            this.InvokeAsync("enableCarriers", new object[] {
                    carrierCodes}, this.enableCarriersOperationCompleted, userState);
        }

        private void OnenableCarriersOperationCompleted(object arg)
        {
            if ((this.enableCarriersCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.enableCarriersCompleted(this, new enableCarriersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("disableCarriersReturn")]
        public bool disableCarriers(string[] carrierCodes)
        {
            object[] results = this.Invoke("disableCarriers", new object[] {
                    carrierCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindisableCarriers(string[] carrierCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("disableCarriers", new object[] {
                    carrierCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EnddisableCarriers(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void disableCarriersAsync(string[] carrierCodes)
        {
            this.disableCarriersAsync(carrierCodes, null);
        }

        /// <remarks/>
        public void disableCarriersAsync(string[] carrierCodes, object userState)
        {
            if ((this.disableCarriersOperationCompleted == null))
            {
                this.disableCarriersOperationCompleted = new System.Threading.SendOrPostCallback(this.OndisableCarriersOperationCompleted);
            }
            this.InvokeAsync("disableCarriers", new object[] {
                    carrierCodes}, this.disableCarriersOperationCompleted, userState);
        }

        private void OndisableCarriersOperationCompleted(object arg)
        {
            if ((this.disableCarriersCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.disableCarriersCompleted(this, new disableCarriersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("enableCarrierServicesReturn")]
        public bool enableCarrierServices(string[] carrierServiceCodes)
        {
            object[] results = this.Invoke("enableCarrierServices", new object[] {
                    carrierServiceCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginenableCarrierServices(string[] carrierServiceCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("enableCarrierServices", new object[] {
                    carrierServiceCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndenableCarrierServices(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void enableCarrierServicesAsync(string[] carrierServiceCodes)
        {
            this.enableCarrierServicesAsync(carrierServiceCodes, null);
        }

        /// <remarks/>
        public void enableCarrierServicesAsync(string[] carrierServiceCodes, object userState)
        {
            if ((this.enableCarrierServicesOperationCompleted == null))
            {
                this.enableCarrierServicesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnenableCarrierServicesOperationCompleted);
            }
            this.InvokeAsync("enableCarrierServices", new object[] {
                    carrierServiceCodes}, this.enableCarrierServicesOperationCompleted, userState);
        }

        private void OnenableCarrierServicesOperationCompleted(object arg)
        {
            if ((this.enableCarrierServicesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.enableCarrierServicesCompleted(this, new enableCarrierServicesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("disableCarrierServicesReturn")]
        public bool disableCarrierServices(string[] carrierServiceCodes)
        {
            object[] results = this.Invoke("disableCarrierServices", new object[] {
                    carrierServiceCodes});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegindisableCarrierServices(string[] carrierServiceCodes, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("disableCarrierServices", new object[] {
                    carrierServiceCodes}, callback, asyncState);
        }

        /// <remarks/>
        public bool EnddisableCarrierServices(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void disableCarrierServicesAsync(string[] carrierServiceCodes)
        {
            this.disableCarrierServicesAsync(carrierServiceCodes, null);
        }

        /// <remarks/>
        public void disableCarrierServicesAsync(string[] carrierServiceCodes, object userState)
        {
            if ((this.disableCarrierServicesOperationCompleted == null))
            {
                this.disableCarrierServicesOperationCompleted = new System.Threading.SendOrPostCallback(this.OndisableCarrierServicesOperationCompleted);
            }
            this.InvokeAsync("disableCarrierServices", new object[] {
                    carrierServiceCodes}, this.disableCarrierServicesOperationCompleted, userState);
        }

        private void OndisableCarrierServicesOperationCompleted(object arg)
        {
            if ((this.disableCarrierServicesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.disableCarrierServicesCompleted(this, new disableCarrierServicesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void enableCarriersCompletedEventHandler(object sender, enableCarriersCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class enableCarriersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal enableCarriersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void disableCarriersCompletedEventHandler(object sender, disableCarriersCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class disableCarriersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal disableCarriersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void enableCarrierServicesCompletedEventHandler(object sender, enableCarrierServicesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class enableCarrierServicesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal enableCarrierServicesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void disableCarrierServicesCompletedEventHandler(object sender, disableCarrierServicesCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class disableCarrierServicesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal disableCarrierServicesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ManifestServiceSoapBinding", Namespace = "urn:DeliveryManager/services")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(ReadyToManifestInfo))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Manifest))]
    public partial class ManifestServiceService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback createManifestForFutureDespatchOperationCompleted;

        private System.Threading.SendOrPostCallback createManifestOperationCompleted;

        private System.Threading.SendOrPostCallback findConsignmentsOnManifestOperationCompleted;

        private System.Threading.SendOrPostCallback findManifestsOperationCompleted;

        private System.Threading.SendOrPostCallback findReadyToManifestRecordsOperationCompleted;

        private System.Threading.SendOrPostCallback sendManifestOperationCompleted;

        private System.Threading.SendOrPostCallback createManifestAsPdfOperationCompleted;

        /// <remarks/>
        public ManifestServiceService()
        {
            this.Url = "http://test2.metapack.com/dm/services/ManifestService";
        }

        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest req;
            req = (System.Net.HttpWebRequest)base.GetWebRequest(uri);
            req.ProtocolVersion = System.Net.HttpVersion.Version10;
            return req;
        }

        /// <remarks/>
        public event createManifestForFutureDespatchCompletedEventHandler createManifestForFutureDespatchCompleted;

        /// <remarks/>
        public event createManifestCompletedEventHandler createManifestCompleted;

        /// <remarks/>
        public event findConsignmentsOnManifestCompletedEventHandler findConsignmentsOnManifestCompleted;

        /// <remarks/>
        public event findManifestsCompletedEventHandler findManifestsCompleted;

        /// <remarks/>
        public event findReadyToManifestRecordsCompletedEventHandler findReadyToManifestRecordsCompleted;

        /// <remarks/>
        public event sendManifestCompletedEventHandler sendManifestCompleted;

        /// <remarks/>
        public event createManifestAsPdfCompletedEventHandler createManifestAsPdfCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createManifestForFutureDespatchReturn")]
        public string[] createManifestForFutureDespatch(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime despatchDate, bool specificDateOnly)
        {
            object[] results = this.Invoke("createManifestForFutureDespatch", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    despatchDate,
                    specificDateOnly});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateManifestForFutureDespatch(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime despatchDate, bool specificDateOnly, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createManifestForFutureDespatch", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    despatchDate,
                    specificDateOnly}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndcreateManifestForFutureDespatch(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void createManifestForFutureDespatchAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime despatchDate, bool specificDateOnly)
        {
            this.createManifestForFutureDespatchAsync(carrierCode, warehouseCode, transactionType, manifestGroupCode, despatchDate, specificDateOnly, null);
        }

        /// <remarks/>
        public void createManifestForFutureDespatchAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime despatchDate, bool specificDateOnly, object userState)
        {
            if ((this.createManifestForFutureDespatchOperationCompleted == null))
            {
                this.createManifestForFutureDespatchOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateManifestForFutureDespatchOperationCompleted);
            }
            this.InvokeAsync("createManifestForFutureDespatch", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    despatchDate,
                    specificDateOnly}, this.createManifestForFutureDespatchOperationCompleted, userState);
        }

        private void OncreateManifestForFutureDespatchOperationCompleted(object arg)
        {
            if ((this.createManifestForFutureDespatchCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createManifestForFutureDespatchCompleted(this, new createManifestForFutureDespatchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createManifestReturn")]
        public string[] createManifest(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode)
        {
            object[] results = this.Invoke("createManifest", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode});
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateManifest(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createManifest", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode}, callback, asyncState);
        }

        /// <remarks/>
        public string[] EndcreateManifest(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        /// <remarks/>
        public void createManifestAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode)
        {
            this.createManifestAsync(carrierCode, warehouseCode, transactionType, manifestGroupCode, null);
        }

        /// <remarks/>
        public void createManifestAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, object userState)
        {
            if ((this.createManifestOperationCompleted == null))
            {
                this.createManifestOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateManifestOperationCompleted);
            }
            this.InvokeAsync("createManifest", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode}, this.createManifestOperationCompleted, userState);
        }

        private void OncreateManifestOperationCompleted(object arg)
        {
            if ((this.createManifestCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createManifestCompleted(this, new createManifestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findConsignmentsOnManifestReturn")]
        public Consignment[] findConsignmentsOnManifest(string manifestCode)
        {
            object[] results = this.Invoke("findConsignmentsOnManifest", new object[] {
                    manifestCode});
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindConsignmentsOnManifest(string manifestCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findConsignmentsOnManifest", new object[] {
                    manifestCode}, callback, asyncState);
        }

        /// <remarks/>
        public Consignment[] EndfindConsignmentsOnManifest(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Consignment[])(results[0]));
        }

        /// <remarks/>
        public void findConsignmentsOnManifestAsync(string manifestCode)
        {
            this.findConsignmentsOnManifestAsync(manifestCode, null);
        }

        /// <remarks/>
        public void findConsignmentsOnManifestAsync(string manifestCode, object userState)
        {
            if ((this.findConsignmentsOnManifestOperationCompleted == null))
            {
                this.findConsignmentsOnManifestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindConsignmentsOnManifestOperationCompleted);
            }
            this.InvokeAsync("findConsignmentsOnManifest", new object[] {
                    manifestCode}, this.findConsignmentsOnManifestOperationCompleted, userState);
        }

        private void OnfindConsignmentsOnManifestOperationCompleted(object arg)
        {
            if ((this.findConsignmentsOnManifestCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findConsignmentsOnManifestCompleted(this, new findConsignmentsOnManifestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findManifestsReturn")]
        public Manifest[] findManifests(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime dateFrom, System.DateTime dateTo)
        {
            object[] results = this.Invoke("findManifests", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    dateFrom,
                    dateTo});
            return ((Manifest[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindManifests(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime dateFrom, System.DateTime dateTo, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findManifests", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    dateFrom,
                    dateTo}, callback, asyncState);
        }

        /// <remarks/>
        public Manifest[] EndfindManifests(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((Manifest[])(results[0]));
        }

        /// <remarks/>
        public void findManifestsAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime dateFrom, System.DateTime dateTo)
        {
            this.findManifestsAsync(carrierCode, warehouseCode, transactionType, manifestGroupCode, dateFrom, dateTo, null);
        }

        /// <remarks/>
        public void findManifestsAsync(string carrierCode, string warehouseCode, string transactionType, string manifestGroupCode, System.DateTime dateFrom, System.DateTime dateTo, object userState)
        {
            if ((this.findManifestsOperationCompleted == null))
            {
                this.findManifestsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindManifestsOperationCompleted);
            }
            this.InvokeAsync("findManifests", new object[] {
                    carrierCode,
                    warehouseCode,
                    transactionType,
                    manifestGroupCode,
                    dateFrom,
                    dateTo}, this.findManifestsOperationCompleted, userState);
        }

        private void OnfindManifestsOperationCompleted(object arg)
        {
            if ((this.findManifestsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findManifestsCompleted(this, new findManifestsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("findReadyToManifestRecordsReturn")]
        public ReadyToManifestInfo[] findReadyToManifestRecords(string warehouseCode, string transactionType)
        {
            object[] results = this.Invoke("findReadyToManifestRecords", new object[] {
                    warehouseCode,
                    transactionType});
            return ((ReadyToManifestInfo[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfindReadyToManifestRecords(string warehouseCode, string transactionType, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("findReadyToManifestRecords", new object[] {
                    warehouseCode,
                    transactionType}, callback, asyncState);
        }

        /// <remarks/>
        public ReadyToManifestInfo[] EndfindReadyToManifestRecords(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ReadyToManifestInfo[])(results[0]));
        }

        /// <remarks/>
        public void findReadyToManifestRecordsAsync(string warehouseCode, string transactionType)
        {
            this.findReadyToManifestRecordsAsync(warehouseCode, transactionType, null);
        }

        /// <remarks/>
        public void findReadyToManifestRecordsAsync(string warehouseCode, string transactionType, object userState)
        {
            if ((this.findReadyToManifestRecordsOperationCompleted == null))
            {
                this.findReadyToManifestRecordsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindReadyToManifestRecordsOperationCompleted);
            }
            this.InvokeAsync("findReadyToManifestRecords", new object[] {
                    warehouseCode,
                    transactionType}, this.findReadyToManifestRecordsOperationCompleted, userState);
        }

        private void OnfindReadyToManifestRecordsOperationCompleted(object arg)
        {
            if ((this.findReadyToManifestRecordsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findReadyToManifestRecordsCompleted(this, new findReadyToManifestRecordsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("sendManifestReturn")]
        public bool sendManifest(string manifestCode)
        {
            object[] results = this.Invoke("sendManifest", new object[] {
                    manifestCode});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginsendManifest(string manifestCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("sendManifest", new object[] {
                    manifestCode}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndsendManifest(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void sendManifestAsync(string manifestCode)
        {
            this.sendManifestAsync(manifestCode, null);
        }

        /// <remarks/>
        public void sendManifestAsync(string manifestCode, object userState)
        {
            if ((this.sendManifestOperationCompleted == null))
            {
                this.sendManifestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnsendManifestOperationCompleted);
            }
            this.InvokeAsync("sendManifest", new object[] {
                    manifestCode}, this.sendManifestOperationCompleted, userState);
        }

        private void OnsendManifestOperationCompleted(object arg)
        {
            if ((this.sendManifestCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.sendManifestCompleted(this, new sendManifestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "urn:DeliveryManager/services", ResponseNamespace = "urn:DeliveryManager/services")]
        [return: System.Xml.Serialization.SoapElementAttribute("createManifestAsPdfReturn")]
        public string createManifestAsPdf(string manifestCode)
        {
            object[] results = this.Invoke("createManifestAsPdf", new object[] {
                    manifestCode});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateManifestAsPdf(string manifestCode, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createManifestAsPdf", new object[] {
                    manifestCode}, callback, asyncState);
        }

        /// <remarks/>
        public string EndcreateManifestAsPdf(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void createManifestAsPdfAsync(string manifestCode)
        {
            this.createManifestAsPdfAsync(manifestCode, null);
        }

        /// <remarks/>
        public void createManifestAsPdfAsync(string manifestCode, object userState)
        {
            if ((this.createManifestAsPdfOperationCompleted == null))
            {
                this.createManifestAsPdfOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateManifestAsPdfOperationCompleted);
            }
            this.InvokeAsync("createManifestAsPdf", new object[] {
                    manifestCode}, this.createManifestAsPdfOperationCompleted, userState);
        }

        private void OncreateManifestAsPdfOperationCompleted(object arg)
        {
            if ((this.createManifestAsPdfCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createManifestAsPdfCompleted(this, new createManifestAsPdfCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class Manifest
    {

        private string carrierCodeField;

        private int consignmentCountField;

        private System.Nullable<System.DateTime> createdDateTimeField;

        private string manifestCodeField;

        private string manifestGroupCodeField;

        private string manifestStatusField;

        private System.Nullable<System.DateTime> sentDateTimeField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCode
        {
            get
            {
                return this.carrierCodeField;
            }
            set
            {
                this.carrierCodeField = value;
            }
        }

        /// <remarks/>
        public int consignmentCount
        {
            get
            {
                return this.consignmentCountField;
            }
            set
            {
                this.consignmentCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> createdDateTime
        {
            get
            {
                return this.createdDateTimeField;
            }
            set
            {
                this.createdDateTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string manifestCode
        {
            get
            {
                return this.manifestCodeField;
            }
            set
            {
                this.manifestCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string manifestGroupCode
        {
            get
            {
                return this.manifestGroupCodeField;
            }
            set
            {
                this.manifestGroupCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string manifestStatus
        {
            get
            {
                return this.manifestStatusField;
            }
            set
            {
                this.manifestStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> sentDateTime
        {
            get
            {
                return this.sentDateTimeField;
            }
            set
            {
                this.sentDateTimeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "urn:DeliveryManager/types")]
    public partial class ReadyToManifestInfo
    {

        private string carrierCodeField;

        private int consignmentCountField;

        private string manifestGroupCodeField;

        private int parcelCountField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string carrierCode
        {
            get
            {
                return this.carrierCodeField;
            }
            set
            {
                this.carrierCodeField = value;
            }
        }

        /// <remarks/>
        public int consignmentCount
        {
            get
            {
                return this.consignmentCountField;
            }
            set
            {
                this.consignmentCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string manifestGroupCode
        {
            get
            {
                return this.manifestGroupCodeField;
            }
            set
            {
                this.manifestGroupCodeField = value;
            }
        }

        /// <remarks/>
        public int parcelCount
        {
            get
            {
                return this.parcelCountField;
            }
            set
            {
                this.parcelCountField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createManifestForFutureDespatchCompletedEventHandler(object sender, createManifestForFutureDespatchCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createManifestForFutureDespatchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createManifestForFutureDespatchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createManifestCompletedEventHandler(object sender, createManifestCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createManifestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createManifestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findConsignmentsOnManifestCompletedEventHandler(object sender, findConsignmentsOnManifestCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findConsignmentsOnManifestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findConsignmentsOnManifestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Consignment[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Consignment[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findManifestsCompletedEventHandler(object sender, findManifestsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findManifestsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findManifestsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public Manifest[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((Manifest[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void findReadyToManifestRecordsCompletedEventHandler(object sender, findReadyToManifestRecordsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findReadyToManifestRecordsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal findReadyToManifestRecordsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public ReadyToManifestInfo[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ReadyToManifestInfo[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void sendManifestCompletedEventHandler(object sender, sendManifestCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sendManifestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal sendManifestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createManifestAsPdfCompletedEventHandler(object sender, createManifestAsPdfCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createManifestAsPdfCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createManifestAsPdfCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState)
            :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}
