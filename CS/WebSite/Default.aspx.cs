using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack && !IsCallback)
            Session.Clear();
        ((BootstrapGridViewComboBoxColumn)BootstrapGridView1.Columns["TypeID"]).PropertiesComboBox.DataSource = ItemTypeFactory.GetItemTypes();
        BootstrapGridView1.DataSource = objectDataSource1;
        BootstrapGridView1.DataBind();

    }
    protected void OnItemRequestedByValue_View(object source, ListEditItemRequestedByValueEventArgs e) {
        if (e.Value == null) return;
        int subTypeID = (int)e.Value;
        BootstrapComboBox editor = source as BootstrapComboBox;
        editor.DataSource = ItemTypeFactory.GetItemSubTypes().Where(s => s.SubTypeID == subTypeID);
        editor.DataBind();
    }
    protected void OnItemsRequestedByFilterCondition_View(object source, ListEditItemsRequestedByFilterConditionEventArgs e) {
        BootstrapComboBox editor = source as BootstrapComboBox;
        editor.DataSource = ItemTypeFactory.GetItemSubTypes().Where(s => s.SubTypeDescription.Contains(e.Filter)
        || s.SubTypeName.Contains(e.Filter)
        || s.SubTypeID.ToString().Contains(e.Filter));
        editor.DataBind();
    }
    protected void OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e) {
        if (e.Value == null) return;
        string id = e.Value.ToString();
        BootstrapComboBox editor = source as BootstrapComboBox;
        int typeID = GetCurrentItemTypeID();
        List<SubType> subTypes;
        if (typeID > -1)
            subTypes = ItemTypeFactory.GetItemSubTypes().Where(s => s.TypeID == typeID && s.SubTypeID.ToString() == id).ToList();
        else
            subTypes = ItemTypeFactory.GetItemSubTypes().Where(s => s.TypeID == typeID).ToList();
        editor.DataSource = subTypes;
        editor.DataBind();
    }

    protected void OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e) {
        BootstrapComboBox editor = source as BootstrapComboBox;
        int typeID = GetCurrentItemTypeID();
        List<SubType> subTypes;
        if (typeID > -1)
            subTypes = ItemTypeFactory.GetItemSubTypes().Where(s => s.TypeID == typeID && (s.SubTypeDescription.Contains(e.Filter)
        || s.SubTypeName.Contains(e.Filter)
        || s.SubTypeID.ToString().Contains(e.Filter))).ToList();
        else
            subTypes = ItemTypeFactory.GetItemSubTypes().Where(s => s.SubTypeDescription.Contains(e.Filter)
        || s.SubTypeName.Contains(e.Filter)
        || s.SubTypeID.ToString().Contains(e.Filter)).ToList();
        editor.DataSource = subTypes;
        editor.DataBind();
    }
    public int GetCurrentItemTypeID() {
        object typeID;
        if (ASPxHiddenField1.TryGet("currentTypeID", out typeID))
            return Convert.ToInt32(typeID);
        return -1;
    }
}