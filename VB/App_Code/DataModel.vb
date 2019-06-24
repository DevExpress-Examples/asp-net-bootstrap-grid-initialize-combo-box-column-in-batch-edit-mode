Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Public Class DataModel
	Private Const SessionKey As String = "Items"
	Public Sub SaveToSession(ByVal items As List(Of Item))
		HttpContext.Current.Session(SessionKey) = items
	End Sub
	Public Function GetItems() As List(Of Item)
		If HttpContext.Current.Session(SessionKey) Is Nothing Then
			GenerateItems()
		End If
		Return CType(HttpContext.Current.Session(SessionKey), List(Of Item))
	End Function
	Public Sub InsertItem(ByVal item As Item)
		Dim items As List(Of Item) = GetItems()
		Dim id As Integer = GetNewID()
		item.ID = id
		items.Add(item)
		SaveToSession(items)
	End Sub
	Public Sub RemoveItem(ByVal item As Item)
		Dim items As List(Of Item) = GetItems()
		items.RemoveAll(Function(c) c.ID = item.ID)
		SaveToSession(items)
	End Sub

	Public Sub UpdateItem(ByVal newItemData As Item)
		Dim items As List(Of Item) = GetItems()
		Dim index As Integer = items.IndexOf(items.First(Function(s) s.ID = newItemData.ID))
		items(index) = newItemData
		SaveToSession(items)
	End Sub

	Private Sub GenerateItems()
		Dim items As New List(Of Item)()
		For i As Integer = 0 To 9
			For j As Integer = 0 To 4
				Dim item As New Item()
				item.ID = i * 5 + j
				item.TypeID = i
				item.SubTypeID = i * 10 + j
				items.Add(item)
			Next j
		Next i
		SaveToSession(items)
	End Sub
	Private Function GetNewID() As Integer
		Dim items As List(Of Item) = GetItems()
		Dim maxID As Integer = items.Max(Function(s) s.ID)
		Return maxID + 1
	End Function
End Class

Public NotInheritable Class ItemTypeFactory

	Private Sub New()
	End Sub

	Public Shared Function GetItemTypes() As List(Of ItemType)
		Dim itemTypes As New List(Of ItemType)()
		For i As Integer = 0 To 9
			Dim itemType As New ItemType()
			itemType.TypeID = i
			itemType.TypeName = String.Format("Type: {0}", i)

			itemTypes.Add(itemType)
		Next i
		Return itemTypes
	End Function
	Public Shared Function GetItemSubTypes() As List(Of SubType)
		Dim itemSubTypes As New List(Of SubType)()
		For i As Integer = 0 To 9
			For j As Integer = 0 To 4
				Dim subType As New SubType()
				subType.TypeID = i
				subType.SubTypeID = i * 10 + j
				subType.SubTypeName = String.Format("SubType: {0:00}", i * 10 + j)
				subType.SubTypeDescription = String.Format("SubType description {0}", i * j + j)
				itemSubTypes.Add(subType)
			Next j
		Next i
		Return itemSubTypes
	End Function
End Class

Public Class Item
	Public Property ID() As Integer
	Public Property TypeID() As Integer
	Public Property SubTypeID() As Integer
End Class

Public Class ItemType
	Public Property TypeID() As Integer
	Public Property TypeName() As String
End Class
Public Class SubType
	Public Property TypeID() As Integer
	Public Property SubTypeID() As Integer
	Public Property SubTypeName() As String
	Public Property SubTypeDescription() As String
End Class