﻿using Volo.Abp.Application.Dtos;

namespace Lazy.Abp.IdentityServer.IdentityResources
{
    public class IdentityResourceGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
