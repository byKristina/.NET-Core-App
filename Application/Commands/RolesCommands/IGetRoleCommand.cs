﻿using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.RolesCommands
{
    public interface IGetRoleCommand : ICommand<int, RoleDto> 
    {
    }
}
