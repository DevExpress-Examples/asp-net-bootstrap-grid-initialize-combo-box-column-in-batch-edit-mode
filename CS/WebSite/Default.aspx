<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.1, Version=19.1.16.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.16.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.css" />
    <title>BootstrapGridView - Batch Edit - How to initialize a ComboBox column based on a row</title>
    <script type="text/javascript">
        function OnBatchEditStartEdit(s, e) {
            var currentTypeID = grid.batchEditApi.GetCellValue(e.visibleIndex, 'TypeID');
            var cellInfo = e.rowValues[ColIndexByName('SubTypeID')];
            this.setTimeout(function () {
                bsCombobox.SetValue(cellInfo.value);
                bsCombobox.SetText(cellInfo.text);
            }, 0);
            RefreshData(currentTypeID);
        }
        function OnBatchEditEndEdit(s, e) {
            var cellInfo = e.rowValues[ColIndexByName('SubTypeID')];
            cellInfo.value = bsCombobox.GetValue();
            cellInfo.text = bsCombobox.GetText();
            bsCombobox.SetValue(null);
        }
        function ColIndexByName(name) {
            return grid.GetColumnByField(name).index;
        }
        function RefreshData(currentTypeID) {
            hf1.Set('currentTypeID', currentTypeID);
            bsCombobox.PerformCallback();
        }
        function bsCombobox_LostFocus(s, e) {
            grid.batchEditApi.EndEdit();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server" ClientInstanceName="hf1"></dx:ASPxHiddenField>
            <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" KeyFieldName="ID" ClientInstanceName="grid" EnableCallBacks="true">
                <SettingsDataSecurity AllowEdit="True" AllowInsert="True" AllowDelete="True"></SettingsDataSecurity>
                <Columns>
                    <dx:BootstrapGridViewCommandColumn ShowNewButtonInHeader="True" VisibleIndex="0" ShowDeleteButton="True"></dx:BootstrapGridViewCommandColumn>
                    <dx:BootstrapGridViewDataColumn FieldName="ID" VisibleIndex="1">
                        <SettingsEditForm Visible="False" />
                    </dx:BootstrapGridViewDataColumn>
                    <dx:BootstrapGridViewComboBoxColumn FieldName="TypeID" VisibleIndex="3">
                        <PropertiesComboBox ValueField="TypeID" TextField="TypeName">
                        </PropertiesComboBox>
                    </dx:BootstrapGridViewComboBoxColumn>

                    <dx:BootstrapGridViewComboBoxColumn Caption="ItemSubType" FieldName="SubTypeID" VisibleIndex="4">
                        <PropertiesComboBox ValueField="SubTypeID" OnItemRequestedByValue="OnItemRequestedByValue_View" OnItemsRequestedByFilterCondition="OnItemsRequestedByFilterCondition_View" TextFormatString="{0} ---- {1}">
                            <Fields>
                                <dx:BootstrapListBoxField FieldName="SubTypeName" />
                                <dx:BootstrapListBoxField FieldName="SubTypeDescription" />
                            </Fields>
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <dx:BootstrapComboBox ID="bsCombobox" runat="server" EnableCallbackMode="true" ClientInstanceName="bsCombobox" OnItemRequestedByValue="OnItemRequestedByValue" OnItemsRequestedByFilterCondition="OnItemsRequestedByFilterCondition" ValueField="SubTypeID" TextFormatString="SubTypeID: {0} ---- Desription: {1}">
                                <Fields>
                                    <dx:BootstrapListBoxField FieldName="SubTypeName" />
                                    <dx:BootstrapListBoxField FieldName="SubTypeDescription" />
                                </Fields>
                                <ClientSideEvents LostFocus="bsCombobox_LostFocus" />
                            </dx:BootstrapComboBox>
                        </EditItemTemplate>
                    </dx:BootstrapGridViewComboBoxColumn>
                </Columns>
                <SettingsEditing Mode="Batch">
                    <BatchEditSettings EditMode="Cell" />
                </SettingsEditing>
                <ClientSideEvents BatchEditStartEditing="OnBatchEditStartEdit" BatchEditEndEditing="OnBatchEditEndEdit" />
            </dx:BootstrapGridView>
            <asp:ObjectDataSource ID="objectDataSource1" runat="server" DataObjectTypeName="Item" DeleteMethod="RemoveItem" InsertMethod="InsertItem" SelectMethod="GetItems" TypeName="DataModel" UpdateMethod="UpdateItem"></asp:ObjectDataSource>
        </div>

    </form>
</body>
</html>
