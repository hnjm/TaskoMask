﻿using TaskoMask.BuildingBlocks.Application.Commands;
using System.ComponentModel.DataAnnotations;
using TaskoMask.BuildingBlocks.Contracts.Resources;
using TaskoMask.BuildingBlocks.Contracts.Helpers;

namespace TaskoMask.Services.Monolith.Application.Workspace.Owners.Commands.Models
{
    public abstract class OwnerBaseCommand : BaseCommand
    {
        protected OwnerBaseCommand( string displayName, string email)
        {
            DisplayName = displayName;
            Email = email;
        }


        [StringLength(DomainConstValues.Owner_DisplayName_Max_Length, MinimumLength = DomainConstValues.Owner_DisplayName_Min_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        [Required(ErrorMessageResourceName = nameof(ContractsMetadata.Required), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string DisplayName { get; }
       
        
        [Required(ErrorMessageResourceName = nameof(ContractsMetadata.Required), ErrorMessageResourceType = typeof(ContractsMetadata))]
        [StringLength(DomainConstValues.Owner_Email_Max_Length, MinimumLength = DomainConstValues.Owner_Email_Min_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        [EmailAddress]
        public string Email { get; }

    }
}