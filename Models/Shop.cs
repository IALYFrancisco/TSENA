using System.ComponentModel.DataAnnotations;

namespace TSENA.Models;

public class Shop {

    public int Id {get;set;}

    public string? ShopName {get;set;}

    [DataType(DataType.Date)]
    public DateTime ShopCreationDate {get;set;}

    public string? ShopLogoNetworkPath{get;set;}

}