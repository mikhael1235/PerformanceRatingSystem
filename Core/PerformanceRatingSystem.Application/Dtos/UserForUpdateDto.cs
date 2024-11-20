﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceRatingSystem.Application.Dtos;

public class UserForUpdateDto
{
    public string Id { get; set; }

    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Display(Name = "Имя")]
    public string? FirstName { get; set; }

    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = "Некорректный адрес")]
    public string Email { get; set; }

    [Display(Name = "Дата регистрации")]
    [DataType(DataType.Date)]
    public DateTime RegistrationDate { get; set; }

    [Display(Name = "Роль")]
    public string UserRole { get; set; } 

    public UserForUpdateDto()
    {
        UserRole = "User";
    }
}