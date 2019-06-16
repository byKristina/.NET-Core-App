using Application.DTO;
using Application.Interfaces;
using Application.Responses;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CategoriesCommands
{
    public interface IGetAllCategoriesCommand : ICommand<CategorySearch, IEnumerable<CategoryDto>>
    {
    }
}
