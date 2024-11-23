using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManagementAPI.Dto;

public class UserCredentialDTO
{
    [Required]
    public required string Username {get; set;}

    [Required]
    public required string Password {get; set;}
}
