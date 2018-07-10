using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IHF.BusinessLayer.BusinessClasses;
using IHF.BusinessLayer.DataAccessObjects;
using IHF.EnterpriseLibrary.DataServices;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using IHF.BusinessLayer.DataAccessObjects.Despatch;


namespace IHF.ApplicationLayer.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DespatchServiceDAO despatchDAO;
            //despatchDAO = new DespatchServiceDAO();

            //IDataReader result = despatchDAO.Despatch("DPD", "dtadmin");
            //result.Read();

            //Response.Write(result[0].ToString());
            
        }

        protected void GetClassObject_Click(object sender, EventArgs e)
        {
            OrderDAO omgr = new OrderDAO();
            Order order = omgr.OrderDetail(1);
            OrderNumber.Text = order.OrderNumber.ToString();
            Description.Text = order.Description.ToString();
        }

        protected void NormalQuery_Click(object sender, EventArgs e)
        {
            OrderDAO omgr = new OrderDAO();
            DataSet dataSet = omgr.AllDetail();
            GridView1.DataSource = dataSet;
            GridView1.DataBind();
        }

        protected void AddOrder_Click(object sender, EventArgs e)
        {
            OrderDAO orderMgr = new OrderDAO();
            Order order = new Order();
            order.OrderNumber = int.Parse(NewOrderNumber.Text);
            order.Description = NewDescription.Text;
            Order newOrder = orderMgr.SaveOrder(order);

            Workstation pack = new Packstation();
            
        }

        protected void ListOfClasses_Click(object sender, EventArgs e)
        {
            OrderDAO omgr = new OrderDAO();
            List<Order> orders = omgr.ListOfOrders();

            GridView2.DataSource = orders;
            GridView2.DataBind();
        }

        public void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            string err = "<b>Error Caught in Page_Error event</b><hr><br>" +
                    "<br><b>Error in: </b>" + Request.Url.ToString() +
                    "<br><b>Error Message: </b>" + objErr.Message.ToString() +
                    "<br><b>Stack Trace:</b><br>" +
                              objErr.StackTrace.ToString();
            Response.Write(err.ToString());
            //Server.ClearError();
        }

        protected void CreateWorkstation_Click(object sender, EventArgs e)
        {
            PackstationDAO packstationMgr = new PackstationDAO();
            Workstation packstation = new Packstation();
            packstation.ID = 0;
            packstation.Type = 2;
            packstation.Status = 2;
            packstation.CreatedOn = DateTime.Today;
            packstation.Barcode = "WS00000000000001";
            packstation.WorkstationLabel = "WS01";
            packstation.CreatedBy = User.Identity.Name;
            packstation.TrolleyID = 1;
            packstation.LastChangedOn = DateTime.Today;
            packstation.LastChangedBy = User.Identity.Name;
            packstation.IsInternational = "F";

            decimal returnValue = packstationMgr.Add(packstation);
            packstation.ID = returnValue;
        }

    }
}