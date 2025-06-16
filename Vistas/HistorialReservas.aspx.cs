using System;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Vistas
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        NegocioHistorialReservas negocioHistorialReservas = new NegocioHistorialReservas();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable HistorialReservas = negocioHistorialReservas.GetHistorialReserva();
            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            string numeroHabitacion = txtNumber.Text;
            string fechaDesde = txtDateFrom.Text;
            string fechaHasta = txtDateTo.Text;

            DataTable HistorialReservas = negocioHistorialReservas.GetFilterHistorialReserva(
                numeroHabitacion, fechaDesde, fechaHasta);

            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
        }

        protected void btnSacarFiltro_Click(object sender, EventArgs e)
        {
            DataTable HistorialReservas = negocioHistorialReservas.GetHistorialReserva();
            grvHistorialReservas.DataSource = HistorialReservas;
            grvHistorialReservas.DataBind();
            txtNumber.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
        }
    }
}