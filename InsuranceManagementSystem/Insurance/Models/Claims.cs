using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Models;

public class Claims{

    public int ClaimID{get;set;}

    public int PolicyID{get;set;}
   
    public string? Document{get;set;}
    public string? Status {get;set;}

}