﻿using System;
using System.Web.UI;

namespace WebForms
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}