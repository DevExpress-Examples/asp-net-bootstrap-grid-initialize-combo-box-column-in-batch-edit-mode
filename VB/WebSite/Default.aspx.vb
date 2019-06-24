Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If (Not IsPostBack) AndAlso (Not IsCallback) Then
			Session.Clear()
		End If
		CType(BootstrapGridView1.Columns("TypeID"), BootstrapGridViewComboBoxColumn).PropertiesComboBox.DataSource = ItemTypeFactory.GetItemTypes()
		BootstrapGridView1.DataSource = objectDataSource1
		BootstrapGridView1.DataBind()

	End Sub
	Protected Sub OnItemRequestedByValue_View(ByVal source As Object, ByVal e As ListEditItemRequestedByValueEventArgs)
		If e.Value Is Nothing Then
			Return
		End If
		Dim subTypeID As Integer = CInt(Fix(e.Value))
		Dim editor As BootstrapComboBox = TryCast(source, BootstrapComboBox)
		editor.DataSource = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.SubTypeID = subTypeID)
		editor.DataBind()
	End Sub
	Protected Sub OnItemsRequestedByFilterCondition_View(ByVal source As Object, ByVal e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim editor As BootstrapComboBox = TryCast(source, BootstrapComboBox)
		editor.DataSource = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.SubTypeDescription.Contains(e.Filter) OrElse s.SubTypeName.Contains(e.Filter) OrElse s.SubTypeID.ToString().Contains(e.Filter))
		editor.DataBind()
	End Sub
	Protected Sub OnItemRequestedByValue(ByVal source As Object, ByVal e As ListEditItemRequestedByValueEventArgs)
		If e.Value Is Nothing Then
			Return
		End If
		Dim id_Renamed As String = e.Value.ToString()
		Dim editor As BootstrapComboBox = TryCast(source, BootstrapComboBox)
		Dim typeID As Integer = GetCurrentItemTypeID()
		Dim subTypes As List(Of SubType)
		If typeID > -1 Then
			subTypes = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.TypeID = typeID AndAlso s.SubTypeID.ToString() = id_Renamed).ToList()
		Else
			subTypes = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.TypeID = typeID).ToList()
		End If
		editor.DataSource = subTypes
		editor.DataBind()
	End Sub

	Protected Sub OnItemsRequestedByFilterCondition(ByVal source As Object, ByVal e As ListEditItemsRequestedByFilterConditionEventArgs)
		Dim editor As BootstrapComboBox = TryCast(source, BootstrapComboBox)
		Dim typeID As Integer = GetCurrentItemTypeID()
		Dim subTypes As List(Of SubType)
		If typeID > -1 Then
			subTypes = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.TypeID = typeID AndAlso (s.SubTypeDescription.Contains(e.Filter) OrElse s.SubTypeName.Contains(e.Filter) OrElse s.SubTypeID.ToString().Contains(e.Filter))).ToList()
		Else
			subTypes = ItemTypeFactory.GetItemSubTypes().Where(Function(s) s.SubTypeDescription.Contains(e.Filter) OrElse s.SubTypeName.Contains(e.Filter) OrElse s.SubTypeID.ToString().Contains(e.Filter)).ToList()
		End If
		editor.DataSource = subTypes
		editor.DataBind()
	End Sub
	Public Function GetCurrentItemTypeID() As Integer
		Dim typeID As Object
		If ASPxHiddenField1.TryGet("currentTypeID", typeID) Then
			Return Convert.ToInt32(typeID)
		End If
		Return -1
	End Function
End Class
