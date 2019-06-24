using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class DataModel {
    const string SessionKey = "Items";
    public void SaveToSession(List<Item> items) {
        HttpContext.Current.Session[SessionKey] = items;
    }
    public List<Item> GetItems() {
        if (HttpContext.Current.Session[SessionKey] == null) {
            GenerateItems();
        }
        return (List<Item>)HttpContext.Current.Session[SessionKey];
    }
    public void InsertItem(Item item) {
        List<Item> items = GetItems();
        int id = GetNewID();
        item.ID = id;
        items.Add(item);
        SaveToSession(items);
    }
    public void RemoveItem(Item item) {
        List<Item> items = GetItems();
        items.RemoveAll(c => c.ID == item.ID);
        SaveToSession(items);
    }

    public void UpdateItem(Item newItemData) {
        List<Item> items = GetItems();
        int index = items.IndexOf(items.First(s => s.ID == newItemData.ID));
        items[index] = newItemData;
        SaveToSession(items);
    }

    void GenerateItems() {
        List<Item> items = new List<Item>();
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 5; j++) {
                Item item = new Item();
                item.ID = i * 5 + j;                
                item.TypeID = i;
                item.SubTypeID = i * 10 + j;
                items.Add(item);
            }
        }
        SaveToSession(items);
    }
    int GetNewID() {
        List<Item> items = GetItems();
        int maxID = items.Max(s => s.ID);
        return maxID + 1;
    }
}

public static class ItemTypeFactory {
    public static List<ItemType> GetItemTypes() {
        List<ItemType> itemTypes = new List<ItemType>();
        for (int i = 0; i < 10; i++) {
            ItemType itemType = new ItemType();
            itemType.TypeID = i;
            itemType.TypeName = String.Format("Type: {0}", i);

            itemTypes.Add(itemType);
        }
        return itemTypes;
    }
    public static List<SubType> GetItemSubTypes() {
        List<SubType> itemSubTypes = new List<SubType>();
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 5; j++) {
                SubType subType = new SubType();
                subType.TypeID = i;
                subType.SubTypeID = i * 10 + j;
                subType.SubTypeName = String.Format("SubType: {0:00}", i * 10 + j);
                subType.SubTypeDescription = String.Format("SubType description {0}", i * j + j);
                itemSubTypes.Add(subType);
            }
        }
        return itemSubTypes;
    }
}

public class Item {
    public int ID { get; set; }
    public int TypeID { get; set; }
    public int SubTypeID { get; set; }
}

public class ItemType {
    public int TypeID { get; set; }
    public string TypeName { get; set; }
}
public class SubType {
    public int TypeID { get; set; }
    public int SubTypeID { get; set; }
    public string SubTypeName { get; set; }
    public string SubTypeDescription { get; set; }
}