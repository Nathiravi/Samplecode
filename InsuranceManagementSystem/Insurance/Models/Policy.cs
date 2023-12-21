using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Models;

public class Policy{

    public int PolicyID{get;set;}
    public int Userid {get;set;}
    public string? Name{get;set;}
    public string? Type {get;set;}
    public int  Age {get;set;}
    public string? ClaimAmount {get;set;}
   
    public string? PolicyName{get;set;}
    public string? PolicyDuration {get;set;}
    public DateTime PolicyDate {get;set;}

}