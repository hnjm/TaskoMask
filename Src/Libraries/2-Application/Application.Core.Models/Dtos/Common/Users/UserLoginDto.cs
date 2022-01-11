﻿using System.ComponentModel.DataAnnotations;
using TaskoMask.Application.Share.Resources;
using TaskoMask.Domain.Share.Helpers;
using TaskoMask.Domain.Share.Resources;

namespace TaskoMask.Application.Share.Dtos.Common.Users
{
    public class UserLoginDto
    {

        [Display(Name = nameof(ApplicationMetadata.Email), ResourceType = typeof(ApplicationMetadata))]
        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = nameof(ApplicationMetadata.Password), ResourceType = typeof(ApplicationMetadata))]
        [Required(ErrorMessageResourceName = nameof(DomainMessages.Required), ErrorMessageResourceType = typeof(DomainMessages))]
        [StringLength(DomainConstValues.Member_Password_Max_Length, MinimumLength = DomainConstValues.Member_Password_Min_Length, ErrorMessageResourceName = nameof(DomainMessages.Length_Error), ErrorMessageResourceType = typeof(DomainMessages))]
        [DataType(DataType.Password)]
        public string Password { get; set; }


       
        /// <summary>
        /// use in cookie authentication
        /// </summary>
        [Display(Name = nameof(ApplicationMetadata.RememberMe), ResourceType = typeof(ApplicationMetadata))]
        public bool RememberMe { get; set; }

    }
}
