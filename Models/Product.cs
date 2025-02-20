using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class Product {

    public int Id {get;set;}

    public string? ProductName {get;set;}

    [DataType(DataType.Date)]
    public DateTime ProductAddingDate {get;set;}

    public string? ProductNetworkPath{get;set;}

    public int ProductStockNumber{get;set;}

    public string? ProductDetail{get;set;}

    public int ShopId {get;set;}

}