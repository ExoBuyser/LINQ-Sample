<Query Kind="Expression">
  <Connection>
    <ID>b2b8ab30-4d5d-4944-8af6-6455130aad4b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//This is a muliplr column group
//grouping data placed in a local temp data set for further processing
//.Key allows you to have access the value in your group key
//if you have multiple group columns they MUST be in an anonymous datatype
//to create a DTO type collection you can use .ToList() on the temp data set
//you can have a custom anonymous data collection by using a nested query

//STEP A
from food in Items
    group food by new {
	food.MenuCategoryID, food.CurrentPrice
	}
	
//STEP B DTO style dataset
from food in Items
    group food by new {
	food.MenuCategoryID, food.CurrentPrice
	} into tempdataset
	select new {
		MenuCategoryID = tempdataset.Key.MenuCategoryID,
		CurrentPrice = tempdataset.key.CurrentPrice,
		FoodItems = tempdataset.ToList()
	}

//STEP C
from food in Items
    group food by new {
	food.MenuCategoryID, food.CurrentPrice
	} into tempdataset
	select new {
		MenuCategoryID = tempdataset.Key.MenuCategoryID,
		CurrentPrice = tempdataset.Key.CurrentPrice,
		FoodItems = from x in tempdataset
					select new {
						ItemID = x.ItemID,
						FoodDescription = x.Description,
						TimeServed = x.BillItems.Count()
					}
	}