using System;
using Data.Repositories;

namespace WebForms
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductsRepeater();
            }
        }

        private void BindProductsRepeater()
        {
            var repo = new ProductRepository();
            var products = repo.GetAll();
            rProducts.DataSource = products;
            rProducts.DataBind();
        }
    }
}