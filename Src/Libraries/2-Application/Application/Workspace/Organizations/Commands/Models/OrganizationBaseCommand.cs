﻿using TaskoMask.Application.Core.Commands;
using System.ComponentModel.DataAnnotations;
using TaskoMask.Domain.Share.Resources;
using TaskoMask.Domain.Share.Helpers;
using TaskoMask.Application.Share.Resources;

namespace TaskoMask.Application.Workspace.Organizations.Commands.Models
{
    public abstract class OrganizationBaseCommand : BaseCommand
    {
        protected OrganizationBaseCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [Display(Name = nameof(ApplicationMetadata.Name), ResourceType = typeof(ApplicationMetadata))]
        [StringLength(DomainConstValues.Organization_Name_Max_Length, MinimumLength = DomainConstValues.Organization_Name_Min_Length, ErrorMessageResourceName = nameof(DomainMessages.Length_Error), ErrorMessageResourceType = typeof(DomainMessages))]
        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        public string Name { get; }


        [Display(Name = nameof(ApplicationMetadata.Description), ResourceType = typeof(ApplicationMetadata))]
        [StringLength(DomainConstValues.Organization_Description_Max_Length, MinimumLength = DomainConstValues.Organization_Description_Min_Length, ErrorMessageResourceName = nameof(DomainMessages.Length_Error), ErrorMessageResourceType = typeof(DomainMessages))]
        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        public string Description { get; }


    }
}
